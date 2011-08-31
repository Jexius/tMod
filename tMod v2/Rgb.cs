using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tMod_v3
{
    [Serializable]
    public class Rgb
    {
        public byte R;
        public byte G;
        public byte B;

        public Rgb()
        {
        }

        public Rgb(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public Rgb(byte all)
        {
            R = G = B = all;
        }
    }
}
