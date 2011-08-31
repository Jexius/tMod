using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Reflection;
using tMod_v3;

namespace Terraria
{
    public static partial class MainMod
    {
        public static bool ChatMode
        {
            get
            {
                return (bool)tMod.main.GetField("chatMode").GetValue(null);
            }
            set
            {
                tMod.main.GetField("chatMode").SetValue(null, value);
            }
        }

        public static bool GrabSun
        {
            get
            {
                return (bool)tMod.main.GetField("grabSun").GetValue(null);
            }
            set
            {
                tMod.main.GetField("grabSun").SetValue(null, value);
            }
        }

        public static bool ShowSplash
        {
            get
            {
                return (bool)tMod.main.GetField("showSplash").GetValue(null);
            }
            set
            {
                tMod.main.GetField("showSplash").SetValue(null, value);
            }
        }

        public static bool FrameRelease
        {
            get
            {
                return (bool)tMod.main.GetField("frameRelease").GetValue(null);
            }
            set
            {
                tMod.main.GetField("frameRelease").SetValue(null, value);
            }
        }

        public static bool ReleaseUI
        {
            get
            {
                return (bool)tMod.main.GetField("releaseUI").GetValue(null);
            }
            set
            {
                tMod.main.GetField("releaseUI").SetValue(null, value);
            }
        }

        public static bool GameMenu
        {
            get
            {
                return (bool)tMod.main.GetField("gameMenu").GetValue(null);
            }
            set
            {
                tMod.main.GetField("gameMenu").SetValue(null, value);
            }
        }

        public static bool EditSign
        {
            get
            {
                return (bool)tMod.main.GetField("editSign").GetValue(null);
            }
            set
            {
                tMod.main.GetField("editSign").SetValue(null, value);
            }
        }

        public static Color TileColor
        {
            get
            {
                return (Color)tMod.main.GetField("tileColor").GetValue(null);
            }
            set
            {
                tMod.main.GetField("tileColor").SetValue(null, value);
            }
        }

        public static Random Rand
        {
            get
            {
                return (Random)tMod.main.GetField("rand").GetValue(null);
            }
            set
            {
                tMod.main.GetField("rand").SetValue(null, value);
            }
        }

        public static bool LightTiles
        {
            get
            {
                return (bool)tMod.main.GetField("lightTiles").GetValue(null);
            }
            set
            {
                tMod.main.GetField("lightTiles").SetValue(null, value);
            }
        }

        public static KeyboardState KeyState
        {
            get
            {
                return (KeyboardState)tMod.main.GetField("keyState").GetValue(null);
            }
            set
            {
                tMod.main.GetField("keyState").SetValue(null, value);
            }
        }

        public static GameWindow Window
        {
            get
            {
                return (GameWindow)tMod.main.GetField("Window").GetValue(null);
            }
            set
            {
                tMod.main.GetField("Window").SetValue(null, value);
            }
        }

        public static int MaxHair
        {
            get
            {
                return (int)tMod.main.GetField("maxHair").GetValue(null);
            }
            set
            {
                tMod.main.GetField("maxHair").SetValue(null, value);
            }
        }

        public static bool SkipMenu
        {
            get
            {
                return (bool)tMod.main.GetField("skipMenu").GetValue(null);
            }
            set
            {
                tMod.main.GetField("skipMenu").SetValue(null, value);
            }
        }

        public static bool DedServer
        {
            get
            {
                return (bool)tMod.main.GetField("dedServ").GetValue(null);
            }
            set
            {
                tMod.main.GetField("dedServ").SetValue(null, value);
            }
        }

        public static int Time
        {
            get
            {
                return (int)tMod.main.GetField("time").GetValue(null);
            }
            set
            {
                tMod.main.GetField("time").SetValue(null, value);
            }
        }

        public static bool DayTime
        {
            get
            {
                return (bool)tMod.main.GetField("dayTime").GetValue(null);
            }
            set
            {
                tMod.main.GetField("dayTime").SetValue(null, value);
            }
        }

        public static int SpawnTileX
        {
            get
            {
                return (int)tMod.main.GetField("spawnTileX").GetValue(null);
            }
            set
            {
                tMod.main.GetField("spawnTileX").SetValue(null, value);
            }
        }

        public static int SpawnTileY
        {
            get
            {
                return (int)tMod.main.GetField("spawnTileY").GetValue(null);
            }
            set
            {
                tMod.main.GetField("spawnTileY").SetValue(null, value);
            }
        }

        public static int ScreenWidth
        {
            get
            {
                return (int)tMod.main.GetField("screenWidth").GetValue(null);
            }
            set
            {
                tMod.main.GetField("screenWidth").SetValue(null, value);
            }
        }

        public static int ScreenHeight
        {
            get
            {
                return (int)tMod.main.GetField("screenHeight").GetValue(null);
            }
            set
            {
                tMod.main.GetField("screenHeight").SetValue(null, value);
            }
        }

        public static int EvilTiles
        {
            get
            {
                return (int)tMod.main.GetField("evilTiles").GetValue(null);
            }
            set
            {
                tMod.main.GetField("evilTiles").SetValue(null, value);
            }
        }

        public static int MeteorTiles
        {
            get
            {
                return (int)tMod.main.GetField("meteorTiles").GetValue(null);
            }
            set
            {
                tMod.main.GetField("meteorTiles").SetValue(null, value);
            }
        }

        public static int JungleTiles
        {
            get
            {
                return (int)tMod.main.GetField("jungleTiles").GetValue(null);
            }
            set
            {
                tMod.main.GetField("jungleTiles").SetValue(null, value);
            }
        }

        public static int DungeonTiles
        {
            get
            {
                return (int)tMod.main.GetField("dungeonTiles").GetValue(null);
            }
            set
            {
                tMod.main.GetField("dungeonTiles").SetValue(null, value);
            }
        }

        public static int ChatLength
        {
            get
            {
                return (int)tMod.main.GetField("chatLength").GetValue(null);
            }
            set
            {
                tMod.main.GetField("chatLength").SetValue(null, value);
            }
        }

        public static int MaxTilesX
        {
            get
            {
                return (int)tMod.main.GetField("maxTilesX").GetValue(null);
            }
            set
            {
                tMod.main.GetField("maxTilesX").SetValue(null, value);
            }
        }

        public static int MaxTilesY
        {
            get
            {
                return (int)tMod.main.GetField("maxTilesY").GetValue(null);
            }
            set
            {
                tMod.main.GetField("maxTilesY").SetValue(null, value);
            }
        }

        public static int DrawTime
        {
            get
            {
                return (int)tMod.main.GetField("drawTime").GetValue(null);
            }
            set
            {
                tMod.main.GetField("drawTime").SetValue(null, value);
            }
        }

        public static string StatusText
        {
            get
            {
                return (string)tMod.main.GetField("statusText").GetValue(null);
            }
            set
            {
                tMod.main.GetField("statusText").SetValue(null, value);
            }
        }

        public static string SavePath
        {
            get
            {
                return (string)tMod.main.GetField("SavePath").GetValue(null);
            }
            set
            {
                tMod.main.GetField("SavePath").SetValue(null, value);
            }
        }

        public static int MenuState
        {
            get
            {
                return (int)tMod.main.GetField("menuMode").GetValue(null);
            }
            set
            {
                tMod.main.GetField("menuMode").SetValue(null, value);
            }
        }

        public static int curRelease
        {
            get
            {
                return (int)tMod.main.GetField("curRelease").GetValue(null);
            }
            set
            {
                tMod.main.GetField("curRelease").SetValue(null, value);
            }
        }

        public static double WorldSurface
        {
            get
            {
                return (double)tMod.main.GetField("worldSurface").GetValue(null);
            }
            set
            {
                tMod.main.GetField("worldSurface").SetValue(null, value);
            }
        }

        public static dynamic[] Player
        {
            get
            {
                return (dynamic[])tMod.main.GetField("player").GetValue(null);
            }
            set
            {
                tMod.main.GetField("player").SetValue(null, value);
            }
        }

        public static bool[] TileDungeon
        {
            get
            {
                return (bool[])tMod.main.GetField("tileDungeon").GetValue(null);
            }
            set
            {
                tMod.main.GetField("tileDungeon").SetValue(null, value);
            }
        }

        public static Vector2 ScreenPosition
        {
            get
            {
                return (Vector2)tMod.main.GetField("screenPosition").GetValue(null);
            }
            set
            {
                tMod.main.GetField("screenPosition").SetValue(null, value);
            }
        }

        public static Vector2 ScreenLastPosition
        {
            get
            {
                return (Vector2)tMod.main.GetField("screenLastPosition").GetValue(null);
            }
            set
            {
                tMod.main.GetField("screenLastPosition").SetValue(null, value);
            }
        }

        public static dynamic[,] Tile
        {
            get
            {
                return (object[,])tMod.main.GetField("tile").GetValue(null);
            }
            set
            {
                tMod.main.GetField("tile").SetValue(null, value);
            }
        }

        public static bool[] TileFrameImportant
        {
            get
            {
                return (bool[])tMod.main.GetField("tileFrameImportant").GetValue(null);
            }
            set
            {
                tMod.main.GetField("tileFrameImportant").SetValue(null, value);
            }
        }

        public static bool[] TileSolid
        {
            get
            {
                return (bool[])tMod.main.GetField("tileSolid").GetValue(null);
            }
            set
            {
                tMod.main.GetField("tileSolid").SetValue(null, value);
            }
        }

        public static bool[] TileBlockLight
        {
            get
            {
                return (bool[])tMod.main.GetField("tileBlockLight").GetValue(null);
            }
            set
            {
                tMod.main.GetField("tileBlockLight").SetValue(null, value);
            }
        }

        public static Color[] TeamColor
        {
            get
            {
                return (Color[])tMod.main.GetField("teamColor").GetValue(null);
            }
            set
            {
                tMod.main.GetField("teamColor").SetValue(null, value);
            }
        }

        public static dynamic[] Npc
        {
            get
            {
                return (dynamic[])tMod.main.GetField("npc").GetValue(null);
            }
            set
            {
                tMod.main.GetField("npc").SetValue(null, value);
            }
        }

        public static dynamic[] Gore
        {
            get
            {
                return (dynamic[])tMod.main.GetField("gore").GetValue(null);
            }
            set
            {
                tMod.main.GetField("gore").SetValue(null, value);
            }
        }

        public static dynamic[] Projectile
        {
            get
            {
                return (dynamic[])tMod.main.GetField("projectile").GetValue(null);
            }
            set
            {
                tMod.main.GetField("projectile").SetValue(null, value);
            }
        }

        public static dynamic[] Dust
        {
            get
            {
                return (dynamic[])tMod.main.GetField("dust").GetValue(null);
            }
            set
            {
                tMod.main.GetField("dust").SetValue(null, value);
            }
        }

        public static dynamic[] Item
        {
            get
            {
                return (dynamic[])tMod.main.GetField("item").GetValue(null);
            }
            set
            {
                tMod.main.GetField("item").SetValue(null, value);
            }
        }

        public static dynamic[] ChatLine
        {
            get
            {
                return (dynamic[])tMod.main.GetField("chatLine").GetValue(null);
            }
            set
            {
                tMod.main.GetField("chatLine").SetValue(null, value);
            }
        }

        public static string motd
        {
            get
            {
                return (string)tMod.main.GetField("motd").GetValue(null);
            }
            set
            {
                tMod.main.GetField("motd").SetValue(null, value);
            }
        }

        public static int MyPlayer
        {
            get
            {
                return (int)tMod.main.GetField("myPlayer").GetValue(null);
            }
            set
            {
                tMod.main.GetField("myPlayer").SetValue(null, value);
            }
        }

        public static int NumChatLines
        {
            get
            {
                return (int)tMod.main.GetField("numChatLines").GetValue(null);
            }
            set
            {
                tMod.main.GetField("numChatLines").SetValue(null, value);
            }
        }

        public static double InvasionX
        {
            get
            {
                return (double)tMod.main.GetField("invasionX").GetValue(null);
            }
            set
            {
                tMod.main.GetField("invasionX").SetValue(null, value);
            }
        }

        public static int InvasionWarn
        {
            get
            {
                return (int)tMod.main.GetField("invasionWarn").GetValue(null);
            }
            set
            {
                tMod.main.GetField("invasionWarn").SetValue(null, value);
            }
        }

        public static int InvasionSize
        {
            get
            {
                return (int)tMod.main.GetField("invasionSize").GetValue(null);
            }
            set
            {
                tMod.main.GetField("invasionSize").SetValue(null, value);
            }
        }

        public static int InvasionType
        {
            get
            {
                return (int)tMod.main.GetField("invasionType").GetValue(null);
            }
            set
            {
                tMod.main.GetField("invasionType").SetValue(null, value);
            }
        }

        public static int NetMode
        {
            get
            {
                return (int)tMod.main.GetField("netMode").GetValue(null);
            }
            set
            {
                tMod.main.GetField("netMode").SetValue(null, value);
            }
        }

        public static int MenuMode
        {
            get
            {
                return (int)tMod.main.GetField("menuMode").GetValue(null);
            }
            set
            {
                tMod.main.GetField("menuMode").SetValue(null, value);
            }
        }

        public static string WorldPathName
        {
            get
            {
                return (string)tMod.main.GetField("worldPathName").GetValue(null);
            }
            set
            {
                tMod.main.GetField("worldPathName").SetValue(null, value);
            }
        }

        public static string[] LoadWorldPath
        {
            get
            {
                return (string[])tMod.main.GetField("loadWorldPath").GetValue(null);
            }
            set
            {
                tMod.main.GetField("loadWorldPath").SetValue(null, value);
            }
        }

        public static bool InputTextEnter
        {
            get
            {
                return (bool)tMod.main.GetField("inputTextEnter").GetValue(null);
            }
            set
            {
                tMod.main.GetField("inputTextEnter").SetValue(null, value);
            }
        }

        public static bool ChatRelease
        {
            get
            {
                return (bool)tMod.main.GetField("chatRelease").GetValue(null);
            }
            set
            {
                tMod.main.GetField("chatRelease").SetValue(null, value);
            }
        }

        public static bool FixedTiming
        {
            get
            {
                return (bool)tMod.main.GetField("fixedTiming").GetValue(null);
            }
            set
            {
                tMod.main.GetField("fixedTiming").SetValue(null, value);
            }
        }

        public static bool IgnoreErrors
        {
            get
            {
                return (bool)tMod.main.GetField("ignoreErrors").GetValue(null);
            }
            set
            {
                tMod.main.GetField("ignoreErrors").SetValue(null, value);
            }
        }

        public static string ChatText
        {
            get
            {
                return (string)tMod.main.GetField("chatText").GetValue(null);
            }
            set
            {
                tMod.main.GetField("chatText").SetValue(null, value);
            }
        }

        public static SpriteFont FontMouseText
        {
            get
            {
                return (SpriteFont)tMod.main.GetField("fontMouseText").GetValue(null);
            }
            set
            {
                tMod.main.GetField("fontMouseText").SetValue(null, value);
            }
        }

        public static short SunModY
        {
            get
            {
                return (short)tMod.main.GetField("sunModY").GetValue(null);
            }
            set
            {
                tMod.main.GetField("sunModY").SetValue(null, value);
            }
        }

        public static short MoonModY
        {
            get
            {
                return (short)tMod.main.GetField("moonModY").GetValue(null);
            }
            set
            {
                tMod.main.GetField("moonModY").SetValue(null, value);
            }
        }

        public static void PlaySound(int a, int b, int c, int d)
        {
            tMod.main.GetMethod("PlaySound").Invoke(null, new object[] { a, b, c, d });
        }

        public static string GetInputText(string text)
        {
            return (string)tMod.main.GetMethod("GetInputText").Invoke(null, new object[] { text });
        }

        public static string NextLoadWorld()
        {
            return (string)tMod.main.GetMethod("nextLoadWorld", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[0]);
        }

        public static void LoadWorlds()
        {
            tMod.main.GetMethod("LoadWorlds").Invoke(null, new object[0]);
        }

        public static void LoadPlayers()
        {
            tMod.main.GetMethod("LoadPlayers", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[0]);
        }

        public static void Initialize()
        {
            tMod.main.GetMethod("Initialize", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(main, new object[0]);
        }

        public static void NewText(string a, byte b, byte c, byte d)
        {
            tMod.main.GetMethod("NewText").Invoke(null, new object[] { a, b, c, d });
        }

       
    }
}
