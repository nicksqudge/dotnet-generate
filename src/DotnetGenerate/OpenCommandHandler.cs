using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;

namespace DotnetGenerate
{
    public class OpenCommandHandler
    {
        public bool HasCommand => Command.HasValue() && ProcessFileName.HasValue();
        public string Command { get; private set; } = string.Empty;
        private string ProcessFileName = string.Empty;
        private string CommandStart = "";
        private PathBuilderResult _path;

        public OpenCommandHandler(string input)
        {
            Command = input;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                ProcessFileName = "cmd.exe";
                CommandStart = @"\c";
            }
            else if(RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                ProcessFileName = "/bin/bash";
                CommandStart = "-c";
            }
        }

        public OpenCommandHandler SetPath(PathBuilderResult path)
        {
            if (HasCommand == false)
                return this;

            _path = path;

            return this;
        }

        public string GetCommand(string fileName)
        {
            if (HasCommand == false)
                return "";

            string filePath = Path.Combine(_path.Directory, fileName);

            if (Command.Contains("{path}"))
                return Command.Replace("{path}", $"\"{filePath}\"");
            else
                return $"{Command} \"{filePath}\"";
        }

        public bool Handle(string fileName)
        {
            if (HasCommand == false)
                return false;

            string command = GetCommand(fileName);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                command = command.Replace("/", "\\");

            Console.WriteLine($"Executing: {command}");
            Process.Start(ProcessFileName, $"{CommandStart} {command}");
            return false;
        }
    }
}