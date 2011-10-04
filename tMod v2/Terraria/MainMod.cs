using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;
using System.Net;
using Microsoft.Xna.Framework;
using System.Threading;
using tMod_v3;
using tMod_v3.Events;

namespace Terraria
{
    public static partial class MainMod
    {
        public static string tModVersion = "3.1.5_1";
        public static bool IsServer = true;
        public static dynamic main;
        public static Configuration Config;
        public static Groups Groups;
        private static string ConfigFile = "Config.xml";
        public static event EventHandler Update;
        public static event ConfigLoadedEventHandler ConfigLoaded;

        public static void UpdateMod()
        {
            if (Update != null)
            {
                Update.Invoke(null, new EventArgs());
            }
        }

        static bool generating;
        public static void DedServ()
        {
            Console.Title = "tMod v3";
            if(Config.CheckForUpdates) CheckForUpdates();
            MainMod.LoadWorlds();
            if (!File.Exists(Config.WorldPath))
            {
                // god damned Terraria, I'll fix this next release.
                Console.WriteLine("Generating new world.");
                generating = true;
                ThreadPool.QueueUserWorkItem(delegate { CheckText(); });
                MainMod.WorldPathName = Config.WorldPath;
                //Console.Write("Specify world width (MIN 8400, MAX 2147483647 but it's veeeeery slow!): ");
                //string str = Console.ReadLine();
                //int width = 8400;
                //if (int.TryParse(str, out width) && width >= 8400)
                //{
                //    MainMod.MaxTilesX = width;
                //    if (width > short.MaxValue) Console.WriteLine("It's gonna take sooome time... Well, you have been warned!");
                //}
                //else
                //{
                //    Console.WriteLine("An error occured. World width set to 8400.");
                    MainMod.MaxTilesX = 8400;
                //}
                MainMod.MaxTilesY = 2400;
                MainMod.Tile = (dynamic)tMod.main.Assembly.GetType("Terraria.Tile").MakeArrayType(2).GetConstructor(new Type[] { typeof(int), typeof(int) }).Invoke(new object[] { MainMod.MaxTilesX, MainMod.MaxTilesY });
                tMod.main.GetField("bottomWorld").SetValue(null, MainMod.MaxTilesY * 0x10);
                tMod.main.GetField("rightWorld").SetValue(null, MainMod.MaxTilesX * 0x10);
                tMod.main.GetField("maxSectionsX").SetValue(null, MainMod.MaxTilesX / 200);
                tMod.main.GetField("maxSectionsY").SetValue(null, MainMod.MaxTilesY / 150);
                MainMod.DedServer = true;
                MainMod.ShowSplash = false;
                MainMod.Initialize();
                MainMod.MenuState = 10;
                WorldGenMod.SaveLock = false;
                MainMod.SkipMenu = false;
                WorldGenMod.CreateNewWorld();
                while (MainMod.MenuState == 10) { Thread.Sleep(200); } // workaround
                generating = false;
                Process.Start("tMod v3.exe");
                Environment.Exit(0);
            }
            MainMod.WorldPathName = Config.WorldPath;
            main.DedServ();
        }

        public static void CheckText()
        {
            string oldText = "";
            while (generating)
            {
                if (MainMod.StatusText != oldText)
                {
                    oldText = MainMod.StatusText;
                    Console.WriteLine(oldText);
                }
                Thread.Sleep(100);
            }
        }

        public static void StartDedInputMod()
        {
            Console.Clear();
            Console.Title = "tMod v" + tModVersion;
            Console.WriteLine("tMod v{0}", tModVersion);
            Console.WriteLine("http://tmod.biz/");
            Console.WriteLine("If you paid for this, get your money back, it's FREE!");
            Console.WriteLine();
            Console.WriteLine("Loading .NET Plugins...");
            Console.WriteLine();
            PluginManager.LoadPlugins(MainMod.Config);
            Console.WriteLine();
            Console.WriteLine("Loading Lua Plugins...");
            Console.WriteLine();
            LuaHandler.LuaInit();
            Console.WriteLine();
            Console.WriteLine("Done! tMod has started!");
            if (MainMod.Config.AsyncMode) Console.WriteLine("Running in Async mode!");
        }

        public static void CheckForUpdates()
        {
#if !DEBUG
            Console.WriteLine("Checking for updates...");
            Console.WriteLine("You can disable this in the configuration file! Set CheckForUpdates to false!");
            try
            {
                string version = GetText("http://tmod.biz/current_version.txt");
                if (version != tModVersion)
                {
                    Console.WriteLine("tMod is outdated! Downloading update...");
                    string[] files = GetText("http://tmod.biz/update/update_files.txt").Split(Environment.NewLine.ToCharArray());
                    foreach (string file in files)
                    {
                        if(file != "")
                        {
                            Console.WriteLine("[AutoUpdate] Downloading: {0}", file);
                            DownloadFile("http://tmod.biz/update/" + file, file);
                        }
                    }
                    Console.WriteLine("[AutoUpdate] Downloading updated executable...");
                    DownloadFile("http://tmod.biz/update/update_temp.exe", "update_temp.exe");
                    Process.Start("update_temp.exe");
                    Environment.Exit(0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to check for/download updates! {0}", e.Message);
            }
#endif
        }

        private static string GetText(string from)
        {
            Uri site = new Uri(from);
            WebRequest wReq = WebRequest.Create(site);
            wReq.Proxy = new WebProxy();
            using (WebResponse wResp = wReq.GetResponse())
            {
                Stream respStream = wResp.GetResponseStream();
                StreamReader reader = new StreamReader(respStream, Encoding.ASCII);
                return reader.ReadToEnd();
            }
        }

        private static void DownloadFile(string downloadFrom, string saveTo)
        {
            using (WebClient client = new WebClient())
            {
                client.Proxy = new WebProxy();
                client.DownloadFile(downloadFrom, saveTo);
            }
        }

        public static void Notice(string notice)
        {
            Console.WriteLine("[NOTICE] {0}", notice);
        }

        public static void Log(string notice)
        {
            Console.WriteLine("[LOG] {0}", notice);
        }

        public static void Notice(string notice, bool urgent)
        {
            Console.WriteLine("[NOTICE] {0}", notice);
        }

        public static bool IsOp(int player)
        {
            return Config.Ops.Contains(Session.Sessions[player].Username) && Session.Sessions[player].IsLoggedIn || Config.Ops.Contains(Session.Sessions[player].IpAddress);
        }

        public static bool IsMod(int player)
        {
            return Config.Mods.Contains(Session.Sessions[player].Username) && Session.Sessions[player].IsLoggedIn || Config.Mods.Contains(Session.Sessions[player].IpAddress);
        }

        public static bool HasPermission(int player, string command)
        {
            return (Session.Sessions[player].IsLoggedIn && Session.Sessions[player].Group.HasPermission(command)) || MainMod.Groups.Default.HasPermission(command);
        }

        public static void SaveConfig()
        {
            XmlSerializer xml = new XmlSerializer(typeof(Configuration));
            FileStream fs = File.Create(ConfigFile);
            xml.Serialize(fs, Config);
            fs.Flush();
            fs.Close();
            xml = new XmlSerializer(typeof(Groups));
            fs = File.Create("Groups.xml");
            xml.Serialize(fs, Groups);
            fs.Flush();
            fs.Close();
        }

        public static bool ServerModCommand(byte player, string text)
        {
            return ServerCommandHandler.ServerModCommand(player, text);
        }

        public static void LoadConfig(bool ConfigMode = false)
        {
#if !DEBUG
			if (File.Exists(ConfigFile))
			{
				XmlSerializer xml = new XmlSerializer(typeof(Configuration));
				FileStream fs = File.OpenRead(ConfigFile);
				Config = (Configuration)xml.Deserialize(fs);
				fs.Close();
			}
			else
			{
#endif
            Config = new Configuration();
#if !DEBUG
			}
#endif
            if (!File.Exists("Groups.xml"))
            {
                Groups = new Groups();
                Config.Ops.Add("127.0.0.1");
                Groups.Default.GroupPermissions.AddRange(new string[]
				{
					"/playing",
                    "/about",
					"/list",
					"/who",
					"/p",
					"/party",
					"/me",
					"/help",
                    "/motd",
                    "/login",
                    "/id",
                    "/identify",
                    "/register",
                    "/spawn",
                    "/getpos",
                    "/mypos",
				});
                Groups.Member.GroupPermissions.AddRange(new string[]
				{
                    "/opme",
                    "/changepw",
                    "/changepassword",
                    "/changelogin",
				});
                Groups.Mods.GroupPermissions.AddRange(new string[]
				{
                    "/mute",
                    "/unmute",
                    "/slience",
                    "/ban",
                    "/unban",
                    "/pandon",
					"/kick",
                    "/d",
                    "/drop",
                    "/i",
                    "/item",
                    "/give",
                    "/npc",
                    "/summon",
                    "/mob",
                    "/say",
                    "/broadcast",
                    "/b",
                    "/moderate",
                    "/allowbuild",
                    "/allowbreak",
                    "/allowbomb",
                    "/allowwater",
                    "/allowlava",
                    "/allowspike",
                    "/butcher",
                    "/butcherall",
                    "/kill",
                    "/heal",
                    "/hearts",
                    "/giveheart",
                    "/peace",
                    "/peaceful",
                    "/pvp",
                    "/allowregister",
                    "/day",
                    "/night",
                    "/dusk",
                    "/noon",
                    "/midnight",
                    "/whois",
                    "/ip",
                    "/rollback",
                    "/rban",
                    "/check",
				});
                Groups.Ops.GroupPermissions.Add("*");
                Group Donator = new Group();
                Donator.GroupName = "Donator";
                Groups.CustomGroups.Add(Donator);
            }
            else
            {
                Console.WriteLine("Deserializing Groups.xml...");
                try
                {
                    XmlSerializer xml = new XmlSerializer(typeof(Groups));
                    FileStream fs = File.OpenRead("Groups.xml");
                    Groups = (Groups)xml.Deserialize(fs);
                    
                    fs.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Groups] Problem deserializing Groups.xml, check it's valid.");
                    Console.WriteLine(ex.Message);
                }
            }
            if (!ConfigMode)
            {
                MainMod.motd = Config.MOTD;
                NetplayMod.ServerListenIP = IPAddress.Parse(Config.BindAddress);
                NetplayMod.ServerPort = Config.BindPort;
                NetplayMod.password = Config.ServerPassword;
                NPCMod.DefaultSpawnRate = NPCMod.SpawnRate = Config.SpawnRate;
                NPCMod.DefaultMaxSpawns = NPCMod.MaxSpawns = Config.MaximumSpawns;
            }
            if(!Groups.Default.GroupPermissions.Contains("/about"))
            {
                Console.WriteLine("/about HAS to be enabled, sorry.");
                Groups.Default.GroupPermissions.Add("/about");
            }
            
            if(!ConfigMode) LoadOldOps();
            SaveConfig();

            if (ConfigLoaded != null)
            {
                ConfigLoaded.Invoke(null, new ConfigLoadedEventArgs(ConfigFile));
            }
        }

        private static void LoadOldOps()
        {
            if (File.Exists(SavePath + @"\ops.txt"))
            {
                Config.Ops.Clear();
                foreach (string o in File.ReadAllLines(SavePath + @"\ops.txt"))
                {
                    Config.Ops.Add(o);
                    Config.Mods.Remove(o);
                }
                File.Delete(SavePath + @"\ops.txt");
            }
        }

        public static void SetTime(bool dayTime, int time)
        {
            MainMod.DayTime = dayTime;
            MainMod.Time = time;
            NetMessageMod.SendData((int)PacketTypes.UpdateTime, -1, -1, "", 0, 0, MainMod.SunModY, MainMod.MoonModY);
            NetMessageMod.SyncPlayers();
        }

        public static int GetPlayer(string player)
        {
            player = player.ToLower();
            for (int i = 0; i < MainMod.Player.Length; i++)
            {
                if (MainMod.Player[i].name.ToLower() == player || player == "-" && string.IsNullOrWhiteSpace(MainMod.Player[i].name))
                {
                    return i;
                }
            }
            for (int i = 0; i < MainMod.Player.Length; i++)
            {
                if (MainMod.Player[i].name.ToLower().Contains(player))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}