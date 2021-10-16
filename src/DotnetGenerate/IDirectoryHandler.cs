using System;
using System.IO;

namespace DotnetGenerate
{
    public interface IDirectoryHandler
    {
        FileInfo[] GetFiles(DirectoryInfo directory);
    }  
}