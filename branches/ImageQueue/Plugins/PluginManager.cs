using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace Plugins
{
    public static class PluginManager
    {
        public static List<T> LoadPlugins<T>(string path)
        {
            List<T> list = new List<T>();

            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path, "*.dll");

                foreach (string file in files)
                {
                    try
                    {
                        Assembly assembly = Assembly.LoadFile(file);

                        foreach (Type type in assembly.GetTypes())
                        {
                            if (!type.IsClass || type.IsNotPublic) continue;

                            Type[] interfaces = type.GetInterfaces();

                            if (((IList)interfaces).Contains(typeof(T)))
                            {
                                T obj = (T)Activator.CreateInstance(type);
                                list.Add(obj);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                    }
                }
            }

            return list;
        }

        public static Image ApplyEffects(IPluginItem[] plugins, Image img)
        {
            foreach (IPluginItem plugin in plugins)
            {
                img = plugin.ApplyEffect(img);
            }

            return img;
        }
    }
}