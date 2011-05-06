using System;
using System.Reflection;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Custom_Controls;

namespace HistoryLib.Custom_Controls
{
    public class ObjectListView : MyListView
    {
        public enum ObjectType { Fields, Properties }

        public ObjectType SetObjectType { get; set; }

        public object SelectedObject
        {
            set
            {
                SelectObject(value);
            }
        }

        public ObjectListView()
        {
            this.SetObjectType = ObjectType.Properties;
            this.MultiSelect = false;
            this.Columns.Add("Name", 125);
            this.Columns.Add("Value", 300);
            ContextMenu contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add("Copy name").Click += new EventHandler(PropertyListView_Click_Name);
            contextMenu.MenuItems.Add("Copy value").Click += new EventHandler(PropertyListView_Click_Value);
            this.ContextMenu = contextMenu;
        }

        private void PropertyListView_Click_Name(object sender, EventArgs e)
        {
            if (this.SelectedItems.Count > 0)
            {
                string text = this.SelectedItems[0].Text;
                if (!string.IsNullOrEmpty(text))
                {
                    Helpers.CopyTextSafely(text);
                }
            }
        }

        private void PropertyListView_Click_Value(object sender, EventArgs e)
        {
            if (this.SelectedItems.Count > 0)
            {
                string text = this.SelectedItems[0].SubItems[1].Text;
                if (!string.IsNullOrEmpty(text))
                {
                    Helpers.CopyTextSafely(text);
                }
            }
        }

        public void SelectObject(object obj)
        {
            this.Items.Clear();

            if (obj != null)
            {
                Type type = obj.GetType();

                if (SetObjectType == ObjectType.Fields)
                {
                    foreach (FieldInfo property in type.GetFields())
                    {
                        AddObject(property.GetValue(obj), property.Name);
                    }
                }
                else if (SetObjectType == ObjectType.Properties)
                {
                    foreach (PropertyInfo property in type.GetProperties())
                    {
                        AddObject(property.GetValue(obj, null), property.Name);
                    }
                }

                FillLastColumn();
            }
        }

        private void AddObject(object obj, string name)
        {
            if (obj is HistoryItem)
            {
                SelectObject(obj);
                return;
            }

            ListViewItem lvi = new ListViewItem(name);
            lvi.Tag = obj;
            if (obj != null)
            {
                lvi.SubItems.Add(obj.ToString());
            }

            this.Items.Add(lvi);
        }
    }
}