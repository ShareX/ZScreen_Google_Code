using System.IO;
using System;

// Version 1.0.0.0
namespace ZSS.IndexersLib
{
    public class HtmlHelper
    {
        public enum ListType
        {
            Numbered,
            Bulletted
        }

        public string GetDocType()
        {
            return GetText("html.txt");
        }

        public string GetValidXhtmlLine(string line)
        {

            if (line != null)
            {

                line = line.Replace("&", "&amp;");
                //Replace & 
                line = line.Replace("™", "&trade;");
                // Replace ™
                line = line.Replace("©", "&copy;");
                // Replace ©
                // Replace ®
                line = line.Replace("®", "&reg;");
            }



            return line;
        }


        public string GetJavaScript(string filePath)
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine("// <![CDATA[");

            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    sb.AppendLine(sr.ReadToEnd());
                }
            }
            else
            {
                sb.AppendLine(GetText("domCollapse.js"));
            }

            sb.AppendLine("// ]]>");
            sb.AppendLine("</script>");


            return sb.ToString();
        }

        public string GetCollapseJs()
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine("// <![CDATA[");
            sb.AppendLine(GetText("domCollapse.js"));
            sb.AppendLine("// ]]>");
            sb.AppendLine("</script>");


            return sb.ToString();
        }


        public string GetCollapseCss()
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("<style type=\"text/css\">");
            sb.AppendLine(GetText("domCollapse.css"));
            sb.AppendLine("</style>");


            return sb.ToString();
        }

        public string GetCssStyle(string filePath)
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("<style type=\"text/css\">");
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    sb.AppendLine(sr.ReadToEnd());
                }
            }
            else
            {
                sb.AppendLine(GetText("Default.css"));
            }
            sb.AppendLine("</style>");


            return sb.ToString();
        }

        public string OpenDiv()
        {
            return "<div>";
        }

        public string CloseDiv()
        {
            return "</div>";
        }

        public string OpenBulletedList()
        {
            return "<ul>";
        }

        public string CloseBulletedList()
        {
            return "</ul>";
        }

        public string OpenNumberedList()
        {
            return "<ol>";
        }


        public string CloseNumberedList()
        {
            return "</ol>";
        }

        public string GetTitle(string title)
        {
            return "<title>" + title + "</title>";
        }

        public string GetSpan(string lText, string lClass)
        {
            return "<span class=\"" + lClass + "\">" + lText + "</span>";
        }
        public string CloseHead()
        {
            return "</head>";
        }

        public string OpenBody()
        {
            return "<body>";
        }

        public string CloseBody()
        {
            return "</body></html>";
        }

        public string AddBreak()
        {
            return "<br />";
        }

        public string GetPara(string msg)
        {
            return OpenPara("") + GetValidXhtmlLine(msg) + ClosePara();
        }


        public string GetBreak()
        {
            return OpenPara("") + AddBreak() + ClosePara();
        }

        public string OpenPara(string span)
        {
            if (span.Length > 0)
            {
                return string.Format("<p class=\"{0}\">", span);
            }
            else
            {
                return "<p>";
            }
        }

        public string ClosePara()
        {
            return "</p>";
        }

        public string GetWarning(string msg)
        {
            return "<p class=\"warning\">" + GetValidXhtmlLine(msg) + ClosePara();
        }

        public static string GetText(string name)
        {
            string text = "";
            try
            {
                System.Reflection.Assembly oAsm = System.Reflection.Assembly.GetExecutingAssembly();

                string fn = "";
                foreach (string n in oAsm.GetManifestResourceNames())
                {
                    if (n.Contains(name))
                    {
                        fn = n;
                        break;
                    }
                }
                Stream oStrm = oAsm.GetManifestResourceStream(fn);
                StreamReader oRdr = new StreamReader(oStrm);
                text = oRdr.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return text;
        }

        public string GetList(string msg)
        {
            return "<li>" + GetValidXhtmlLine(msg) + "</li>";
        }

        public string GetHeading(string msg, int order)
        {


            return string.Format("<h{0}>{1}</h{0}>", order, GetValidXhtmlLine(msg));
        }

        public string OpenList(ListType type)
        {

            switch (type)
            {
                case ListType.Bulletted:
                    return OpenBulletedList();
                case ListType.Numbered:
                    return OpenNumberedList();
                default:
                    return OpenNumberedList();

            }
        }

        public string CloseList(ListType type)
        {

            switch (type)
            {
                case ListType.Bulletted:
                    return CloseBulletedList();
                case ListType.Numbered:
                    return CloseNumberedList();
                default:
                    return CloseNumberedList();

            }
        }
    }
}

