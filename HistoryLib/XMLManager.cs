using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using HelpersLib;

namespace HistoryLib
{
    internal class XMLManager
    {
        private static object thisLock = new object();

        private string xmlPath;

        public XMLManager(string xmlFilePath)
        {
            xmlPath = xmlFilePath;
        }

        public List<HistoryItem> Load()
        {
            List<HistoryItem> historyItemList = new List<HistoryItem>();

            lock (thisLock)
            {
                if (!string.IsNullOrEmpty(xmlPath) && File.Exists(xmlPath))
                {
                    XmlDocument xml = new XmlDocument();
                    xml.Load(xmlPath);

                    XmlNode rootNode = xml.ChildNodes[1];

                    if (rootNode.Name == "HistoryItems" && rootNode.ChildNodes != null && rootNode.ChildNodes.Count > 0)
                    {
                        HistoryItem hi;

                        foreach (XmlNode historyItem in rootNode.ChildNodes)
                        {
                            hi = ParseHistoryItem(historyItem.ChildNodes);
                            historyItemList.Add(hi);
                        }
                    }
                }
            }

            return historyItemList;
        }

        private HistoryItem ParseHistoryItem(XmlNodeList nodes)
        {
            HistoryItem hi = new HistoryItem();

            foreach (XmlNode node in nodes)
            {
                if (node == null || string.IsNullOrEmpty(node.InnerText)) continue;

                switch (node.Name)
                {
                    case "Filename":
                        hi.Filename = node.InnerText;
                        break;
                    case "Filepath":
                        hi.Filepath = node.InnerText;
                        break;
                    case "DateTimeUtc":
                        hi.DateTimeUtc = DateTime.Parse(node.InnerText);
                        break;
                    case "Type":
                        hi.Type = node.InnerText;
                        break;
                    case "Host":
                        hi.Host = node.InnerText;
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
                    case "ShortenedURL":
                        hi.ShortenedURL = node.InnerText;
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
            if (!string.IsNullOrEmpty(xmlPath))
            {
                lock (thisLock)
                {
                    XmlDocument xml = new XmlDocument();

                    if (File.Exists(xmlPath))
                    {
                        xml.Load(xmlPath);
                    }

                    if (xml.ChildNodes.Count == 0)
                    {
                        xml.AppendChild(xml.CreateXmlDeclaration("1.0", "UTF-8", null));
                        xml.AppendElement("HistoryItems");
                    }

                    XmlNode rootNode = xml.ChildNodes[1];

                    if (rootNode.Name == "HistoryItems")
                    {
                        XmlNode historyItemNode = rootNode.PrependElement("HistoryItem");
                        historyItemNode.AppendElement("Filename", historyItem.Filename);
                        historyItemNode.AppendElement("Filepath", historyItem.Filepath);
                        historyItemNode.AppendElement("DateTimeUtc", historyItem.DateTimeUtc.ToString("o"));
                        historyItemNode.AppendElement("Type", historyItem.Type);
                        historyItemNode.AppendElement("Host", historyItem.Host);
                        historyItemNode.AppendElement("URL", historyItem.URL);
                        historyItemNode.AppendElement("ThumbnailURL", historyItem.ThumbnailURL);
                        historyItemNode.AppendElement("DeletionURL", historyItem.DeletionURL);
                        historyItemNode.AppendElement("ShortenedURL", historyItem.ShortenedURL);

                        xml.Save(xmlPath);

                        return true;
                    }
                }
            }

            return false;
        }

        public bool RemoveHistoryItemByIndex(int index) // TODO: History Remove
        {
            lock (thisLock)
            {
                if (!string.IsNullOrEmpty(xmlPath) && File.Exists(xmlPath))
                {
                    XmlDocument xml = new XmlDocument();
                    xml.Load(xmlPath);

                    XmlNode rootNode = xml.ChildNodes[1];

                    if (rootNode.Name == "HistoryItems" && rootNode.ChildNodes != null &&
                        rootNode.ChildNodes.Count > 0 && index.IsBetween(0, rootNode.ChildNodes.Count))
                    {
                        rootNode.RemoveChild(rootNode.ChildNodes[index]);
                        xml.Save(xmlPath);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}