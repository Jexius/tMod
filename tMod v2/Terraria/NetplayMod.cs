using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Sockets;

namespace Terraria
{
    public class NetplayMod
    {
        public static Type Netplay;

        public static dynamic[] ServerSock
        {
            get
            {
                return (dynamic[])Netplay.GetField("serverSock").GetValue(null);
            }
            set
            {
                Netplay.GetField("serverSock").SetValue(null, value);
            }
        }

        public static dynamic ClientSock
        {
            get
            {
                return (dynamic)Netplay.GetField("clientSock").GetValue(null);
            }
            set
            {
                Netplay.GetField("clientSock").SetValue(null, value);
            }
        }

        public static IPAddress ServerListenIP
        {
            get
            {
                return (IPAddress)Netplay.GetField("serverListenIP").GetValue(null);
            }
            set
            {
                Netplay.GetField("serverListenIP").SetValue(null, value);
            }
        }

        public static bool disconnect
        {
            get
            {
                return (bool)Netplay.GetField("disconnect").GetValue(null);
            }
            set
            {
                Netplay.GetField("disconnect").SetValue(null, value);
            }
        }

        public static string password
        {
            get
            {
                return (string)Netplay.GetField("password").GetValue(null);
            }
            set
            {
                Netplay.GetField("password").SetValue(null, value);
            }
        }

        public static int ServerPort
        {
            get
            {
                return (int)Netplay.GetField("serverPort").GetValue(null);
            }
            set
            {
                Netplay.GetField("serverPort").SetValue(null, value);
            }
        }

        public static void KickPlayer(byte player, string reason = "Kicked")
        {
            NetMessageMod.SendData(0x2, player, -1, reason, 0, 0f, 0f, 0f);
            MainMod.Notice(MainMod.Player[player].name + " was kicked: " + reason, MainMod.Config.ShowKickNotifications);
        }

        public static void AddBanIP(string ip)
        {
            string banFile = (string)Netplay.GetField("banFile").GetValue(null);
            using (StreamWriter writer = new StreamWriter(banFile, true))
            {
                writer.WriteLine("//IP Ban");
                writer.WriteLine(ip);
            }
            MainMod.Notice(ip + " was banned", MainMod.Config.ShowBanNotifications);
        }

        public static void RemBan(string ip)
        {
            string tempFile = Path.GetTempFileName();
            string banFile = (string)Netplay.GetField("banFile").GetValue(null);

            using (var sr = new StreamReader(banFile))
            {
                using (var sw = new StreamWriter(tempFile))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line != ip)
                            sw.WriteLine(line);
                    }
                }
            }

            File.Delete(banFile);
            File.Move(tempFile, banFile);

            MainMod.Notice(ip + " was unbanned", MainMod.Config.ShowBanNotifications);
        }

        public static int GetSectionY(int n)
        {
            return (int)Netplay.GetMethod("GetSectionY").Invoke(null, new object[] { n });
        }

        public static int GetSectionX(int n)
        {
            return (int)Netplay.GetMethod("GetSectionX").Invoke(null, new object[] { n });
        }

        public static void AddBan(int target)
        {
            Netplay.GetMethod("AddBan").Invoke(null, new object[] { target });
            MainMod.Notice(MainMod.Player[target].name + " was banned", MainMod.Config.ShowBanNotifications);
        }

        public static bool CheckBan(string ip)
        {
            return (bool)Netplay.GetMethod("CheckBan").Invoke(null, new object[] { ip });
        }
    }
}
