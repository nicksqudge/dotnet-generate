using System;
using System.Linq;

namespace DotnetGenerate
{
    public class FileWriteOptions
    {
        public bool IsAbtract { get; set; }
        public bool IsStatic { get; set; }
        public string Visibility { get; set; }
        public bool IsDryRun { get; set; }
        public bool UseForce { get; set; }
        public string Inherits { get; set; }
    }  
}