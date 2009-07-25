using System;
using System.IO;
using System.Text;

namespace ZSS.IndexersLib
{
    public static class HTMLHelper
    {
        public enum ListType
        {
            Numbered,
            Bulletted
        }

        public static string GetDocType()
        {
            return IndexerAdapter.GetText("html.txt");
        }

        public static string GetValidXhtmlLine(string line)
        {
            if (line != null)
            {
                line = line.Replace("&", "&amp;");
                line = line.Replace("™", "&trade;");
                line = line.Replace("©", "&copy;");
                line = line.Replace("®", "&reg;");
            }

            return line;
        }

        public static string GetJavaScript(string filePath)
        {
            StringBuilder sb = new StringBuilder();
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
                sb.AppendLine(IndexerAdapter.GetText("domCollapse.js"));
            }

            sb.AppendLine("// ]]>");
            sb.AppendLine("</script>");

            return sb.ToString();
        }

        public static string GetCollapseJs()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine("// <![CDATA[");
            sb.AppendLine(IndexerAdapter.GetText("domCollapse.js"));
            sb.AppendLine("// ]]>");
            sb.AppendLine("</script>");

            return sb.ToString();
        }

        public static string GetCollapseCss()
        {
            StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("<style type=\"text/css\">");
            sb.AppendLine(IndexerAdapter.GetText("domCollapse.css"));
            sb.AppendLine("</style>");


            return sb.ToString();
        }

        public static string GetCssStyle(string filePath)
        {
            StringBuilder sb = new StringBuilder();
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
                sb.AppendLine(IndexerAdapter.GetText("Default.css"));
            }
            sb.AppendLine("</style>");

            return sb.ToString();
        }

        public static string MakeAnchor(string url, string text)
        {
            return string.Format("<a href=\"{0}\">{1}</a>", url, text);
        }

        public static string OpenDiv()
        {
            return "<div>";
        }

        public static string CloseDiv()
        {
            return "</div>";
        }

        public static string OpenBulletedList()
        {
            return "<ul>";
        }

        public static string CloseBulletedList()
        {
            return "</ul>";
        }

        public static string OpenNumberedList()
        {
            return "<ol>";
        }

        public static string CloseNumberedList()
        {
            return "</ol>";
        }

        public static string GetTitle(string title)
        {
            return "<title>" + title + "</title>";
        }

        public static string GetSpan(string lText, string lClass)
        {
            return "<span class=\"" + lClass + "\">" + lText + "</span>";
        }
        public static string CloseHead()
        {
            return "</head>";
        }

        public static string OpenBody()
        {
            return "<body>";
        }

        public static string CloseBody()
        {
            return "</body></html>";
        }

        public static string AddBreak()
        {
            return "<br />";
        }

        public static string GetPara(string msg)
        {
            return OpenPara("") + GetValidXhtmlLine(msg) + ClosePara();
        }

        public static string GetBreak()
        {
            return OpenPara("") + AddBreak() + ClosePara();
        }

        public static string OpenPara(string span)
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

        public static string ClosePara()
        {
            return "</p>";
        }

        public static string GetWarning(string msg)
        {
            return "<p class=\"warning\">" + GetValidXhtmlLine(msg) + ClosePara();
        }

        public static string GetList(string msg)
        {
            return "<li>" + GetValidXhtmlLine(msg) + "</li>";
        }

        public static string GetHeading(string msg, int order)
        {
            return string.Format("<h{0}>{1}</h{0}>", order, GetValidXhtmlLine(msg));
        }

        public static string OpenList(ListType type)
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

        public static string CloseList(ListType type)
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