using System;

namespace Yet_Another_Better_Search
{
    abstract class MinimalFileSystemInfo
    {
        public DateTime CreationTime { get; protected set; }
        public DateTime LastWriteTime { get; protected set; }
    }
}
