using System.Runtime.Serialization.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotnetGenerate
{
    public class FileWriter
    {
        public static int Write(string filePath, string fileData, string fileDisplayName, FileWriteOptions fileWriteOptions)
        {
            if (fileWriteOptions.IsDryRun)
                return DryRun(filePath, fileData, fileDisplayName, fileWriteOptions);
            else
                return WriteFile(filePath, fileData, fileDisplayName, fileWriteOptions);
        }

        public static string BuildFileData(string name, string namespaceValue, string templateFileData, FileWriteOptions fileWriteOptions)
        {
            var variables = new Dictionary<string, string>();
            variables.Add("name", name);
            variables.Add("namespace", namespaceValue);

            var modifiers = new List<string>();

            if (fileWriteOptions.Visibility.HasValue())
                modifiers.Add(fileWriteOptions.Visibility);

            if (fileWriteOptions.IsAbtract)
                modifiers.Add("abstract");

            if (fileWriteOptions.IsStatic)
                modifiers.Add("static");

            if (modifiers.Any() == false)
                variables.Add("modifiers", "");
            else
                variables.Add("modifiers", string.Join(" ", modifiers) + " ");

            if (fileWriteOptions.Inherits.HasValue())
                variables.Add("inherits", $" : {fileWriteOptions.Inherits}");
            else
                variables.Add("inherits", "");

            foreach(var val in variables)
                templateFileData = templateFileData.Replace("{{" + val.Key + "}}", val.Value);

            return templateFileData;
        }

        private static int DryRun(string filePath, string fileData, string fileDisplayName, FileWriteOptions fileWriteOptions)
        {
            if (File.Exists(filePath))
            {
                if (fileWriteOptions.UseForce)
                {
                    Console.WriteLine($"File exists, would overwrite: {fileDisplayName}");
                    return Program.Success;
                }
                else
                {
                    Console.WriteLine($"File exists, wouldn't write: {fileDisplayName}");
                    return Program.Fail;
                }
            }
            else
            {
                Console.WriteLine($"Writing file {fileDisplayName}");
                return Program.Success;
            }
        }

        private static int WriteFile(string filePath, string fileData, string fileDisplayName, FileWriteOptions fileWriteOptions)
        {
            if (File.Exists(filePath))
            {
                if (fileWriteOptions.UseForce)
                {
                    WriteFile(filePath, fileData);
                    Console.WriteLine($"File existed, overwritten {fileDisplayName}");
                    return Program.Success;
                }
                else
                {
                    Console.WriteLine($"File already exists. Use --force to overwrite");
                    return Program.Fail;
                }
            }
            else
            {
                WriteFile(filePath, fileData);
                Console.WriteLine($"File created: {fileDisplayName}");
                return Program.Success;
            }
        }

        private static void WriteFile(string filePath, string fileData)
        {
            string dir = Path.GetDirectoryName(filePath);
            if (Directory.Exists(dir) == false)
                Directory.CreateDirectory(dir);

            File.WriteAllText(filePath, fileData);
        }
    }  
}