using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;

namespace DotnetGenerate
{
    [HelpOption]
    public class Program
    {
        internal static int Success = 0;
        internal static int Fail = 1;

        public static void Main(string[] args)
            => CommandLineApplication.Execute<Program>(args);

        [Argument(0, Description = "The sort of file you want to generate. Use --list to view the current schematics")]
        public string Schematic { get; set; }

        [Argument(1, Description = "The name of the file, you can also include the path. Use ./ for the root of the csproj")]
        public string InputName { get; set; }

        [Option("-d|--dry-run", Description = "Run through and reports activity without writing out results")]
        public bool DryRun { get; set; } = false;

        [Option("-f|--force", Description = "Force overwriting of existing files")]
        public bool Force { get; set; } = false;

        [Option("-v|--visibility", Description = "Set the visibility (public, private, internal) of the class, default is empty")]
        public string Visibility { get; set; } = string.Empty;

        [Option("-a|--abstract", Description = "Add the abstract modifier to the file")]
        public bool IsAbstract { get; set; } = false;

        [Option("-s|--static", Description = "Add the static modifier to the file")]
        public bool IsStatic { get; set; } = false;

        [Option("-ls|--list", Description = "List all schematics")]
        public bool ListSchematics { get; set; } = false;

        [Option("-i|--inherits", Description = "Provide the inheritance data")]
        public string Inherits { get; set; } = string.Empty;

        [Option("-o|--open", Description = "Run open command after creating file")]
        public string OpenCommand { get; set; } = string.Empty;

        private List<Schematic> _schematics = new List<Schematic>()
        {
            new Schematics.ClassSchematic(),
            new Schematics.InterfaceSchematic(),
            new Schematics.EnumSchematic()
        };

        private int OnExecute(CommandLineApplication application)
        {
            try
            {
                if (ListSchematics == true)
                {
                    ShowSchematics();
                    return Success;
                }

                return WriteFile();
            }
            catch(Exception ex)
            {
                return ReturnError(ex.Message);
            }
        }

        private void ShowSchematics()
        {
            Console.WriteLine("Current supported schematics:");
            foreach (var schematic in _schematics)
            {
                Console.WriteLine($"{schematic.LongName} or {schematic.ShortName}");
            }
        }
        
        private int WriteFile()
        {
            DirectoryInfo directory = new DirectoryInfo(Environment.CurrentDirectory);

            var project = new ProjectFinder().Find(directory);
            if (project == null)
                ReturnError("Could not find project file");

            var schematic = FindSchematic();
            if (schematic == null)
                ReturnError($"Could not find a schematic for {Schematic}");

            var path = new PathHandler()
                .SetCurrentWorkingDir(directory.FullName)
                .SetNamespace(project.RootNamespace)
                .SetProjectPath(project.ProjectPath)
                .SetFileNameTransform(schematic.TransformFileName)
                .Run(InputName);

            var openCommand = new OpenCommandHandler(OpenCommand)
                .SetPath(path);

            bool cont = CheckFileExists(path.FullPath);
            if (cont == false)
                return Fail;

            if (DryRun == true)
            {
                Console.WriteLine($"Would write a file to {path.RelativePath} using the template {schematic.LongName}");

                if (openCommand.HasCommand)
                    Console.WriteLine($"Would run this open command: {openCommand.Command}");

                return Success;
            }
            else
            {
                var writeFileResult = schematic.WriteFile(path, Visibility, IsAbstract, IsStatic, Inherits);
                if (writeFileResult == Success && openCommand.HasCommand)
                {
                    if(openCommand.Handle())
                        return Success;
                    else
                        return Fail;
                }
                
                return writeFileResult;
            }
        }

        private bool CheckFileExists(string fullPath)
        {
            if (File.Exists(fullPath))
            {
                if (Force == true)
                {
                    if (DryRun == true)
                        Console.WriteLine("File exists at path, so would be overwritten");
                    else
                        File.Delete(fullPath);

                    return true;
                }
                else
                {
                    if (DryRun == true)
                        WriteError("File exists at path, process would stop here");
                    else
                        return false;
                }
            }
            
            return true;
        }

        private void WriteError(string error)
        {
            var foreGround = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ForegroundColor = foreGround;
        }

        private int ReturnError(string error)
        {
            WriteError(error);
            return Fail;
        }

        private Schematic FindSchematic()
        {
            var lookup = new Dictionary<string, Schematic>();
            _schematics.ForEach(s => {
                lookup.Add(s.LongName, s);
                lookup.Add(s.ShortName, s);
            });

            if (lookup.ContainsKey(Schematic))
                return lookup[Schematic];
            else
                return null;
        }

    }
}
