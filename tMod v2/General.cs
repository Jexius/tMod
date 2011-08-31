using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace tMod_v3
{
    public class General
    {
        public dynamic[] Player
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

        public dynamic[,] Tile
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
    }
}
