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
        public string InputName { get; set; }

        [Option("-d|--dry-run", Description = "Run through and reports activity without writing out results")]
        public bool DryRun { get; set; } = false;

        [Option("-f|--force", Description = "Force overwriting of existing files")]
        public bool Force { get; set; } = false;

        [Option("-v|--visibility")]
        public string Visibility { get; set; } = "public";

        [Option("-a|--abstract")]
        public bool IsAbstract { get; set; } = false;

        private int OnExecute(CommandLineApplication application)
        {
            DirectoryInfo directory = new DirectoryInfo(Environment.CurrentDirectory);

            var project = new ProjectFinder().Find(directory);
            if (project == null)
            {
                WriteErrorLine("Could not find project file");
                return 1;
            }

            // Workout the schematic

            var path = new PathHandler()
                .SetCurrentWorkingDir(directory.FullName)
                .SetNamespace(project.RootNamespace)
                .SetProjectPath(project.ProjectPath)
                .Run(InputName);

            // Does the file exist?
                // If so, are we doing a forced run?

            // Are we doing a dry-run?

            // Find the template and write the file

            return 0;
        }

        private void WriteErrorLine(string error)
        {
            var foreGround = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ForegroundColor = foreGround;
        }
    }
}
