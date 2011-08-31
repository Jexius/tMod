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

        public static void SetDefaultsMod(dynamic inst, int type)
        {
            switch (type)
            {
                case 0x16: // Guide
                    // This was a "meh" update
                    //inst.friendly = false;
                    //inst.defense = 2;
                    break;
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
