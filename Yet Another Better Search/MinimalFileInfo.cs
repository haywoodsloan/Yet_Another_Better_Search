using System.IO;

namespace Yet_Another_Better_Search
{
    class MinimalFileInfo : MinimalFileSystemInfo
    {
        public MinimalFileInfo(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            LastWriteTime = fileInfo.LastWriteTime;
            CreationTime = fileInfo.CreationTime;
            Length = fileInfo.Length;
        }

        public long Length { get; private set; }
    }
}
