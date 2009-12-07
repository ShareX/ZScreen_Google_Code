using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Collections;

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
                        Console.WriteLine(ex.ToString());
                    }
                }
            }

            return list;
        }
    }
}