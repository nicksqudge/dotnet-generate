using System;
using System.IO;

namespace DotnetGenerate
{
    public interface ICsProjectFileReader
    {
        string CheckForRootNamespace(FileInfo projFile);
    }  
}