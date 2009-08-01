#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
    
    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using ZSS.TextUploadersLib.Helpers;

namespace ZSS.TextUploadersLib
{
    public class GoogleTranslate
    {
        public Options LanguageOptions { get; private set; }
        public IWebProxy ProxySettings { get; set; }

        public GoogleTranslate(IWebProxy proxySettings)
        {
        	this.ProxySettings = proxySettings;
            this.LanguageOptions = this.GetLanguageOptions();            
        }

        /// <summary>Gets "from country" and "to country" lists from google.</summary>
        /// <returns>2 country list, first for "from country list" second for "to country list".</returns>
        public Options GetLanguageOptions()
        {
            Options gtLangOp = new Options();
            // The remote name could not be resolved: 'translate.google.com'
            try
            {
                WebClient webClient = new WebClient();
                webClient.Proxy = this.ProxySettings;
                string source = webClient.DownloadString("http://translate.google.com/translate_t");
                string[] selectName = new[] { "sl", "tl" };

                for (int i = 0; i < selectName.Length; i++)
                {
                    string countrySource = Regex.Match(source, "(?<=<select name=" + selectName[i] + ").+?(?=</select>)").Value;
                    MatchCollection countryResults = Regex.Matches(countrySource, "(?<=value=\")(.+?)\">(.+?)(?=</option)");
                    foreach (Match countryResult in countryResults)
                    {
                        GTLanguage lang = new GTLanguage(countryResult.Groups[1].Value, countryResult.Groups[2].Value);
                        if (i == 0)
                        {
                            gtLangOp.SourceLangList.Add(lang);
                        }
                        else
                        {
                            gtLangOp.TargetLangList.Add(lang);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return gtLangOp;
        }

        /// <summary>Translate text from google translate.</summary>
        /// <param name="translateInfo"></param>
        /// <returns>First index: Translated text, Second index: Languages</returns>
        public ResultPacket TranslateText(TranslationInfo translateInfo)
        {
            return TranslateText(translateInfo.SourceText, translateInfo.SourceLanguage, translateInfo.TargetLanguage);
        }

        /// <summary>Translate text from google translate.</summary>
        /// <param name="sourceText">Text for translate.</param>
        /// <param name="sourceLanguage"></param>
        /// <param name="targetLanguage"></param>
        /// <returns>First index: Translated text, Second index: Languages</returns>
        public ResultPacket TranslateText(string sourceText, GTLanguage sourceLanguage, GTLanguage targetLanguage)
        {
            ResultPacket result = new ResultPacket();
            try
            {
                if (string.IsNullOrEmpty(sourceLanguage.Value))
                {
                    sourceLanguage.Value = "auto";
                }
                string url = GetDownloadLink(sourceText, sourceLanguage.Value, targetLanguage.Value);
                WebClient webClient = new WebClient { Encoding = Encoding.UTF8 };
                webClient.Proxy = this.ProxySettings;
                string wc = webClient.DownloadString(url);
                result.TranslationType = HttpUtility.HtmlDecode(Regex.Match(wc, "(?<=:</span> ).+?(?=</td>)").NextMatch().Value);
                result.TranslatedText = HttpUtility.HtmlDecode(Regex.Match(wc, "(?<=(?:ltr|rtl)\">).+?(?=</div>)").Value);
                result.TranslatedText = result.TranslatedText.Replace(" \r<br> ", Environment.NewLine);
                result.Dictionary = SearchGrammer(wc);
            }
            catch (Exception ex)
            {
                result.TranslationType = string.Format("{0} » {1}", sourceLanguage.Name, targetLanguage.Name);
                result.TranslatedText = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Method to populate Dictionary
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private string SearchGrammer(string source)
        {
            string result = "";
            int foundWords = 0;

            string[] dictionary = new[] { "", "adverb", "adjective", "conjunction", "interjection", "noun", "pronoun", "preposition", "verb" };

            for (int x = 0; x < dictionary.Length; x++)
            {
                string matchDictionary = Regex.Match(source, "(?<=top>" + dictionary[x] + "</td>).+?(?=</ol>)").Value;
                if (!string.IsNullOrEmpty(matchDictionary))
                {
                    if (foundWords != 0)
                    {
                        result += "\r\n\r\n";
                    }
                    if (dictionary[x] != "")
                    {
                        result += dictionary[x] + ":\r\n";
                    }
                    MatchCollection matchesDictionary = Regex.Matches(matchDictionary, "(?<=<li>).+?(?=</li>)");
                    for (int i = 0; i < matchesDictionary.Count; i++)
                    {
                        result += (i + 1) + ". " + matchesDictionary[i].Value;
                        if (i != matchesDictionary.Count - 1)
                        {
                            result += "\r\n";
                        }
                    }
                    foundWords++;
                }
            }
            return HttpUtility.HtmlDecode(result);
        }

        /// <summary>For use in TranslateText returning URL.</summary>
        /// <param name="translateText">Text for translate.</param>
        /// <param name="sourceLanguage">From language.</param>
        /// <param name="targetLanguage">To language.</param>
        /// <returns>Returning URL for use in TranslateText.</returns>
        public static string GetDownloadLink(string translateText, string sourceLanguage, string targetLanguage)
        {
            return "http://www.google.com/translate_t?hl=en&ie=UTF8&oe=UTF8&text=" + HttpUtility.HtmlEncode(translateText) +
                "&langpair=" + sourceLanguage + "|" + targetLanguage; //translateText.Replace(Environment.NewLine, "%0A")
        }

        public static GTLanguage FindLanguage(string language, List<GTLanguage> languages)
        {
            foreach (GTLanguage gtlanguage in languages)
            {
                if (gtlanguage.Value == language)
                {
                    return gtlanguage;
                }
            }
            return null;
        }

        /// <summary>
        /// Represents current possible translation options of Google Translate
        /// </summary>
        public class Options
        {
            public List<GTLanguage> SourceLangList { get; set; }
            public List<GTLanguage> TargetLangList { get; set; }

            public Options()
            {
                this.SourceLangList = new List<GTLanguage>();
                this.TargetLangList = new List<GTLanguage>();
            }
        }

        /// <summary>
        /// Google Translate Language
        /// </summary>
        public class GTLanguage
        {
            /// <summary>
            /// Example: sr
            /// </summary>
            public string Value { get; set; }
            /// <summary>
            /// Example: Serbian
            /// </summary>
            public string Name { get; set; }

            public GTLanguage(string value, string name)
            {
                this.Value = value;
                this.Name = name;
            }

            public bool IsEmpty()
            {
                return string.IsNullOrEmpty(Value) || string.IsNullOrEmpty(Name);
            }
        }

        public struct ResultPacket
        {
            /// <summary>
            /// Translation Type as determined by Google 
            /// Example String: English (automatically detected) » Serbian
            /// </summary>
            public string TranslationType { get; set; }
            /// <summary>
            /// Translated Text
            /// </summary>
            public string TranslatedText { get; set; }
            public string Dictionary { get; set; }
        }

        public class TranslationInfo
        {
            public string SourceText { get; set; }
            public GTLanguage SourceLanguage { get; set; }
            public GTLanguage TargetLanguage { get; set; }
            public ResultPacket Result { get; set; }

            public TranslationInfo(string sourceText, GTLanguage sourceLanguage, GTLanguage targetLanguage)
            {
                this.SourceText = sourceText;
                this.SourceLanguage = sourceLanguage;
                this.TargetLanguage = targetLanguage;
            }

            public bool IsEmpty()
            {
                return string.IsNullOrEmpty(SourceText) || SourceLanguage.IsEmpty() || TargetLanguage.IsEmpty();
            }
        }
    }
}