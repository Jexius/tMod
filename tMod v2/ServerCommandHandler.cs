using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using Terraria;
using tMod_v3.Events;
using System.IO;

namespace tMod_v3
{
    public static class ServerCommandHandler
    {
        public static event CommandReceivedEventHandler PreCommandReceived;
        public static event CommandReceivedEventHandler CommandReceived;

        public static bool ServerModCommand(int player, string text)
        {
            if (text.StartsWith("/"))
            {
                string[] parts = text.Split(new char[] { ' ' }, 3);
                string cmd = parts[0].ToLower();
                if (!MainMod.HasPermission(player, cmd))
                {
                    if (Session.Sessions[player].IsLoggedIn)
                    {
                        Session.Sessions[player].SendError("You do not have permission for this command.");
                    }
                    else if (Database.IsUserRegistered(MainMod.Player[player].name))
                    {
                        Session.Sessions[player].SendError("Try logging in before using this command");
                    }
                    else
                    {
                        Session.Sessions[player].SendError("You do not have permission for this command.");
                    }
                }
                else
                {
                    if (PreCommandReceived != null)
                    {
                        CommandReceivedEventArgs e = new CommandReceivedEventArgs(player, text);
                        PreCommandReceived.Invoke(null, e);
                        if (e.Canceled)
                        {
                            return true;
                        }
                    }
                    if (CommandReceived != null)
                    {
                        CommandReceivedEventArgs e = new CommandReceivedEventArgs(player, text);
                        CommandReceived.Invoke(null, e);
                        if (e.Canceled)
                        {
                            return true;
                        }
                    }

                    //MainMod.Notice("Command: " + MainMod.Player[player].name + ": " + text);

                    switch (cmd)
                    {
                        case "/tp":
                        case "/tele":
                        case "/goto":
                            if (parts.Length > 1)
                            {
                                OnTeleCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;
                        
                        case "/opme":
                            if (parts.Length > 1)
                            {
                                OnOpMeCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error. Usage: /opme {password}");
                            }
                            break;

                        case "/tphere":
                        case "/telehere":
                        case "/bring":
                            if (parts.Length > 1)
                            {
                                OnTeleHereCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/playing":
                        case "/list":
                        case "/who":
                            OnListCommand(player);
                            break;

                        case "/motd":
                            if (parts.Length > 1)
                            {
                                OnMOTDCommand(player, parts[1]);
                            }
                            else
                            {
                                OnMOTDCommand(player);
                            }
                            break;

                        case "/warp":
                            if (parts.Length > 1)
                            {
                                OnWarpCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax error.");
                            }
                            break;

                        case "/setwarp":
                            if (parts.Length > 1)
                            {
                                OnSetWarpCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax error.");
                            }
                            break;

                        case "/p":
                        case "/party":
                            if (parts.Length > 1)
                            {
                                OnPartyCommand(player, text.Replace(parts[0], ""));
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/easteregg":
                            Session.Sessions[player].SendError("Oh come on, that's too obvious! Try again! You're close though!");
                            break;

                        case "/easteregg2":
                            Session.Sessions[player].SendText("That's it! Pinkie Pie would be proud of you if she were real!");
                            break;

                        case "/setgroup":
                            Console.WriteLine(parts.Length);
                            if (parts.Length > 2)
                            {
                                OnSetGroupCommand(player, parts[1], parts[2]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/w":
                        case "/whisper":
                        case "/tell":
                            if (parts.Length > 1)
                            {
                                OnWhisperCommand(player, text.Replace(parts[0], ""));
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/reply":
                        case "/r":
                            if (parts.Length > 1)
                            {
                                OnReplyCommand(player, text.Replace(parts[0], "")); // ... that or have an if statement, or have one command >_>
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/me":
                            if (parts.Length > 1)
                            {
                                OnMeCommand(player, text.Substring(4));
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/mute":
                        case "/silence":
                            if (parts.Length > 1)
                            {
                                OnMuteCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/kick":
                            if (parts.Length > 1)
                            {
                                OnKickCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/butterfingers": // Thank you Dinnerbone and script-o-matic.net for this idea ;D
                            if (parts.Length > 1)
                            {
                                OnButterfingersCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/stupidize": // Thank you Dinnerbone and script-o-matic.net for this idea ;D
                            if (parts.Length > 1)
                            {
                                OnDumbCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/ban":
                            if (parts.Length > 1)
                            {
                                OnBanCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/about":
                            Session.Sessions[player].SendText("Server is powered by tMod v" + MainMod.tModVersion);
                            break;

                        case "/reloadlua":
                            LuaHandler.ReloadLua();
                            break;

                        case "/pardon":
                        case "/unban":
                            if (parts.Length > 1)
                            {
                                OnUnBanCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/d":
                        case "/drop":
                        case "/item":
                        case "/i":
                        case "/give":
                            if (parts.Length > 1)
                            {
                                OnDropCommand(player, text.Replace(parts[0] + " ", ""));
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/npc":
                        case "/summon":
                        case "/spawn":
                        case "/mob":
                            if (parts.Length > 1)
                            {
                                OnSummonCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/plugin":
                            if (parts.Length > 2)
                            {
                                OnPluginCommand(player, parts[2], parts[1]);
                            }
                            break;

                        case "/help":
                            if (parts.Length > 1)
                            {
                                OnHelpCommand(player, parts[1]);
                            }
                            else
                            {
                                OnHelpCommand(player);
                            }
                            break;

                        case "/destruction":
                        case "/allowbreak":
                            OnDestructionCommand(player);
                            break;

                        case "/construction":
                        case "/allowbuild":
                            OnConstructionCommand(player);
                            break;

                        case "/say":
                        case "/broadcast":
                            if (parts.Length > 1)
                            {
                                OnSayCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/moderate":
                            OnModerateCommand(player);
                            break;

                        case "/meteor":
                        case "/stars":
                        case "/invasion":
                        case "/bloodmoon":
                        case "/hardcore":
                        case "/infinvasion":
                            break;

                        case "/butcher":
                            if (parts.Length > 1)
                            {
                                OnButcherCommand(player, parts[1]);
                            }
                            else
                            {
                                OnButcherCommand(player);
                            }
                            break;

                        case "/kill":
                            if (parts.Length > 1)
                            {
                                OnKillCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/giveheart":
                        case "/hearts":
                        case "/heal":
                            if (parts.Length > 1)
                            {
                                OnHealCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/war":
                        case "/pvp":
                        case "/peace":
                        case "/peaceful":
                            OnPeacefulCommand(player);
                            break;

                        case "/requireregister":
                            OnRequireRegisterCommand(player);
                            break;

                        case "/blockregister":
                            OnBlockRegisterCommand(player);
                            break;

                        case "/allowregister":
                            if (parts.Length > 1)
                            {
                                OnAllowRegisterCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/dynamite":
                        case "/allowbomb":
                            OnDynamiteCommand(player);
                            break;

                        case "/kickdynamite":
                            OnKickDynamiteCommand(player);
                            break;

                        case "/bandynamite":
                            OnBanDynamiteCommand(player);
                            break;

                        case "/save":
                            OnSaveCommand(player);
                            break;

                        case "/shutdown":
                        case "/stop":
                            OnStopCommand(player);
                            break;

                        case "/op":
                            if (parts.Length > 1)
                            {
                                OnOpCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/deop":
                            if (parts.Length > 1)
                            {
                                OnDeopCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/mod":
                            if (parts.Length > 1)
                            {
                                OnModCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/demod":
                            if (parts.Length > 1)
                            {
                                OnDemodCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/time":
                            if (parts.Length > 1)
                            {
                                OnTimeCommand(player, parts[1]);
                            }
                            else
                            {
                                OnTimeCommand(player);
                            }
                            break;

                        case "/day":
                            OnDayCommand(player);
                            break;

                        case "/night":
                            OnNightCommand(player);
                            break;

                        case "/dusk":
                            OnDuskCommand(player);
                            break;

                        case "/noon":
                            OnNoonCommand(player);
                            break;

                        case "/midnight":
                            OnMidnightCommand(player);
                            break;

                        case "/freezetime":
                            OnFreezeTimeCommand(player);
                            break;

                        case "/whowas":
                            if (parts.Length > 1)
                            {
                                OnWhoWasCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/whois":
                        case "/ip":
                            if (parts.Length > 1)
                            {
                                OnIpCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/reload":
                        case "/rehash":
                            OnReloadCommand(player);
                            break;

                        case "/password":
                            if (parts.Length > 1)
                            {
                                OnPasswordCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/lava":
                        case "/allowlava":
                            OnLavaCommand(player);
                            break;

                        case "/water":
                        case "/allowwater":
                            OnWaterCommand(player);
                            break;

                        case "/spike":
                        case "/allowspike":
                            OnSpikeCommand(player);
                            break;

                        case "/rollback":
                            if (parts.Length > 1)
                            {
                                OnRollbackCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/rban":
                            if (parts.Length > 1)
                            {
                                OnRBan(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/check":
                            OnCheckCommand(player);
                            break;

                        case "/register":
                            if (parts.Length > 1)
                            {
                                OnRegisterCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/deregister":
                            if (parts.Length > 1)
                            {
                                OnDeregisterCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/spawnrate":
                        case "/sr":
                        case "/srate":
                            if (parts.Length > 1)
                            {
                                OnSpawnRateCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/maxspawns":
                        case "/ms":
                        case "/mspawns":
                            if (parts.Length > 1)
                            {
                                OnMaxSpawnsCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/id":
                        case "/identify":
                        case "/login":
                            if (parts.Length > 1)
                            {
                                OnIdentifyCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/changepw":
                        case "/changepassword":
                        case "/changelogin":
                            if (parts.Length > 1)
                            {
                                OnChangeCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/maxslots":
                            if (parts.Length > 1)
                            {
                                OnMaxSlotsCommand(player, parts[1]);
                            }
                            else
                            {
                                Session.Sessions[player].SendError("Syntax Error.");
                            }
                            break;

                        case "/trim":
                            OnTrimCommand(player);
                            break;

                        case "/truncate":
                            OnTruncateCommand(player);
                            break;

                        default:
                            Session.Sessions[player].SendError("Command does not exist.");
                            break;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void OnMOTDCommand(int player)
        {
            Session.Sessions[player].SendText(MainMod.motd);
        }

        private static void OnMOTDCommand(int player, string p)
        {
            MainMod.motd = p;
            NetMessageMod.BroadcastMessage("Server MOTD Changed");
            NetMessageMod.BroadcastMessage(p);
        }

        private static void OnPasswordCommand(int player, string pass)
        {
            NetplayMod.password = pass;
            Session.Sessions[player].SendText("Changed Password to: " + pass);
        }

        private static void OnWhoWasCommand(int player, string p)
        {
            Session.Sessions[player].SendError("Not Implemented.");
        }

        private static void OnWhisperCommand(int player, string p)
        {
            Session.Sessions[player].SendError("Not Implemented.");
        }

        private static void OnReplyCommand(int player, string p)
        {
            Session.Sessions[player].SendError("Not Implemented.");
        }

        private static void OnButcherCommand(int player)
        {
            int killcount = 0;
            for (int i = 0; i < MainMod.Npc.Length; i++)
            {
                if (MainMod.Npc[i].active && !MainMod.Npc[i].townNPC && !MainMod.Npc[i].friendly)
                {
                    MainMod.Npc[i].StrikeNPC(99999, 90f, 1);
                    NetMessageMod.SendData((int)PacketTypes.StrikeNpc, -1, -1, "", i, 99999, 90f, 1);
                    killcount++;
                }
            }
            Session.Sessions[player].SendText("Killed " + killcount + " Mobs");
        }

        private static void OnButcherCommand(int player, string range)
        {
            Session.Sessions[player].SendError("Not Implemented.");
        }

        private static void OnRequireRegisterCommand(int player)
        {
            MainMod.Config.RequireRegistration = !MainMod.Config.RequireRegistration;
            Session.Sessions[player].SendText("Player registration is now " + (MainMod.Config.RequireRegistration ? "required" : "optional"));
        }

        private static void OnModerateCommand(int player)
        {
            MainMod.Config.RequireRegistrationForChat = !MainMod.Config.RequireRegistrationForChat;
            Session.Sessions[player].SendText("Unregistered chat is now " + (MainMod.Config.RequireRegistrationForChat ? "blocked" : "unblocked"));
        }

        private static void OnBlockRegisterCommand(int player)
        {
            MainMod.Config.BlockRegistration = !MainMod.Config.BlockRegistration;
            Session.Sessions[player].SendText("Player registration has been " + (MainMod.Config.BlockRegistration ? "blocked" : "unblocked"));
        }

        private static void OnAllowRegisterCommand(int player, string p)
        {
            int target = MainMod.GetPlayer(p);
            if (target < 0)
            {
                Session.Sessions[player].SendError("Player could not be found.");
                return;
            }
            Session.Sessions[target].IsAllowedToRegister = !Session.Sessions[target].IsAllowedToRegister;
            Session.Sessions[player].SendText("Player " + (Session.Sessions[target].IsAllowedToRegister ? "allowed" : "disallowed") + " to register");
        }

        private static void OnSaveCommand(int player)
        {
            Session.Sessions[player].SendText("Force saving world.");
            WorldGenMod.SaveWorld(false);
        }

        private static void OnTimeCommand(int player)
        {
            Session.Sessions[player].SendError("Not Implemented.");
        }

        private static void OnTimeCommand(int player, string time)
        {
            Session.Sessions[player].SendError("Not Implemented.");
        }

        private static void OnOpMeCommand(int player, string password)
        {
            if (password == MainMod.Config.OpMePassword && !password.ToUpper().Contains("DISABLED"))
            {
                string p;
                if (Session.Sessions[player].IsLoggedIn)
                {
                    Session.Sessions[player].SendText("Added IP as OP");
                    p = Session.Sessions[player].Username;
                }
                else
                {
                    Session.Sessions[player].SendText("Added username as OP");
                    p = Session.Sessions[player].IpAddress;
                }
                MainMod.Config.Ops.Add(p);
                MainMod.Config.Mods.Remove(p);
                MainMod.SaveConfig();
            }
            else
            {
                Session.Sessions[player].SendError("Incorrect password.");
            }
        }

        private static void OnDayCommand(int player)
        {
            MainMod.SetTime(true, 0);
            Session.Sessions[player].SendText("Time changed to day.");
        }

        private static void OnNightCommand(int player)
        {
            MainMod.SetTime(false, 0);
            Session.Sessions[player].SendText("Time changed to night.");
        }

        private static void OnDuskCommand(int player)
        {
            MainMod.SetTime(false, 0);
            Session.Sessions[player].SendText("Time changed to dusk.");
        }

        private static void OnNoonCommand(int player)
        {
            MainMod.SetTime(true, 27000);
            Session.Sessions[player].SendText("Time changed to noon.");
        }

        private static void OnMidnightCommand(int player)
        {
            MainMod.SetTime(false, 16200);
            Session.Sessions[player].SendText("Time changed to midnight.");
        }

        private static void OnFreezeTimeCommand(int player)
        {
            Session.Sessions[player].SendError("Not Implemented.");
        }

        private static void OnTeleHereCommand(int player, string p)
        {
            int plr = MainMod.GetPlayer(p);
            if (plr < 0)
            {
                Session.Sessions[player].SendError("Player could not be found.");
                return;
            }
            Session.Sessions[plr].Teleport((int)MainMod.Player[player].position.X / 16, (int)MainMod.Player[player].position.Y / 16);
            Session.Sessions[plr].SendText("You are being teleported to " + Session.Sessions[player].Username + '.');
        }

        private static void OnButterfingersCommand(int player, string target)
        {
            int pl = MainMod.GetPlayer(target);
            if (pl != -1)
            {
                Session.Sessions[pl].Butterfingers = !Session.Sessions[pl].Butterfingers;
                Session.Sessions[player].SendText("Toggled butterfingers on player!");
            }
            else
            {
                Session.Sessions[player].SendError("Player not found!");
            }
        }

        private static void OnDumbCommand(int player, string target)
        {
            int pl = MainMod.GetPlayer(target);
            if (pl != -1)
            {
                Session.Sessions[player].Stupidized = !Session.Sessions[player].Stupidized;
                Session.Sessions[player].SendText("Toggled stupidization on player!");
            }
            else
            {
                Session.Sessions[player].SendError("Player not found!");
            }
        }

        private static void OnTpHereCommand(int player)
        {
            Session.Sessions[player].SendText("Warning! /telehere works by killing the target and changing his spawnpoint.");
            Session.Sessions[player].SendText("If you are okay with the target dying, use /telehere instead of /tphere.");
        }

        private static void OnWarpCommand(int player, string warp)
        {
            if (Database.WarpExists(warp))
            {
                int[] XandY = Database.GetWarp(warp);
                Session.Sessions[player].Teleport(XandY[0] / 16, XandY[1] / 16);
            }
            else
            {
                Session.Sessions[player].SendError("Warp not found.");
            }
        }

        private static void OnSetWarpCommand(int player, string warp)
        {
            try
            {
                int X = (int)MainMod.Player[player].position.X;
                int Y = (int)MainMod.Player[player].position.Y;

                Database.SetWarp(warp, X, Y);
                Session.Sessions[player].SendText("Set warp successfully.");
            }
            catch(Exception e)
            {
                Session.Sessions[player].SendError("Exception whilst setting warp...");
                Console.WriteLine(e);
            }
        }

        private static void OnTeleCommand(int player, string p)
        {
            int plr = MainMod.GetPlayer(p);
            if (plr < 0)
            {
                Session.Sessions[player].SendError("Player could not be found.");
                return;
            }
            Session.Sessions[player].Teleport((int)MainMod.Player[player].position.X / 16, (int)MainMod.Player[player].position.Y / 16);
        }

        private static void OnTpCommand(int player)
        {
            Session.Sessions[player].SendText("Warning! /tele works by killing you and changing your spawnpoint.");
            Session.Sessions[player].SendText("If you are okay with dying, use /tele instead of /tp.");
        }

        private static void OnSetGroupCommand(int player, string targetname, string group)
        {
            int target = MainMod.GetPlayer(targetname);
            if (target < 0)
            {
                Session.Sessions[player].SendError("Player could not be found.");
                return;
            }
            Session.Sessions[target].SetGroup(group);
        }

        private static void OnTruncateCommand(int player)
        {
            Session.Sessions[player].SendText("Starting rollback truncate operation...");
            Database.Truncate();
            Session.Sessions[player].SendText("Truncate operation complete.");
        }

        private static void OnTrimCommand(int player)
        {
            Database.TrimAsync();
            Session.Sessions[player].SendText("Trim request sent.");
        }

        private static void OnCheckCommand(int player)
        {
            Session.Sessions[player].IsCheckingBlock = true;
            Session.Sessions[player].SendText("The next block that you place or destroy will be checked.");
        }

        public static void OnPluginCommand(int player, string pluginName, string param)
        {
            Plugin pl;
            switch (param)
            {
                case "enable":
                    if (PluginManager.PluginExists(pluginName) && !MainMod.Config.Plugins.Contains(pluginName))
                    {
                        MainMod.Config.Plugins.Add(pluginName);
                        PluginManager.LoadPlugin(pluginName);
                        Session.Sessions[player].SendText("Plugin enabled!");
                    }
                    else
                    {
                        Session.Sessions[player].SendError("Plugin already enabled or doesn't exist!");
                    }
                    break;

                case "load":
                    if (PluginManager.PluginExists(pluginName))
                    {
                        PluginManager.LoadPlugin(pluginName);
                        Session.Sessions[player].SendText("Plugin loaded!");
                    }
                    else
                    {
                        Session.Sessions[player].SendError("Plugin not found!");
                    }
                    break;

                case "unload":
                    pl = PluginManager.GetPlugin(pluginName);
                    if (pl != null)
                    {
                        PluginManager.UnloadPlugin(pl);
                    }
                    else
                    {
                        Session.Sessions[player].SendError("Plugin either doesn't exist or isn't loaded!");
                    }
                    break;

                case "disable":
                    pl = PluginManager.GetPlugin(pluginName);
                    if (MainMod.Config.Plugins.Contains(pluginName))
                    {
                        MainMod.Config.Plugins.Remove(pluginName);
                        if (pl != null) PluginManager.UnloadPlugin(pl);
                        Session.Sessions[player].SendText("Disabled plugin.");
                    }
                    else
                    {
                        Session.Sessions[player].SendError("Plugin not enabled! Try /plugin unload name");
                    }
                    break;

                default:
                    Session.Sessions[player].SendError("Invalid selection: " + param);
                    break;
            }
        }

        private static void OnRBan(int player, string p)
        {
            int target = MainMod.GetPlayer(p);
            if (target < 0)
            {
                Session.Sessions[player].SendError("Unknown player.");
                return;
            }
            Session.Sessions[target].Rollback();
            NetplayMod.AddBan(target);
            Session.Sessions[player].SendText("Rolled back and banned " + Session.Sessions[target].Username + ".");
            NetplayMod.KickPlayer((byte)target, "Banned");
        }

        private static void OnRollbackCommand(int player, string p)
        {
            Database.Rollback(player, p);
        }

        private static void OnLavaCommand(int player)
        {
            MainMod.Config.AllowLava = !MainMod.Config.AllowLava;
            Session.Sessions[player].SendText("Lava placement " + (MainMod.Config.AllowLava ? "enabled." : "disabled."));
            MainMod.SaveConfig();
        }

        private static void OnWaterCommand(int player)
        {
            MainMod.Config.AllowWater = !MainMod.Config.AllowWater;
            Session.Sessions[player].SendText("Water placement " + (MainMod.Config.AllowWater ? "enabled." : "disabled."));
            MainMod.SaveConfig();
        }

        private static void OnSpikeCommand(int player)
        {
            MainMod.Config.AllowSpikes = !MainMod.Config.AllowSpikes;
            Session.Sessions[player].SendText("Spike placement " + (MainMod.Config.AllowSpikes ? "enabled." : "disabled."));
            MainMod.SaveConfig();
        }

        private static void OnReloadCommand(int player)
        {
            MainMod.LoadConfig();
            PluginManager.Reload();
            Session.Sessions[player].SendText("Configuration Reloaded");
        }

        private static void OnIpCommand(int player, string p)
        {
            int target = MainMod.GetPlayer(p);
            if (target < 0)
            {
                Session.Sessions[player].SendError("Unknown player.");
                return;
            }
            string address = ((IPEndPoint)NetplayMod.ServerSock[target].tcpClient.Client.RemoteEndPoint).Address.ToString();
            Session.Sessions[player].SendText(MainMod.Player[target].name + "'s IP address is " + address + ".");
        }

        private static void OnSpawnRateCommand(int player, string p)
        {
            int spawnRate;
            if (!int.TryParse(p, out spawnRate))
            {
                Session.Sessions[player].SendError("Invalid input! It's /spawnrate 700");
                return;
            }
            NPCMod.DefaultSpawnRate = spawnRate;
            NPCMod.SpawnRate = spawnRate;
            MainMod.Config.SpawnRate = spawnRate;
            MainMod.SaveConfig();
            Session.Sessions[player].SendText("Set the spawnrate.");
        }

        private static void OnMaxSpawnsCommand(int player, string p)
        {
            int maxSpawns;
            if (!int.TryParse(p, out maxSpawns))
            {
                Session.Sessions[player].SendError("Invalid input! It's /maxspawns 4");
                return;
            }
            NPCMod.DefaultMaxSpawns = maxSpawns;
            NPCMod.MaxSpawns = maxSpawns;
            MainMod.Config.MaximumSpawns = maxSpawns;
            MainMod.SaveConfig();
            Session.Sessions[player].SendText("Set the max spawns.");
        }

        private static void OnMaxSlotsCommand(int player, string p)
        {
            int maxSlots;
            if (!int.TryParse(p, out maxSlots))
            {
                Session.Sessions[player].SendError("Invalid input! It's /maxslots <number>");
                return;
            }
            MainMod.Config.PlayerSlots = maxSlots;
            MainMod.SaveConfig();
            Session.Sessions[player].SendText("Set the max slots to " + maxSlots);
        }

        private static void OnStopCommand(int player)
        {
            MainMod.Notice(MainMod.Player[player].name + " instructed the server to stop", true);
            NetplayMod.disconnect = true;
        }

        private static void OnBanDynamiteCommand(int player)
        {
            MainMod.Config.BanMassDestruction = !MainMod.Config.BanMassDestruction;
            if (MainMod.Config.BanMassDestruction && MainMod.Config.KickMassDestruction)
            {
                MainMod.Config.KickMassDestruction = false;
                Session.Sessions[player].SendText("Kick on use of explosives disabled.");
            }
            Session.Sessions[player].SendText("Ban on use of explosives " + (MainMod.Config.BanMassDestruction ? "enabled." : "disabled."));
            MainMod.SaveConfig();
        }

        private static void OnKickDynamiteCommand(int player)
        {
            MainMod.Config.KickMassDestruction = !MainMod.Config.KickMassDestruction;
            if (MainMod.Config.KickMassDestruction && MainMod.Config.BanMassDestruction)
            {
                MainMod.Config.BanMassDestruction = false;
                Session.Sessions[player].SendText("Ban on use of explosives disabled.");
            }
            Session.Sessions[player].SendText("Kick on use of explosives " + (MainMod.Config.KickMassDestruction ? "enabled." : "disabled."));
            MainMod.SaveConfig();
        }

        private static void OnDynamiteCommand(int player)
        {
            MainMod.Config.AllowMassDestruction = !MainMod.Config.AllowMassDestruction;
            Session.Sessions[player].SendText("Explosives " + (MainMod.Config.AllowMassDestruction ? "enabled." : "disabled."));
            MainMod.SaveConfig();
        }

        private static void OnPeacefulCommand(int player)
        {
            MainMod.Config.Peaceful = !MainMod.Config.Peaceful;
            Session.Sessions[player].SendText("PvP " + (MainMod.Config.Peaceful ? "disabled." : "enabled."));
            MainMod.SaveConfig();
        }

        private static void OnSayCommand(int player, string p)
        {
            NetMessageMod.BroadcastMessage(p);
        }

        private static void OnDestructionCommand(int player)
        {
            MainMod.Config.AllowDestruction = !MainMod.Config.AllowDestruction;
            Session.Sessions[player].SendText("Destruction " + (MainMod.Config.AllowDestruction ? "enabled." : "disabled."));
            MainMod.SaveConfig();
        }

        private static void OnConstructionCommand(int player)
        {
            MainMod.Config.AllowConstruction = !MainMod.Config.AllowConstruction;
            Session.Sessions[player].SendText("Construction " + (MainMod.Config.AllowConstruction ? "enabled." : "disabled."));
            MainMod.SaveConfig();
        }

        private static void OnHelpCommand(int player)
        {
            Session.Sessions[player].SendText("For help, go to http://tmod.us -> Server.");
        }

        private static void OnHelpCommand(int player, string p)
        {
            Session.Sessions[player].SendText("Seriously... For help, go to http://tmod.us -> Server.");
        }

        private static void OnDropCommand(int player, string p)
        {
            string raw = p.Trim();
            string name = raw.TrimEnd(" 1234567890".ToCharArray());
            try
            {
                int stack = 1;
                if (raw != name)
                {
                    stack = int.Parse(raw.Substring(name.Length + 1));
                    if (stack < 1)
                    {
                        stack = 1;
                    }
                    if (stack > 9999)
                    {
                        stack = 9999;
                    }
                }

                dynamic item = ItemMod.Item.GetConstructor(new Type[0]).Invoke(new object[0]);
                item.SetDefaults(name.ToProper());
                if (item.type < 1)
                {
                    Session.Sessions[player].SendError("Item does not exist.");
                    return;
                }
                Session.Sessions[player].SendText("Dropping item at your feet.");
                int itemid = ItemMod.NewItem((int)MainMod.Player[player].position.X, (int)MainMod.Player[player].position.Y, 0x20, 0x20, item.type, stack);
                MainMod.Item[itemid].stack = stack;
                MainMod.Item[itemid].owner = player;
                NetMessageMod.SendData((int)PacketTypes.DropItem, -1, -1, "", itemid, 0f, 0f, 0f);
                NetMessageMod.SendData((int)PacketTypes.ItemOwner, -1, -1, "", itemid, 0f, 0f, 0f);
            }
            catch
            {
                Session.Sessions[player].SendError("Something went wrong.");
                Session.Sessions[player].SendError("Make sure you're using the correct syntax.");
                Session.Sessions[player].SendError("Example: /give gravitation potion 3");
            }
        }

        private static void OnSummonCommand(int player, string p)
        {
            int id;
            if (!int.TryParse(p, out id) || id < 0)
            {
                dynamic npc = NPCMod.NPC.GetConstructor(new Type[0]).Invoke(new object[0]);
                npc.SetDefaults(p.ToProper());
                if (npc.type < 1)
                {
                    Session.Sessions[player].SendError("NPC doesn't exist");
                    return;
                }
                id = npc.type;
            }
            if (id == 4 || id == 13 || id == 35)
            {
                NPCMod.SpawnOnPlayer(player, id);
            }
            else
            {
                int index = NPCMod.NewNPC((int)MainMod.Player[player].position.X, (int)MainMod.Player[player].position.Y, id);
                MainMod.Npc[index].target = player;
                NetMessageMod.SendData(0x17, -1, -1, "", index, 0f, 0f, 0f);
            }
        }

        private static void OnSummonCommand(int player, string p, string p2)
        {
            int id;
            int count;
            try { count = int.Parse(p2); }
            catch { Session.Sessions[player].SendError("Invalid number!"); return; }
            if (!int.TryParse(p, out id) || id < 0)
            {
                dynamic npc = NPCMod.NPC.GetConstructor(new Type[0]).Invoke(new object[0]);
                npc.SetDefaults(p.ToProper());
                if (npc.type < 1)
                {
                    Session.Sessions[player].SendError("NPC doesn't exist");
                    return;
                }
                id = npc.type;
            }
            for (int i = 0; i < count; i++)
            {
                if (id == 4 || id == 13 || id == 35)
                {
                    NPCMod.SpawnOnPlayer(player, id);
                }
                else
                {
                    int index = NPCMod.NewNPC((int)MainMod.Player[player].position.X, (int)MainMod.Player[player].position.Y, id);
                    MainMod.Npc[index].target = player;
                    NetMessageMod.SendData(0x17, -1, -1, "", index, 0f, 0f, 0f);
                }
            }
        }

        private static void OnHealCommand(int player, string p)
        {
            int target = MainMod.GetPlayer(p);
            if (target < 0)
            {
                Session.Sessions[player].SendError("Player not found.");
                return;
            }
            dynamic plr = MainMod.Player[target];
            Session.Sessions[player].SendText("Dropping hearts and stars.");

            for (int i = 0; i < 20; i++)
            {
                string name = "Heart";
                dynamic item = ItemMod.Item.GetConstructor(new Type[0]).Invoke(new object[0]);
                item.SetDefaults(name.ToProper());
                if (item.type < 1)
                {
                    return;
                }
                int itemid = ItemMod.NewItem((int)MainMod.Player[target].position.X, (int)MainMod.Player[target].position.Y, 0x20, 0x20, item.type, 1);
                MainMod.Item[itemid].owner = target;
                NetMessageMod.SendData((int)PacketTypes.DropItem, -1, -1, "", itemid, 0f, 0f, 0f);
                NetMessageMod.SendData((int)PacketTypes.ItemOwner, -1, -1, "", itemid, 0f, 0f, 0f);
            }

            for (int i = 0; i < 10; i++)
            {
                string name = "Star";
                dynamic item = ItemMod.Item.GetConstructor(new Type[0]).Invoke(new object[0]);
                item.SetDefaults(name.ToProper());
                if (item.type < 1)
                {
                    return;
                }
                int itemid = ItemMod.NewItem((int)MainMod.Player[target].position.X, (int)MainMod.Player[target].position.Y, 0x20, 0x20, item.type, 1);
                MainMod.Item[itemid].owner = target;
                NetMessageMod.SendData((int)PacketTypes.DropItem, -1, -1, "", itemid, 0f, 0f, 0f);
                NetMessageMod.SendData((int)PacketTypes.ItemOwner, -1, -1, "", itemid, 0f, 0f, 0f);
            }
        }

        private static void OnKillCommand(int player, string p)
        {
            int target = MainMod.GetPlayer(p);
            if (target < 0)
            {
                Session.Sessions[player].SendError("Player could not be found.");
                return;
            }
            Session.Sessions[player].SendText("Player killed.");
            NetMessageMod.SendData(0x1a, -1, -1, "", target, 1f, 20000f, 0f);
        }

        private static void OnKickCommand(int player, string p)
        {
            int target = MainMod.GetPlayer(p);
            if (target < 0)
            {
                Session.Sessions[player].SendError("Player could not be found.");
                return;
            }
            Session.Sessions[player].SendText("Player kicked.");
            NetplayMod.KickPlayer((byte)target);
        }

        private static void OnMeCommand(int player, string p)
        {
            NetMessageMod.SendData(0x19, -1, -1, "* " + MainMod.Player[player].name + " " + p, 0xff, 200f, 100f, 0f);
        }

        private static void OnMuteCommand(int player, string p)
        {
            int target = MainMod.GetPlayer(p);
            if (target < 0)
            {
                Session.Sessions[player].SendError("Player could not be found.");
                return;
            }
            Session.Sessions[target].IsMuted = !Session.Sessions[target].IsMuted;
            Session.Sessions[player].SendText("Player " + (Session.Sessions[target].IsMuted ? "muted" : "unmuted"));
        }

        private static void OnPartyCommand(int player, string p)
        {
            if (MainMod.Player[player].team != 0)
            {
                for (int i = 0; i < 0xff; i++)
                {
                    if (MainMod.Player[i].team == MainMod.Player[player].team)
                    {
                        dynamic c = MainMod.TeamColor[MainMod.Player[player].team];
                        NetMessageMod.SendData(0x19, i, -1, p, player, c.R, c.G, c.B);
                    }
                }
                return;
            }
            Session.Sessions[player].SendError("Your not in a party.");
        }

        private static void OnListCommand(int player)
        {
            string str10 = "";
            int num95 = 0;
            int count = 0;
            for (; num95 < 8; num95++)
            {
                if (MainMod.Player[num95].active)
                {
                    if (str10 == "")
                    {
                        str10 = str10 + MainMod.Player[num95].name;
                    }
                    else
                    {
                        str10 = str10 + ", " + MainMod.Player[num95].name;
                    }
                    count++;
                }
            }
            Session.Sessions[player].SendText("Online: " + str10);
            str10 = "";
            for (; num95 < 16; num95++)
            {
                if (MainMod.Player[num95].active)
                {
                    if (str10 == "")
                    {
                        str10 = str10 + MainMod.Player[num95].name;
                    }
                    else
                    {
                        str10 = str10 + ", " + MainMod.Player[num95].name;
                    }
                    count++;
                }
            }
            if (str10 != "")
            {
                Session.Sessions[player].SendText(str10);
            }
            str10 = "";
            for (; num95 < 24; num95++)
            {
                if (MainMod.Player[num95].active)
                {
                    if (str10 == "")
                    {
                        str10 = str10 + MainMod.Player[num95].name;
                    }
                    else
                    {
                        str10 = str10 + ", " + MainMod.Player[num95].name;
                    }
                    count++;
                }
            }
            if (str10 != "")
            {
                Session.Sessions[player].SendText(str10);
            }
            str10 = "";
            for (; num95 < 32; num95++)
            {
                if (MainMod.Player[num95].active)
                {
                    if (str10 == "")
                    {
                        str10 = str10 + MainMod.Player[num95].name;
                    }
                    else
                    {
                        str10 = str10 + ", " + MainMod.Player[num95].name;
                    }
                    count++;
                }
            }
            if (str10 != "")
            {
                Session.Sessions[player].SendText(str10);
            }
            Session.Sessions[player].SendText("Total: " + count);
        }

        private static void OnDeopCommand(int player, string p)
        {
            int target = MainMod.GetPlayer(p);
            if (target < 0)
            {
                if (ValidateIP(p))
                {
                    MainMod.Config.Ops.Remove(p);
                    MainMod.SaveConfig();
                    Session.Sessions[player].SendText("Removed IP from op list");
                    return;
                }
                else
                {
                    MainMod.Config.Ops.Remove(p);
                    MainMod.SaveConfig();
                    Session.Sessions[player].SendText("Removed user from op list");
                    return;
                }
            }
            string address = ((IPEndPoint)NetplayMod.ServerSock[target].tcpClient.Client.RemoteEndPoint).Address.ToString();
            MainMod.Config.Ops.Remove(address);
            MainMod.Config.Ops.Remove(MainMod.Player[target].name);
            MainMod.SaveConfig();
            Session.Sessions[player].SendText(MainMod.Player[target].name + " has been deopped.");
        }

        private static void OnOpCommand(int player, string p)
        {
            if (ValidateIP(p))
            {
                MainMod.Config.Ops.Add(p);
                MainMod.Config.Mods.Remove(p);
                MainMod.SaveConfig();
                Session.Sessions[player].SetGroup("Ops");
                Session.Sessions[player].SendText("Added IP to op list");
                return;
            }
            else if (Database.IsUserRegistered(p))
            {
                Session.Sessions[player].SetGroup("Ops");
                Session.Sessions[player].SendText("Added user to op list");
            }
            else
            {
                Session.Sessions[player].SendText("Player not registered or invalid IP.");
                return;
            }
        }

        private static void OnDemodCommand(int player, string p)
        {
            int target = MainMod.GetPlayer(p);
            if (target < 0)
            {
                if (ValidateIP(p))
                {
                    MainMod.Config.Mods.Remove(p);
                    MainMod.SaveConfig();
                    Session.Sessions[player].SendText("Removed IP from mod list");
                    return;
                }
                else
                {
                    MainMod.Config.Mods.Remove(p);
                    MainMod.SaveConfig();
                    Session.Sessions[player].SendText("Removed user from mod list");
                    return;
                }
            }
            string address = ((IPEndPoint)NetplayMod.ServerSock[target].tcpClient.Client.RemoteEndPoint).Address.ToString();
            MainMod.Config.Mods.Remove(address);
            MainMod.Config.Mods.Remove(MainMod.Player[target].name);
            MainMod.SaveConfig();
            Session.Sessions[player].SendText(MainMod.Player[target].name + " has been demodded.");
        }

        private static void OnModCommand(int player, string p)
        {
            if (ValidateIP(p))
            {
                MainMod.Config.Mods.Add(p);
                MainMod.Config.Ops.Remove(p);
                MainMod.SaveConfig();
                Session.Sessions[player].SetGroup("Mods");
                Session.Sessions[player].SendText("Added IP to mod list");
                return;
            }
            else if (Database.IsUserRegistered(p))
            {
                Session.Sessions[player].SetGroup("Mods");
                Session.Sessions[player].SendText("Added user to mod list");
            }
            else
            {
                Session.Sessions[player].SendText("Player not registered or invalid IP.");
                return;
            }
        }

        private static void OnBanCommand(int player, string p)
        {
            int target = MainMod.GetPlayer(p);
            if (target < 0)
            {
                if (ValidateIP(p))
                {
                    NetplayMod.AddBanIP(p);
                    Session.Sessions[player].SendText("Added IP to ban list");
                    return;
                }
                else
                {
                    Session.Sessions[player].SendText("Player not found or invalid IP.");
                    return;
                }
            }
            NetplayMod.AddBan(target);
            NetplayMod.KickPlayer((byte)target, "Banned");
        }

        private static void OnUnBanCommand(int player, string p)
        {
            if (NetplayMod.CheckBan(p))
            {
                NetplayMod.RemBan(p);
                Session.Sessions[player].SendText("Removed IP to ban list");
                return;
            }
            else
            {
                Session.Sessions[player].SendText("IP not banned.");
                return;
            }
        }

        private static void OnRegisterCommand(int player, string pwd)
        {
            Session.Sessions[player].Register(Database.Password(pwd));
        }

        private static void OnChangeCommand(int player, string pwd)
        {
            Session.Sessions[player].ChangePassword(Database.Password(pwd));
        }

        private static void OnDeregisterCommand(int player, string username)
        {
            if (Database.IsUserRegistered(username))
            {
                Database.DeleteRegistration(username);
                MainMod.Config.Mods.Remove(username);
                MainMod.Config.Ops.Remove(username);
                int target = MainMod.GetPlayer(username);
                if (target != -1)
                {
                    Session.Sessions[player].SendText("User has been kicked and registration deleted.");
                    NetplayMod.KickPlayer((byte)target, "Your registration has been removed");
                    return;
                }
                else
                {
                    Session.Sessions[player].SendText("User's registration has been deleted.");
                }
            }
            else
            {
                Session.Sessions[player].SendError("Username isn't registered.");
            }
        }

        private static void OnIdentifyCommand(int player, string pwd)
        {
            if (!Session.Sessions[player].IsLoggedIn)
            {
                Session.Sessions[player].Login(Database.Password(pwd));
            }
            else
            {
                Session.Sessions[player].SendError("You are already logged in!");
            }
        }

        public static bool ValidateIP(string IP)
        {
            IPAddress test;
            return IPAddress.TryParse(IP, out test);
        }
    }
}