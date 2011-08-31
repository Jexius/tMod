using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;

namespace tMod_v3
{
    public class Edit
    {
        public int SessionId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public byte Action { get; set; }
        public byte Type { get; set; }

        public void Rollback()
        {
            switch (Action)
            {
                case 0:
                case 4:
                    if (Type == 2)
                        WorldGenMod.PlaceTile(X, Y, 0, true, true, -1);
                    else
                        WorldGenMod.PlaceTile(X, Y, Type, true, true, -1);
                    MainMod.Tile[X, Y].type = Type;
                    break;

                case 1:
                    WorldGenMod.KillTile(X, Y, false, false, true);
                    break;

                case 2:
                    WorldGenMod.PlaceWall(X, Y, Type, true);
                    MainMod.Tile[X, Y].wall = Type;
                    break;

                case 3:
                    WorldGenMod.KillWall(X, Y, false);
                    break;

                default:
                    return;
            }
            NetMessageMod.SendTileSquare(-1, X, Y, 3);
        }

        public static byte ReverseAction(byte action)
        {
            switch (action)
            {
                case 0:
                case 4:
                    return 1;

                case 1:
                    return 0;

                case 2:
                    return 3;

                case 3:
                    return 2;

                default:
                    return action;
            }
        }
    }
}
