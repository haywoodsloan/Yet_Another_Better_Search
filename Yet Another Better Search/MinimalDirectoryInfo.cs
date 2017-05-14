using System.IO;

namespace Yet_Another_Better_Search
{
    class MinimalDirectoryInfo : MinimalFileSystemInfo
    {
        public MinimalDirectoryInfo(string dirPath)
        {
            LastWriteTime = Directory.GetLastWriteTime(dirPath);
            CreationTime = Directory.GetCreationTime(dirPath);
        }
    }
}
