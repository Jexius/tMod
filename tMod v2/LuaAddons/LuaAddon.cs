using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using tMod_v3.Events;

namespace tMod_v3
{
    public class LuaAddon
    {
        public static List<LuaAddon> addons = new List<LuaAddon>();
        public string AddonDir;

        public LuaAddon(string AddonDir)
        {
            this.AddonDir = AddonDir;
            addons.Add(this);
        }

        public void Run()
        {
            LuaHandler.Include("shared.lua");
        }

        public static void CallEvent(object sender, PacketReceivedEventArgs args)
        {
            try
            {
                LuaHandler.lua.GetFunction("hook.Call").Call(new object[] { args.PacketType.ToString(), args });
            }
            catch (Exception e)
            {
                Console.WriteLine("[Lua Error] {0}", e.Message);
            }
        }
    }
}
