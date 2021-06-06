using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotnetGenerate
{
    public class FileWriter
    {
        private PathBuilder _pathBuilder;
        private Schematic _schematic;
        private FileWriteOptions _fileWriteOptions;
        private OpenCommandHandler _openCommand;

        public FileWriter()
        {
            _fileWriteOptions = new FileWriteOptions();
        }

        public FileWriter SetPathBuilder(PathBuilder pathBuilder)
        {
            _pathBuilder = pathBuilder;
            return this;
        }

        public FileWriter SetSchematic(Schematic schematic)
        {
            _schematic = schematic;
            return this;
        }

        public FileWriter SetAbstract()
        {
            _fileWriteOptions.IsAbtract = true;
            return this;
        }

        public FileWriter SetStatic()
        {
            _fileWriteOptions.IsStatic = true;
            return this;
        }

        public FileWriter SetVisibility(string visibility)
        {
            _fileWriteOptions.Visibility = visibility;
            return this;
        }

        public FileWriter SetDryRun()
        {
            _fileWriteOptions.IsDryRun = true;
            return this;
        }

        public FileWriter SetOpenCommand(OpenCommandHandler openCommand)
        {
            _openCommand = openCommand;
            return this;
        }

        public static int Write(
            string fileData,
            string schematicName,
            PathBuilder pathHandler,
            FileWriteOptions options
        )
        { 
            var variables = new Dictionary<string, string>();
            variables.Add("name", Path.GetFileNameWithoutExtension(filePath));
            variables.Add("namespace", namespaceValue);

            var modifiers = new List<string>();

            if (visibility.HasValue())
                modifiers.Add(visibility);

            if (isAbstract == true)
                modifiers.Add("abstract");

            if (isStatic == true)
                modifiers.Add("static");

            if (modifiers.Any() == false)
                variables.Add("modifiers", "");
            else
                variables.Add("modifiers", string.Join(" ", modifiers) + " ");

            if (inherits.HasValue())
                variables.Add("inherits", $" : {inherits}");
            else
                variables.Add("inherits", "");

            string dir = Path.GetDirectoryName(filePath);
            if (Directory.Exists(dir) == false)
                Directory.CreateDirectory(dir);

            foreach(var val in variables)
                fileData = fileData.Replace("{{" + val.Key + "}}", val.Value);

            File.WriteAllText(filePath, fileData);
            Console.WriteLine($"Wrote file {relativePath} using the template {schematicName}");

            return Program.Success;
        }
    }  
}