using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tMod_v3.Terraria
{
    public class ProjectileMod
    {
        public static bool NewProjectileMod(float X, float Y, float SpeedX, float SpeedY, int Type, int Damage, float KnockBack, int Owner = 255)
        {
            Console.WriteLine("New projectile mod: {0}", Type);
            return true;
        }

        public static bool SetDefaultsMod(dynamic proj, dynamic type)
        {
            if (XeedMod.cprojs != null)
                lock (XeedMod.cprojs)
                    foreach (dynamic cm in XeedMod.cprojs)
                        if (cm.type == type)
                            try
                            {
                                proj.SetDefaults(cm.GetID());
                                cm.ApplyTo(proj);
                                return true;
                            }
                            catch (Exception ex) { Console.WriteLine(ex); }
            return false;
        }
    }
}
