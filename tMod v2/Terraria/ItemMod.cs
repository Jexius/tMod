using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria
{
    public class ItemMod
    {
        public static Type Item;

        public static int NewItem(int a, int b, int c, int d, int e, int f, bool g = false)
        {
            return (int)Item.GetMethod("NewItem").Invoke(null, new object[] { a, b, c, d, e, f, g });
        }

        public static void SetDefaultsMod(dynamic inst, int type)
        {/*
            if (GlobalSettings.Default.ForceAutoReuse)
            {
                inst.autoReuse = true;
            }
            if (GlobalSettings.Default.ForceShootSpeed >= 0)
            {
                //inst.shootSpeed = GlobalSettings.Default.ForceShootSpeed;
                //inst.useTime = 100;
                //inst.shoot = 0;
            }*/
        }
    }
}
