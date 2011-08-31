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
    }
}
