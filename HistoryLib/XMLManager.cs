using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using HelpersLib;

namespace HistoryLib
{
    internal class XMLManager
    {
        private string xmlPath;

        public XMLManager(string xmlFilePath)
        {
            xmlPath = xmlFilePath;
        }

        public List<HistoryItem> Load()
        {
            List<HistoryItem> historyItemList = new List<HistoryItem>();

            if (!string.IsNullOrEmpty(xmlPath) && File.Exists(xmlPath))
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(xmlPath);

                // XmlNodeList historyItems = xd.GetElementsByTagName("HistoryItem");
                // XmlNodeList historyItems = xd.SelectSingleNode("HistoryItems").ChildNodes;
                XmlNodeList historyItems = xd.ChildNodes[1].ChildNodes;

                HistoryItem hi;

                foreach (XmlNode historyItem in historyItems)
                {
                    hi = ParseHistoryItem(historyItem.ChildNodes);

                    if (hi != null) historyItemList.Add(hi);
                }
            }

            return historyItemList;
        }

        private HistoryItem ParseHistoryItem(XmlNodeList nodes)
        {
            HistoryItem hi = new HistoryItem();

            foreach (XmlNode node in nodes)
            {
                switch (node.Name)
                {
                    case "Filename":
                        hi.Filename = node.InnerText;
                        break;
                    case "Filepath":
                        hi.Filepath = node.InnerText;
                        break;
                    case "URL":
                        hi.URL = node.InnerText;
                        break;
                    case "ThumbnailURL":
                        hi.ThumbnailURL = node.InnerText;
                        break;
                    case "DeletionURL":
                        hi.DeletionURL = node.InnerText;
                        break;
                    case "TinyURL":
                        hi.TinyURL = node.InnerText;
                        break;
                    default:
                        Debug.WriteLine("Unknown node: " + node.Name);
                        break;
                }
            }

            Debug.WriteLineIf(string.IsNullOrEmpty(hi.URL), "URL is empty: " + hi.Filename);

            return hi;
        }

        public bool AddHistoryItem(HistoryItem historyItem)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlPath);

            if (xml.ChildNodes.Count == 0)
            {
                xml.AppendChild(xml.CreateXmlDeclaration("1.0", "UTF-8", null));
                xml.AppendElement("HistoryItems");
            }

            XmlNode rootNode = xml.ChildNodes[1];
            XmlNode historyItemNode = rootNode.PrependElement("HistoryItem");
            historyItemNode.AppendElement("Filename", historyItem.Filename);
            historyItemNode.AppendElement("Filepath", historyItem.Filepath);
            historyItemNode.AppendElement("DateTimeUtc", historyItem.DateTimeUtc.ToString());
            historyItemNode.AppendElement("Type", historyItem.Type);
            historyItemNode.AppendElement("Host", historyItem.Host);
            historyItemNode.AppendElement("URL", historyItem.URL);
            historyItemNode.AppendElement("ThumbnailURL", historyItem.ThumbnailURL);
            historyItemNode.AppendElement("DeletionURL", historyItem.DeletionURL);
            historyItemNode.AppendElement("TinyURL", historyItem.TinyURL);

            xml.Save(xmlPath);

            return true;
        }
    }
}