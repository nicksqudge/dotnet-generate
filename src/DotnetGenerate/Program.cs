using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;

namespace DotnetGenerate
{
    [HelpOption]
    public class Program
    {
        public static void Main(string[] args)
            => CommandLineApplication.Execute<Program>(args);

        [Argument(0, Description = "The sort of file you want to generate. Supported types, you can use the short hand in square brackets: class [c], interface [i], enum [e]")]
        public string Schematic { get; set; }

        [Argument(1, Description = "The name of the file, you can also include the path. Use ./ for the root of the csproj")]
        public string Name { get; set; }

        [Option("-d|--dry-run", Description = "Run through and reports activity without writing out results")]
        public bool DryRun { get; set; } = false;

        [Option("-f|--force", Description = "Force overwriting of existing files")]
        public bool Force { get; set; } = false;

        private int OnExecute(CommandLineApplication application)
        {
            DirectoryInfo directory = new DirectoryInfo(Environment.CurrentDirectory);

            

            return 0;
        }
    }
}
