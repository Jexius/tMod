using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using System.Reflection;
using tMod_v3;

namespace Terraria
{
    public class NPCMod
    {
        public static Type NPC;

        public static int MaxSpawns
        {
            get
            {
                return (int)NPC.GetField("maxSpawns", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            }
            set
            {
                NPC.GetField("maxSpawns", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, value);
            }
        }

        public static int DefaultMaxSpawns
        {
            get
            {
                return (int)NPC.GetField("defaultMaxSpawns", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            }
            set
            {
                NPC.GetField("defaultMaxSpawns", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, value);
            }
        }

        public static int SpawnRate
        {
            get
            {
                return (int)NPC.GetField("spawnRate", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            }
            set
            {
                NPC.GetField("spawnRate", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, value);
            }
        }

        public static int DefaultSpawnRate
        {
            get
            {
                return (int)NPC.GetField("defaultSpawnRate", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            }
            set
            {
                NPC.GetField("defaultSpawnRate", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, value);
            }
        }

        public static int NewNPC(int x, int y, int type, int start = 0)
        {
            return (int)NPC.GetMethod("NewNPC").Invoke(null, new object[] { x, y, type, start });
        }

        public static void SpawnOnPlayer(int player, int id)
        {
            NPC.GetMethod("SpawnOnPlayer").Invoke(null, new object[] { player, id });
        }

        public static bool SetDefaultsMod(dynamic mob, dynamic name)
        {
            if (XeedMod.cmobs != null)
                lock (XeedMod.cmobs)
                    foreach (dynamic cm in XeedMod.cmobs)
                        if (cm.name.Equals(name))
                            try
                            {
                                mob.SetDefaults(cm.GetID(), -1f);
                                cm.ApplyTo(mob);
                                return true;
                            }
                            catch (Exception ex) { Console.WriteLine(ex); }
            return false;
        }

        public static void NPCLootMod(dynamic npc)
        {
            if (XeedMod.cmobs != null)
                lock (XeedMod.cmobs)
                    foreach (dynamic cm in XeedMod.cmobs)
                    {
                        if (cm.name.Equals(npc.name))
                        {
                            foreach (KeyValuePair<string, int> item in cm.drops)
                            {
                                int index = (int)ItemMod.Item.GetMethod("NewItem").Invoke(null, new object[] { (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 0, item.Value, true });
                                MainMod.Item[index].SetDefaults(item.Key);
                                MainMod.Item[index].stack = item.Value;
                                NetMessageMod.SendData(0x15, -1, -1, "", index);
                            }
                            return;
                        }
                    }
        }

        public static void SpawnNpc(string p)
        {
            if (MainMod.NetMode == 1)
            {
                NetMessageMod.SendData(0x19, MainMod.MyPlayer, -1, "/npc " + p, MainMod.MyPlayer, 255f, 255f, 255f);
            }
            else
            {
                dynamic npc = NPCMod.NPC.GetConstructor(new Type[0]).Invoke(new object[0]);
                npc.SetDefaults(p.ToProper());
                if (npc.type < 1)
                {
                    MainMod.NewText("NPC does not exist.", 255, 0, 0);
                    return;
                }

                MouseState mouseState = Mouse.GetState();
                int x = (int)(mouseState.X + MainMod.ScreenPosition.X);
                int y = (int)(mouseState.Y + MainMod.ScreenPosition.Y);
                int index = NPCMod.NewNPC(x, y, npc.type, 0);
                MainMod.Npc[index].target = MainMod.MyPlayer;

                if (index == 1000)
                {
                    MainMod.NewText("There are too many NPCs!", 255, 0, 0);
                    return;
                }

                MainMod.NewText("Spawned " + MainMod.Npc[index].name + ".", 175, 75, 255);
            }
        }
    }
}
