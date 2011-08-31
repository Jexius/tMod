using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using tMod_v3;

namespace Terraria
{
    public class Program
    {
        public static void tMod(string[] args)
        {
            Console.Clear();
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("tMod successfully injected!");
            Console.WriteLine();
            Console.WriteLine("Starting server...");
            dynamic main = tMod_v3.tMod.main.GetConstructor(new Type[0]).Invoke(new object[0]);
            MainMod.main = main;
            MainMod.LoadConfig();
            Database.Initialize();
            MainMod.DedServ();
            Database.Disconnect();
        }
    }
}
