using System;

namespace ProjectPSX.Util
{

    static class MathHelper
    {

        public static int Clamp(int value, int min, int max)
        {
            return Math.Min(Math.Max(value, min), max);
        }

    }

}
