using System;
using System.IO;
using System.Linq;

namespace DotnetGenerate
{
    public class DirectoryHandler : IDirectoryHandler
    {
        public FileInfo[] GetFiles(DirectoryInfo directory)
        {
            return directory.GetFiles();
        }
    }  
}