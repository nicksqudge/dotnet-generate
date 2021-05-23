using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotnetGenerate
{
    public class PathHandler
    {
        public string FileName { get; set; }
        public string Directory { get; set; }

        private string _expectedSlash { get; set; }
        private string _toReplaceSlash { get; set; }
        private string _startingDir;
        private string _userInput;

        public PathHandler(string startingDir, string userInput)
        {
            if (startingDir.Contains("\\"))
            {
                _expectedSlash = "\\";
                _toReplaceSlash = "/";   
            }
            else
            {
                _expectedSlash = "/";
                _toReplaceSlash = "\\";
            }

            if (startingDir.EndsWith(_expectedSlash) == false)
                startingDir += _expectedSlash;

            if (userInput.EndsWith(".cs") == false)
                userInput += ".cs";

            userInput = userInput.Replace(_toReplaceSlash, _expectedSlash);

            _startingDir = startingDir;
            _userInput = userInput;
        }

        public void Generate()
        {
            string path = Path.Combine(_startingDir, _userInput);

            var sections = ProcessPaths(path);

            Directory = string.Join(_expectedSlash, sections.Take(sections.Count() - 1));
            FileName = sections[sections.Count() - 1];

            FinishFormatting();
        }

        private string[] ProcessPaths(string inputPath)
        {
            var sections = inputPath.Split(_expectedSlash);

            if (sections.Count() < 2)
                return sections;

            int start = sections.Count() - 1;
            var result = new List<string>();

            for (int i = start; i >= 0; i--)
            {
                string section = sections[i];

                if (section == "..")
                    i--;
                else
                    result.Add(section);
            }

            result.Reverse();
            return result.ToArray();
        }

        private void FinishFormatting()
        {
            FileName = char.ToUpper(FileName[0]) + FileName.Substring(1);

            if (Directory.EndsWith(_expectedSlash) == false)
                Directory += _expectedSlash;
        }
    }
}
