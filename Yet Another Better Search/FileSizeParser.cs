using System;

namespace Yet_Another_Better_Search
{
    public enum FileSizeScale
    {
        Bytes,
        KB,
        MB,
        GB,
        TB,
        PB,
        EB
    }

    class FileSizeParser
    {
        public static string ToString(long absSize)
        {
            if (absSize == 0)
            {
                return "0 Bytes";
            }

            int sizeOrder = (int)Math.Log(absSize, 1024);
            double reducedSize = absSize / Math.Pow(1024, sizeOrder);

            return $"{reducedSize.ToString("F2")} {Enum.GetName(typeof(FileSizeScale), sizeOrder)}";
        }
    }
}
