using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotnetGenerate
{
    public class PathHandler
    {
        private string _projectPath = string.Empty;
        private string _projectDirectory = string.Empty;
        private string _nameSpaceStartName = string.Empty;
        private string _currentWorkingDir = string.Empty;

        public PathHandler SetProjectPath(string projectPath)
        {
            _projectPath = projectPath;
            _projectDirectory = Path.GetDirectoryName(projectPath) + "/";

            if (_nameSpaceStartName.HasValue() == false)
                _nameSpaceStartName = Path.GetFileNameWithoutExtension(projectPath);

            return this;
        }

        public PathHandler SetCurrentWorkingDir(string cwd)
        {
            _currentWorkingDir = cwd;
            return this;
        }

        public PathHandler SetNamespace(string ns)
        {
            _nameSpaceStartName = ns;
            return this;
        }

        public PathHandlerResult Run(string userInput)
        {
            if (userInput.EndsWith(".cs") == false)
                userInput += ".cs";

            string startingDirectory = _projectDirectory;
            if (_currentWorkingDir.HasValue())
                startingDirectory = _currentWorkingDir;

            if (userInput.StartsWith("./"))
            {
                startingDirectory = _projectDirectory;
                userInput = userInput.Replace("./", "");
            }

            string fullPath = BuildFullPath(startingDirectory, userInput);
            string projectParent = Path.GetDirectoryName(_projectDirectory) + "/";
            string relativePath = fullPath.Replace(projectParent, "./");
            string nameSpaceValue = GenerateNameSpace(relativePath);

            return new PathHandlerResult()
            {
                FullPath = fullPath,
                RelativePath = relativePath,
                Namespace = nameSpaceValue
            };
        }

        private string BuildFullPath(string startingDir, string userInput)
        {
            string fullPath = Path.Combine(startingDir, userInput);

            if (fullPath.Contains("../"))
            {
                var parts = SplitPath(fullPath).ToList();
                parts.Reverse();

                var result = new List<string>();
                int skip = 0;
                for (int i = 0; i < parts.Count(); i++)
                {
                    string part = parts[i];
                    if (part.Contains(".."))
                        skip++;
                    else
                    {
                        if (skip == 0)
                            result.Add(part);
                        else
                            skip--;
                    }
                }

                result.Reverse();
                fullPath = string.Join("/", result);
            }

            return fullPath;
        }

        private string GenerateNameSpace(string relativePath)
        {
            var nameSpaceBlocks = new List<string>();
            SplitPath(Path.GetDirectoryName(relativePath))
                .ToList()
                .ForEach(part =>
                {
                    if (part.HasValue() && part.Contains(".") == false)
                        nameSpaceBlocks.Add(part);
                });

            string nameSpaceValue = _nameSpaceStartName;
            if (nameSpaceBlocks.Count() > 0)
                nameSpaceValue += "." + string.Join(".", nameSpaceBlocks);

            return nameSpaceValue;
        }

        private string[] SplitPath(string path)
            => path.Split(new string[] { "\\", "/" }, StringSplitOptions.None);
    }

    public class PathHandlerResult
    {
        public string RelativePath { get; set; }
        public string FullPath { get; set; }
        public string Namespace { get; set; }
    }
}
