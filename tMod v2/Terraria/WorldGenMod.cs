using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using tMod_v3;

namespace Terraria
{
    public class WorldGenMod
    {
        public static void serverLoadWorld()
        {
            tMod.worldGen.GetMethod("serverLoadWorld").Invoke(null, new object[0]);
        }

        public static Object Padlock
        {
            get
            {
                return (Object)tMod.worldGen.GetField("padlock").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("padlock").SetValue(null, value);
            }
        }


        public static Int32 LavaLine
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("lavaLine").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("lavaLine").SetValue(null, value);
            }
        }


        public static Int32 WaterLine
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("waterLine").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("waterLine").SetValue(null, value);
            }
        }


        public static bool NoTileActions
        {
            get
            {
                return (bool)tMod.worldGen.GetField("noTileActions").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("noTileActions").SetValue(null, value);
            }
        }


        public static bool SpawnEye
        {
            get
            {
                return (bool)tMod.worldGen.GetField("spawnEye").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("spawnEye").SetValue(null, value);
            }
        }


        public static bool Gen
        {
            get
            {
                return (bool)tMod.worldGen.GetField("gen").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("gen").SetValue(null, value);
            }
        }

        public static void CreateNewWorld()
        {
            tMod.worldGen.GetMethod("CreateNewWorld").Invoke(null, new object[0]);
        }

        public static bool ShadowOrbSmashed
        {
            get
            {
                return (bool)tMod.worldGen.GetField("shadowOrbSmashed").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("shadowOrbSmashed").SetValue(null, value);
            }
        }


        public static Int32 ShadowOrbCount
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("shadowOrbCount").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("shadowOrbCount").SetValue(null, value);
            }
        }


        public static bool SpawnMeteor
        {
            get
            {
                return (bool)tMod.worldGen.GetField("spawnMeteor").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("spawnMeteor").SetValue(null, value);
            }
        }


        public static bool LoadFailed
        {
            get
            {
                return (bool)tMod.worldGen.GetField("loadFailed").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("loadFailed").SetValue(null, value);
            }
        }


        public static bool LoadSuccess
        {
            get
            {
                return (bool)tMod.worldGen.GetField("loadSuccess").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("loadSuccess").SetValue(null, value);
            }
        }


        public static bool WorldCleared
        {
            get
            {
                return (bool)tMod.worldGen.GetField("worldCleared").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("worldCleared").SetValue(null, value);
            }
        }


        public static bool WorldBackup
        {
            get
            {
                return (bool)tMod.worldGen.GetField("worldBackup").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("worldBackup").SetValue(null, value);
            }
        }


        public static bool LoadBackup
        {
            get
            {
                return (bool)tMod.worldGen.GetField("loadBackup").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("loadBackup").SetValue(null, value);
            }
        }


        public static Int32 LastMaxTilesX
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("lastMaxTilesX").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("lastMaxTilesX").SetValue(null, value);
            }
        }


        public static Int32 LastMaxTilesY
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("lastMaxTilesY").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("lastMaxTilesY").SetValue(null, value);
            }
        }


        public static bool SaveLock
        {
            get
            {
                return (bool)tMod.worldGen.GetField("saveLock").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("saveLock").SetValue(null, value);
            }
        }


        public static bool MergeUp
        {
            get
            {
                return (bool)tMod.worldGen.GetField("mergeUp").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("mergeUp").SetValue(null, value);
            }
        }


        public static bool MergeDown
        {
            get
            {
                return (bool)tMod.worldGen.GetField("mergeDown").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("mergeDown").SetValue(null, value);
            }
        }


        public static bool MergeLeft
        {
            get
            {
                return (bool)tMod.worldGen.GetField("mergeLeft").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("mergeLeft").SetValue(null, value);
            }
        }


        public static bool MergeRight
        {
            get
            {
                return (bool)tMod.worldGen.GetField("mergeRight").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("mergeRight").SetValue(null, value);
            }
        }


        public static Int32 TempMoonPhase
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("tempMoonPhase").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("tempMoonPhase").SetValue(null, value);
            }
        }


        public static bool TempDayTime
        {
            get
            {
                return (bool)tMod.worldGen.GetField("tempDayTime").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("tempDayTime").SetValue(null, value);
            }
        }


        public static bool TempBloodMoon
        {
            get
            {
                return (bool)tMod.worldGen.GetField("tempBloodMoon").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("tempBloodMoon").SetValue(null, value);
            }
        }


        public static Double TempTime
        {
            get
            {
                return (Double)tMod.worldGen.GetField("tempTime").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("tempTime").SetValue(null, value);
            }
        }


        public static bool StopDrops
        {
            get
            {
                return (bool)tMod.worldGen.GetField("stopDrops").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("stopDrops").SetValue(null, value);
            }
        }


        public static bool NoLiquidCheck
        {
            get
            {
                return (bool)tMod.worldGen.GetField("noLiquidCheck").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("noLiquidCheck").SetValue(null, value);
            }
        }


        public static Random GenRand
        {
            get
            {
                return (Random)tMod.worldGen.GetField("genRand").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("genRand").SetValue(null, value);
            }
        }


        public static String StatusText
        {
            get
            {
                return (String)tMod.worldGen.GetField("statusText").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("statusText").SetValue(null, value);
            }
        }


        public static bool DestroyObject
        {
            get
            {
                return (bool)tMod.worldGen.GetField("destroyObject").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("destroyObject").SetValue(null, value);
            }
        }


        public static Int32 SpawnDelay
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("spawnDelay").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("spawnDelay").SetValue(null, value);
            }
        }


        public static Int32 SpawnNPC
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("spawnNPC").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("spawnNPC").SetValue(null, value);
            }
        }


        public static Int32 MaxRoomTiles
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("maxRoomTiles").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("maxRoomTiles").SetValue(null, value);
            }
        }


        public static Int32 NumRoomTiles
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("numRoomTiles").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("numRoomTiles").SetValue(null, value);
            }
        }


        public static Int32[] RoomX
        {
            get
            {
                return (Int32[])tMod.worldGen.GetField("roomX").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("roomX").SetValue(null, value);
            }
        }


        public static Int32[] RoomY
        {
            get
            {
                return (Int32[])tMod.worldGen.GetField("roomY").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("roomY").SetValue(null, value);
            }
        }


        public static Int32 RoomX1
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("roomX1").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("roomX1").SetValue(null, value);
            }
        }


        public static Int32 RoomX2
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("roomX2").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("roomX2").SetValue(null, value);
            }
        }


        public static Int32 RoomY1
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("roomY1").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("roomY1").SetValue(null, value);
            }
        }


        public static Int32 RoomY2
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("roomY2").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("roomY2").SetValue(null, value);
            }
        }


        public static bool CanSpawn
        {
            get
            {
                return (bool)tMod.worldGen.GetField("canSpawn").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("canSpawn").SetValue(null, value);
            }
        }


        public static bool[] HouseTile
        {
            get
            {
                return (bool[])tMod.worldGen.GetField("houseTile").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("houseTile").SetValue(null, value);
            }
        }


        public static Int32 BestX
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("bestX").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("bestX").SetValue(null, value);
            }
        }


        public static Int32 BestY
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("bestY").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("bestY").SetValue(null, value);
            }
        }


        public static Int32 HiScore
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("hiScore").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("hiScore").SetValue(null, value);
            }
        }


        public static Int32 DungeonX
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("dungeonX").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("dungeonX").SetValue(null, value);
            }
        }


        public static Int32 DungeonY
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("dungeonY").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("dungeonY").SetValue(null, value);
            }
        }


        public static Vector2 LastDungeonHall
        {
            get
            {
                return (Vector2)tMod.worldGen.GetField("lastDungeonHall").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("lastDungeonHall").SetValue(null, value);
            }
        }


        public static Int32 MaxDRooms
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("maxDRooms").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("maxDRooms").SetValue(null, value);
            }
        }


        public static Int32 NumDRooms
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("numDRooms").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("numDRooms").SetValue(null, value);
            }
        }


        public static Int32[] DRoomX
        {
            get
            {
                return (Int32[])tMod.worldGen.GetField("dRoomX").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("dRoomX").SetValue(null, value);
            }
        }


        public static Int32[] DRoomY
        {
            get
            {
                return (Int32[])tMod.worldGen.GetField("dRoomY").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("dRoomY").SetValue(null, value);
            }
        }


        public static Int32[] DRoomSize
        {
            get
            {
                return (Int32[])tMod.worldGen.GetField("dRoomSize").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("dRoomSize").SetValue(null, value);
            }
        }


        public static bool[] DRoomTreasure
        {
            get
            {
                return (bool[])tMod.worldGen.GetField("dRoomTreasure").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("dRoomTreasure").SetValue(null, value);
            }
        }


        public static Int32[] DRoomL
        {
            get
            {
                return (Int32[])tMod.worldGen.GetField("dRoomL").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("dRoomL").SetValue(null, value);
            }
        }


        public static Int32[] DRoomR
        {
            get
            {
                return (Int32[])tMod.worldGen.GetField("dRoomR").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("dRoomR").SetValue(null, value);
            }
        }


        public static Int32[] DRoomT
        {
            get
            {
                return (Int32[])tMod.worldGen.GetField("dRoomT").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("dRoomT").SetValue(null, value);
            }
        }


        public static Int32[] DRoomB
        {
            get
            {
                return (Int32[])tMod.worldGen.GetField("dRoomB").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("dRoomB").SetValue(null, value);
            }
        }


        public static Int32 NumDDoors
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("numDDoors").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("numDDoors").SetValue(null, value);
            }
        }


        public static Int32[] DDoorX
        {
            get
            {
                return (Int32[])tMod.worldGen.GetField("DDoorX").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("DDoorX").SetValue(null, value);
            }
        }


        public static Int32[] DDoorY
        {
            get
            {
                return (Int32[])tMod.worldGen.GetField("DDoorY").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("DDoorY").SetValue(null, value);
            }
        }


        public static Int32[] DDoorPos
        {
            get
            {
                return (Int32[])tMod.worldGen.GetField("DDoorPos").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("DDoorPos").SetValue(null, value);
            }
        }


        public static Int32 NumDPlats
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("numDPlats").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("numDPlats").SetValue(null, value);
            }
        }


        public static Int32[] DPlatX
        {
            get
            {
                return (Int32[])tMod.worldGen.GetField("DPlatX").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("DPlatX").SetValue(null, value);
            }
        }


        public static Int32[] DPlatY
        {
            get
            {
                return (Int32[])tMod.worldGen.GetField("DPlatY").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("DPlatY").SetValue(null, value);
            }
        }


        public static Int32[] JChestX
        {
            get
            {
                return (Int32[])tMod.worldGen.GetField("JChestX").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("JChestX").SetValue(null, value);
            }
        }


        public static Int32[] JChestY
        {
            get
            {
                return (Int32[])tMod.worldGen.GetField("JChestY").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("JChestY").SetValue(null, value);
            }
        }


        public static Int32 NumJChests
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("numJChests").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("numJChests").SetValue(null, value);
            }
        }


        public static Int32 DEnteranceX
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("dEnteranceX").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("dEnteranceX").SetValue(null, value);
            }
        }


        public static bool DSurface
        {
            get
            {
                return (bool)tMod.worldGen.GetField("dSurface").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("dSurface").SetValue(null, value);
            }
        }


        public static Double DxStrength1
        {
            get
            {
                return (Double)tMod.worldGen.GetField("dxStrength1").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("dxStrength1").SetValue(null, value);
            }
        }


        public static Double DyStrength1
        {
            get
            {
                return (Double)tMod.worldGen.GetField("dyStrength1").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("dyStrength1").SetValue(null, value);
            }
        }


        public static Double DxStrength2
        {
            get
            {
                return (Double)tMod.worldGen.GetField("dxStrength2").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("dxStrength2").SetValue(null, value);
            }
        }


        public static Double DyStrength2
        {
            get
            {
                return (Double)tMod.worldGen.GetField("dyStrength2").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("dyStrength2").SetValue(null, value);
            }
        }


        public static Int32 DMinX
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("dMinX").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("dMinX").SetValue(null, value);
            }
        }


        public static Int32 DMaxX
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("dMaxX").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("dMaxX").SetValue(null, value);
            }
        }


        public static Int32 DMinY
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("dMinY").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("dMinY").SetValue(null, value);
            }
        }


        public static Int32 DMaxY
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("dMaxY").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("dMaxY").SetValue(null, value);
            }
        }


        public static Int32 NumIslandHouses
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("numIslandHouses").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("numIslandHouses").SetValue(null, value);
            }
        }


        public static Int32 HouseCount
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("houseCount").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("houseCount").SetValue(null, value);
            }
        }


        public static Int32[] FihX
        {
            get
            {
                return (Int32[])tMod.worldGen.GetField("fihX").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("fihX").SetValue(null, value);
            }
        }


        public static Int32[] FihY
        {
            get
            {
                return (Int32[])tMod.worldGen.GetField("fihY").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("fihY").SetValue(null, value);
            }
        }


        public static Int32 NumMCaves
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("numMCaves").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("numMCaves").SetValue(null, value);
            }
        }


        public static Int32[] MCaveX
        {
            get
            {
                return (Int32[])tMod.worldGen.GetField("mCaveX").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("mCaveX").SetValue(null, value);
            }
        }


        public static Int32[] MCaveY
        {
            get
            {
                return (Int32[])tMod.worldGen.GetField("mCaveY").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("mCaveY").SetValue(null, value);
            }
        }


        public static Int32 JungleX
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("JungleX").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("JungleX").SetValue(null, value);
            }
        }


        public static Int32 HellChest
        {
            get
            {
                return (Int32)tMod.worldGen.GetField("hellChest").GetValue(null);
            }
            set
            {
                tMod.worldGen.GetField("hellChest").SetValue(null, value);
            }
        }

        public static Type WorldGen;

        public static bool forceMeteor(int i, int j)
        {
            /*
            if ((i < 50) || (i > (Main.maxTilesX - 50)))
            {
                return false;
            }
            if ((j < 50) || (j > (Main.maxTilesY - 50)))
            {
                return false;
            }
            int num = 0x19;
            FieldInfo stopDrops = typeof(WorldGen).GetField("stopDrops");
            stopDrops.SetValue(null, true);
            num = 15;
            for (int num6 = i - num; num6 < (i + num); num6++)
            {
                for (int num7 = j - num; num7 < (j + num); num7++)
                {
                    if ((num7 > ((j + Main.rand.Next(-2, 3)) - 5)) && ((Math.Abs((int)(i - num6)) + Math.Abs((int)(j - num7))) < ((num * 1.5) + Main.rand.Next(-5, 5))))
                    {
                        if (!Main.tileSolid[Main.tile[num6, num7].type])
                        {
                            Main.tile[num6, num7].active = false;
                        }
                        Main.tile[num6, num7].type = 0x25;
                    }
                }
            }
            num = 10;
            for (int num8 = i - num; num8 < (i + num); num8++)
            {
                for (int num9 = j - num; num9 < (j + num); num9++)
                {
                    if ((num9 > ((j + Main.rand.Next(-2, 3)) - 5)) && ((Math.Abs((int)(i - num8)) + Math.Abs((int)(j - num9))) < (num + Main.rand.Next(-3, 4))))
                    {
                        Main.tile[num8, num9].active = false;
                    }
                }
            }
            num = 0x10;
            for (int num10 = i - num; num10 < (i + num); num10++)
            {
                for (int num11 = j - num; num11 < (j + num); num11++)
                {
                    if ((Main.tile[num10, num11].type == 5) || (Main.tile[num10, num11].type == 0x20))
                    {
                        WorldGen.KillTile(num10, num11, false, false, false);
                    }
                    WorldGen.SquareTileFrame(num10, num11, true);
                    WorldGen.SquareWallFrame(num10, num11, true);
                }
            }
            num = 0x17;
            for (int num12 = i - num; num12 < (i + num); num12++)
            {
                for (int num13 = j - num; num13 < (j + num); num13++)
                {
                    if ((Main.tile[num12, num13].active && (Main.rand.Next(10) == 0)) && ((Math.Abs((int)(i - num12)) + Math.Abs((int)(j - num13))) < (num * 1.3)))
                    {
                        if ((Main.tile[num12, num13].type == 5) || (Main.tile[num12, num13].type == 0x20))
                        {
                            WorldGen.KillTile(num12, num13, false, false, false);
                        }
                        Main.tile[num12, num13].type = 0x25;
                        WorldGen.SquareTileFrame(num12, num13, true);
                    }
                }
            }
            stopDrops.SetValue(null, false);
            if (Main.netMode == 0)
            {
                Main.NewText("A meteorite has landed!", 50, 0xff, 130);
            }
            else if (Main.netMode == 2)
            {
                NetMessage.SendData(0x19, -1, -1, "A meteorite has landed!", 8, 50f, 255f, 130f);
            }
            if (Main.netMode != 1)
            {
                NetMessage.SendTileSquare(-1, i, j, 30);
            }
            return true;
            */
            return false;
        }

        public static void KillTile(int x, int y, bool fail = false, bool effectOnly = false, bool noItem = false)
        {
            tMod.worldGen.GetMethod("KillTile").Invoke(null, new object[] { x, y, fail, effectOnly, noItem });
        }

        public static void FloatingIsland(int x, int y)
        {
            tMod.worldGen.GetMethod("FloatingIsland").Invoke(null, new object[] { x, y });
        }

        public static void SaveWorld(bool resetTime)
        {
            tMod.worldGen.GetMethod("saveWorld").Invoke(null, new object[] { resetTime });
        }

        public static void PlaceTile(int x, int y, int type, bool mute, bool forced, int plr, int style = 0)
        {
            tMod.worldGen.GetMethod("PlaceTile").Invoke(null, new object[] { x, y, type, mute, forced, plr, style });
        }

        public static void GenerateWorld(int seed = -1)
        {
            tMod.worldGen.GetMethod("generateWorld").Invoke(null, new object[] { seed });
        }

        public static void KillWall(int x, int y, bool fail)
        {
            tMod.worldGen.GetMethod("KillWall").Invoke(null, new object[] { x, y, fail });
        }

        public static void PlaceWall(int x, int y, int type, bool mute)
        {
            tMod.worldGen.GetMethod("PlaceWall").Invoke(null, new object[] { x, y, type, mute });
        }

        public static void RangeFrame(int startx, int starty, int endx, int endy)
        {
            tMod.worldGen.GetMethod("RangeFrame").Invoke(null, new object[] { startx, starty, endx, endy });
        }

        public static void ClearWorld()
        {
            tMod.worldGen.GetMethod("clearWorld").Invoke(null, new object[0]);
        }
    }
}
