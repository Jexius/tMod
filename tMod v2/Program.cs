using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Reflection;
using Terraria;

namespace tMod_v3
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() > 0 && args[0] == "-config")
            {
                Console.WriteLine("tMod is starting in configuration mode...");
                MainMod.LoadConfig(true);
                FieldInfo[] fields = MainMod.Config.GetType().GetFields(BindingFlags.Instance |BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
                foreach (FieldInfo PI in fields)
                {
                    try
                    {
                        // if one person complains about this I'll be so pissed.
                        // It was being weird, no other method was working, sue me.
                        if (PI.FieldType.Equals(typeof(String)) || PI.FieldType.Equals(typeof(Int32)) || PI.FieldType.Equals(typeof(Boolean)) ||
                            PI.Name == "Ops")
                        {
                            if (PI.FieldType.Equals(typeof(String)))
                            {
                                Console.Write("{0} (enter for {1}): ", PI.Name, PI.GetValue(MainMod.Config));
                                string read = Console.ReadLine();
                                if(read != "") PI.SetValue(MainMod.Config, read);
                            }
                            else if (PI.FieldType.Equals(typeof(Int32)))
                            {
                                Console.Write("{0} (enter for {1}): ", PI.Name, PI.GetValue(MainMod.Config));
                                string read = Console.ReadLine();
                                if (read != "") PI.SetValue(MainMod.Config, int.Parse(read));
                            }
                            else if (PI.FieldType.Equals(typeof(Boolean)))
                            {
                                Console.Write("{0} (enter for {1} - put true or false): ", PI.Name, PI.GetValue(MainMod.Config));
                                string read = Console.ReadLine();
                                if (read != "") PI.SetValue(MainMod.Config, bool.Parse(read));
                            }
                            else if (PI.Name == "Ops")
                            {
                                Console.WriteLine("This step will add you as an admin to your server!");
                                Console.Write("Please enter your IP address here (or press enter to skip): ");
                                string IP = Console.ReadLine();
                                if (IP != "" && !MainMod.Config.Ops.Contains(IP))
                                {
                                    MainMod.Config.Ops.Add(IP);
                                }
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Problem with input, default set.");
                    }
                }
                Console.WriteLine("Done!");
                MainMod.SaveConfig();
            }
            else if (args.Count() > 1)
            {
                MainMod.LoadConfig(true);
                for (int i = 0; i < args.Count(); i++)
                {
                    // Not the best way by far, but sue me, I did this at 2am (1:53:03am to be exact ;D)
                    try
                    {
                        FieldInfo field = MainMod.Config.GetType().GetField(args[i].Replace("-", ""));
                        if (field != null)
                        {
                            i++;
                            if (args.Count() > i)
                            {
                                if (field.FieldType.Equals(typeof(String)))
                                {
                                    field.SetValue(MainMod.Config, args[i]);
                                }
                                else if (field.FieldType.Equals(typeof(Boolean)))
                                {
                                    field.SetValue(MainMod.Config, bool.Parse(args[i]));
                                }
                                else if (field.FieldType.Equals(typeof(Int32)))
                                {
                                    field.SetValue(MainMod.Config, int.Parse(args[i]));
                                }
                            }
                        }
                    }
                    catch { }
                }
                MainMod.SaveConfig();
            }
            if (!File.Exists("TerrariaServer.exe"))
            {
                Console.WriteLine("TerrariaServer.exe not found. Please copy it from your Terraria installation directory.");
                Console.ReadLine();
            }
            Console.WriteLine("tMod v{0}", MainMod.tModVersion);
            if (File.Exists(@"tMod\Database.sdf"))
            {
                Console.WriteLine("WARNING!!!");
                Console.WriteLine("tMod no longer supports SqlServerCe databases.");
                Console.WriteLine("We have detected that you still have yours.");
                Console.WriteLine();
                Console.WriteLine("If you want to use your old database (contains rollback information, users, etc) please follow the guide here: ");
                Console.WriteLine("http://www.tmod.biz/database.html");
                Console.WriteLine();
                Console.WriteLine("Program will continue in ten seconds...");
                Console.WriteLine();
                Console.Beep(1000, 100);
                Console.Beep(1000, 100);
                Thread.Sleep(10000);
            }
            if (System.AppDomain.CurrentDomain.FriendlyName == "update_temp.exe")
            {
                Console.WriteLine("Update mode detected, transferring...");
                Thread.Sleep(3000); // Just in case the old executable hasn't quit yet
                if (File.Exists("tMod v3.exe")) File.Delete("tMod v3.exe");
                if (File.Exists("update_mm.exe")) File.Delete("update_mm.exe");
                File.Copy(System.AppDomain.CurrentDomain.FriendlyName, "tMod v3.exe");
                Process.Start("tMod v3.exe");
                Environment.Exit(0);
            }
            else if (File.Exists("update_temp.exe"))
            {
                Console.WriteLine("Please wait, removing update_temp.exe");
                Thread.Sleep(3000);
                File.Delete("update_temp.exe");
            }
            Console.WriteLine("Please wait, scanning for Terraria.exe!");
            tMod tmod = new tMod();
            tmod.inject();
        }

        private static void DownloadFile(string downloadFrom, string saveTo)
        {
            using (WebClient client = new WebClient())
            {
                client.Proxy = new WebProxy();
                client.DownloadFile(downloadFrom, saveTo);
            }
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
    }
}