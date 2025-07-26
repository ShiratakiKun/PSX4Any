using System.Runtime.CompilerServices;

namespace ProjectPSX.Util
{

    static unsafe class MemoryMarshalExt
    {

        public static ref int GetArrayDataReference(int[] array)
        {
            if (array.Length == 0)
                return ref Unsafe.AsRef<int>(null);
            return ref array[0];
        }

        public static ref byte GetArrayDataReference(byte[] array)
        {
            if (array.Length == 0)
                return ref Unsafe.AsRef<byte>(null);
            return ref array[0];
        }

        public static ref uint GetArrayDataReference(uint[] array)
        {
            if (array.Length == 0)
                return ref Unsafe.AsRef<uint>(null);
            return ref array[0];
        }

        public static ref ushort GetArrayDataReference(ushort[] array)
        {
            if (array.Length == 0)
                return ref Unsafe.AsRef<ushort>(null);
            return ref array[0];
        }

    }

}
