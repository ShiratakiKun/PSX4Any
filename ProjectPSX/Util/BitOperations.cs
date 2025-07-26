namespace ProjectPSX.Util
{

    public static class BitOperations
    {

        private static readonly int[] Log2DeBruijn = new int[]
        {
            00, 09, 01, 10, 13, 21, 02, 29,
            11, 14, 16, 18, 22, 25, 03, 30,
            08, 12, 20, 28, 15, 17, 24, 07,
            19, 27, 23, 06, 26, 05, 04, 31
        };

        public static int LeadingZeroCount(uint value)
        {
            if (value == 0) return 32;

            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            value |= value >> 16;

            uint debruijn = (value * 0x07C4ACDDu) >> 27;
            return 31 - Log2DeBruijn[debruijn];
        }

        public static int LeadingZeroCount(int value)
        {
            return LeadingZeroCount((uint)value);
        }

    }

}
