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
using System.Xml.Linq;
using UploadersLib;

namespace UploadersLib.TextServices
{
    public class Lyricsfly : Uploader
    {
        private const string UserID = "72abfc2bceb022534-temporary.API.access";

        /// <summary>
        /// To search by artist and title combination
        /// </summary>
        public Lyric SearchLyrics(string artist, string title)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add("i", UserID);
            arguments.Add("a", FixText(artist));
            arguments.Add("t", FixText(title));
            string response = GetResponseString("http://lyricsfly.com/api/api.php", arguments);
            return ParseResponse(response);
        }

        /// <summary>
        /// To search by lyrics text string
        /// </summary>
        public Lyric SearchLyrics(string text)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add("i", UserID);
            arguments.Add("l", FixText(text));
            string response = GetResponseString("http://lyricsfly.com/api/txt-api.php", arguments);
            return ParseResponse(response);
        }

        private Lyric ParseResponse(string response)
        {
            if (!string.IsNullOrEmpty(response))
            {
                XDocument xdoc = XDocument.Parse(response);
                XElement xele = xdoc.Element("start");

                if (xele != null)
                {
                    string status = xele.ElementValue("status");
                    if (!string.IsNullOrEmpty(status))
                    {
                        switch (status)
                        {
                            case "200": // OK - Results are returned. All parameters checked ok.
                            case "300": // TESTING LIMITED - Temporary access. Limited content. All parameters checked ok.
                                xele = xele.Element("sg");
                                if (xele != null)
                                {
                                    Lyric lyric = new Lyric();
                                    lyric.Checksum = xele.ElementValue("cs");
                                    lyric.SongID = xele.ElementValue("id");
                                    lyric.ArtistName = xele.ElementValue("ar");
                                    lyric.Title = xele.ElementValue("tt");
                                    lyric.AlbumName = xele.ElementValue("al");
                                    lyric.Text = xele.ElementValue("tx").Replace("[br]", string.Empty);

                                    return lyric;
                                }
                                break;
                            case "204": // NO CONTENT
                                throw new Exception("Parameter query returned no results. All parameters checked ok.");
                            case "400": // MISSING KEY
                                throw new Exception("Parameter “i” missing. Authorization failed.");
                            case "401": // UNAUTHORIZED
                                throw new Exception("Parameter “i” invalid. Authorization failed.");
                            case "402": // LIMITED TIME
                                throw new Exception("Query request too soon. Limit query requests. Time of delay is shown in <delay> tag in milliseconds.");
                            case "406": // QUERY TOO SHORT
                                throw new Exception("Query request string is too short. All other parameters checked ok.");
                            default:
                                throw new Exception("Unknown status.");
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Because our database varies with many html format encodings including international characters,
        /// we recommend that you replace all quotes, ampersands and all other special and international characters with "%".
        /// Simply put; if the character is not [A-Z a-z 0-9] or space, just substitute "%" for it to get most out of your results.
        /// </summary>
        private string FixText(string text)
        {
            char[] chars = text.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (!Char.IsLetterOrDigit(chars[i]) && chars[i] != ' ')
                {
                    chars[i] = '%';
                }
            }

            return new string(chars);
        }
    }

    public class Lyric
    {
        /// <summary>
        /// cs - checksum (for original URL link back construction)
        /// </summary>
        public string Checksum { get; set; }

        /// <summary>
        /// id - song ID in the database (for original URL link back construction)
        /// </summary>
        public string SongID { get; set; }

        /// <summary>
        /// ar - artist name
        /// </summary>
        public string ArtistName { get; set; }

        /// <summary>
        /// tt - title of the song
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// al - album name
        /// </summary>
        public string AlbumName { get; set; }

        /// <summary>
        /// tx - lyrics text separated by [br] for line break
        /// </summary>
        public string Text { get; set; }
    }
}