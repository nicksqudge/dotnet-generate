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

        public OpenCommandHandler SetPath(PathBuilder pathBuilder)
        {
            if (HasCommand == false)
                return this;

            var path = pathBuilder.Build();

            if (Command.Contains("{path}"))
                Command = Command.Replace("{path}", "\"{path.FullPath}\"");
            else
                Command += $" \"{path.FullPath}\"";

            return this;
        }

        public bool Handle()
        {
            Console.WriteLine($"Executing: {Command}");
            Process.Start(ProcessFileName, $"{CommandStart} {Command}");
            return false;
        }
    }
}