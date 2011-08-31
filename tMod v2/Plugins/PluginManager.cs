using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Threading;
using tMod_v3.Events;
using Terraria;

namespace tMod_v3
{
    public static class PluginManager
    {
        private static Configuration Config;
        private static List<Plugin> Plugins = new List<Plugin>();

        public static void LoadPlugins(Configuration config)
        {
            Config = config;
            if (!Directory.Exists(config.PluginDirectory))
            {
                Directory.CreateDirectory(config.PluginDirectory);
            }
            if (!Directory.Exists(config.PluginDataDirectory))
            {
                Directory.CreateDirectory(config.PluginDataDirectory);
            }
            foreach (string p in config.Plugins)
            {
                try
                {
                    LoadPlugin(p);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[PluginManager] Problem loading plugin: {0}", p);
                    Console.WriteLine("[PluginManager] Dumped error in PluginManagerException.txt");
                    Console.WriteLine("[PluginManager] Make sure you marked it as safe! Instructions at http://tmod.biz/forum/");
                    using (StreamWriter writer = new StreamWriter("PluginManagerException.txt", true))
                    {
                        writer.WriteLine(DateTime.Now);
                        writer.WriteLine(ex);
                        writer.WriteLine("");
                    }
                }
            }
        }

        public static Plugin[] GetPlugins()
        {
            return Plugins.ToArray();
        }

        public static Plugin GetPlugin(string pluginName)
        {
            foreach (Plugin plugin in Plugins)
            {
                if (plugin.Name.Equals(pluginName))
                {
                    return plugin;
                }
            }
            return null; // so I don't have to add an extra loop to see if it's loaded.
        }

        public static void LoadPlugin(string pluginName)
        {
            byte[] assembly = File.ReadAllBytes(Config.PluginDirectory + @"\" + pluginName + (pluginName.EndsWith(".dll") ? "" : ".dll"));
            Assembly asm = Assembly.Load(assembly);
            foreach (Type t in asm.GetTypes())
            {
                if (t.BaseType == typeof(Plugin) && t.GetConstructor(new Type[0]) != null)
                {
                    Thread tr = new Thread(LoadPlugin);
                    tr.Start(t.GetConstructor(new Type[0]).Invoke(new object[0]));
                }
            }
        }

        public static bool PluginExists(string pluginName)
        {
            return File.Exists(Config.PluginDirectory + @"\" + pluginName + (pluginName.EndsWith(".dll") ? "" : ".dll"));
        }

        public static void LoadPlugin(object pl)
        {
            Plugin plugin = (Plugin)pl;
            Plugins.Add(plugin);
            plugin.OnLoaded(null, new EventArgs());
            Console.WriteLine("Loaded plugin: " + plugin.Name + " v" + plugin.Version + " by " + plugin.Author, true);
        }

        public static void UnloadPlugin(Plugin plugin)
        {
            Plugins.Remove(plugin);
            
            plugin.OnUnloaded(null, new EventArgs());
            Console.WriteLine("Unloaded plugin: " + plugin.Name + " v" + plugin.Version + " by " + plugin.Author, true);
        }

        public static void Reload()
        {
            foreach (Plugin p in Plugins)
            {
                p.Reload();
            }
        }
    }

}
