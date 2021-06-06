using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotnetGenerate
{
    public class FileWriter
    {
        public static int Write(
            string fileData,
            string schematicName,
            string filePath,
            string namespaceValue,
            string relativePath,
            string visibility,
            bool isAbstract,
            bool isStatic,
            string inherits
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