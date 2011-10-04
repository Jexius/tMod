using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria
{
    public class ItemMod
    {
        public static Type Item;

        public static int NewItem(int X, int Y, int Width, int Height, int Type, int Stack = 1, bool noBroadcast = false)
        {
            return (int)Item.GetMethod("NewItem").Invoke(null, new object[] { X, Y, Width, Height, Type, Stack, noBroadcast });
        }

        public static bool SetDefaultsMod(string name, dynamic item)
        {
            foreach (dynamic ci in XeedMod.GetItems().ToArray())
                if (ci.name.Equals(name))
                    try
                    {
                        item.SetDefaults(ci.GetID(), true);
                        ci.ApplyTo(item);
                        return true;
                    }
                    catch (Exception ex) { Console.WriteLine(ex); }
            return false;
        }
    }
}
