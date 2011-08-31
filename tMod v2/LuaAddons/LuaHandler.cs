using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using System.IO;
using LuaInterface;
using Terraria;

namespace tMod_v3
{
    public class LuaHandler
    {
        public static Lua lua;
        public static LuaAddon ActiveAddon;
        public static LuaHandler instance;

        public static void LuaInit(bool reload = false)
        {
            instance = new LuaHandler();
            lua = new Lua();
            if(!reload) MessageBufferMod.LuaInit();
            RegisterFunctions();
            RunCoreLua();
            if (MainMod.IsServer)
            {
                lua["SERVER"] = true;
                lua["CLIENT"] = false;
            }
            else
            {
                lua["SERVER"] = false;
                lua["CLIENT"] = true;
            }
            SandboxLua();
            LoadAddons();
        }

        public static void LoadAddons()
        {
            if (!Directory.Exists(@"lua\addons"))
            {
                Directory.CreateDirectory(@"lua\addons");
            }
            foreach (string dir in Directory.GetDirectories(@"lua\addons"))
            {
                if (File.Exists(dir + "\\shared.lua"))
                {
                    ActiveAddon = new LuaAddon(dir + "\\");
                    ActiveAddon.Run();
                }
                else
                {
                    Console.WriteLine("[Lua] Warning: No shared.lua found in {0}", dir);
                }
            }
            ActiveAddon = null;
        }

        public static void SandboxLua()
        {
            lua["luanet"] = null;
            lua["os"] = null;
            lua["require"] = null;
            lua["dofile"] = null;
        }

        public static void RegisterFunctions()
        {
            lua.RegisterFunction("AddCSLuaFile", instance, instance.GetType().GetMethod("AddCSLuaFile"));
            lua.RegisterFunction("include", instance, instance.GetType().GetMethod("Include"));
            lua.RegisterFunction("print", instance, instance.GetType().GetMethod("LuaPrint"));
            lua.RegisterFunction("GetPlayer", instance, instance.GetType().GetMethod("GetPlayer"));
            lua.RegisterFunction("tostring", instance, instance.GetType().GetMethod("toString"));
            lua.RegisterFunction("GetSession", instance, instance.GetType().GetMethod("GetSession"));
        }

        public static void ReloadLua()
        {
            Console.WriteLine("[LuaHandler] Reloading Lua...");
            lua = null;
            LuaAddon.addons = new List<LuaAddon>();
            LuaInit(true);
        }

        public static void RunCoreLua()
        {
            lua.DoString("luanet.load_assembly 'System';");
            lua.DoString("luanet.load_assembly '" + System.AppDomain.CurrentDomain.FriendlyName + "';");
            lua.DoString("Math = luanet.import_type('System.Math');");
            lua.DoString("Session = luanet.import_type('Terraria.Session');");
            lua.DoString("NetMessageMod = luanet.import_type('Terraria.NetMessageMod');");
            lua.DoString("MainMod = luanet.import_type('Terraria.MainMod');");
            lua.DoString("NPCMod = luanet.import_type('Terraria.NPCMod');");
            lua.DoString("MessageBufferMod = luanet.import_type('Terraria.MessageBufferMod');");
            lua.DoString("NetplayMod = luanet.import_type('Terraria.NetplayMod');");
            lua.DoString("WorldGenMod = luanet.import_type('Terraria.WorldGenMod');");
            lua.DoString("Database = luanet.import_type('tMod_v3.Database');");
            lua["General"] = new General();
            lua.DoFile(@"lua\core\hook.lua");
        }

        public Session GetSession(int Player)
        {
            return Session.Sessions[Player];
        }

        public Session GetPlayer(string Player)
        {
            try
            {
                return Session.Sessions[MainMod.GetPlayer(Player)];
            }
            catch (Exception e)
            {
                Console.WriteLine("[WARNING] Problem fetching player!");
            }
            return null;
        }

        public static void LuaPrint(string toprint) // Workaround since print wasn't working :s
        {
            Console.WriteLine(toprint);
        }

        public string toString(object obj)
        {
            return obj.ToString();
        }

        public static void Include(string file)
        {
            try
            {
                if (ActiveAddon == null)
                {
                    Console.WriteLine("[Lua] Warning: Tried including file after initial run, don't!");
                }
                else
                {
                    lua.DoFile(ActiveAddon.AddonDir + file);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[Lua Error] {0}", e.Message);
            }
        }

        public void AddCSLuaFile(string luaFile)
        {
            Console.WriteLine("[LUA WARNING] What are you doing? There's no client mod yet to send these files to!");
        }
    }
}
