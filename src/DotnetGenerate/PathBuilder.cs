using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotnetGenerate
{
    public class PathBuilder
    {
        private string _projectDirectory = string.Empty;
        private string _nameSpaceStartName = string.Empty;
        private string _currentWorkingDir = string.Empty;
        private string _userInput = string.Empty;

        public PathBuilder SetProjectPath(string projectPath)
        {
            _projectDirectory = GetParentDirectory(projectPath) + "/";

            if (_nameSpaceStartName.HasValue() == false)
                _nameSpaceStartName = Path.GetFileNameWithoutExtension(projectPath);

            return this;
        }

        public PathBuilder SetCurrentWorkingDir(string cwd)
        {
            _currentWorkingDir = cwd;
            return this;
        }

        public PathBuilder SetNamespace(string ns)
        {
            _nameSpaceStartName = ns;
            return this;
        }

        public PathBuilder SetInput(string userInput)
        {
            _userInput = userInput;
            return this;
        }

        public PathBuilderResult Build()
        {
            string startingDirectory = _projectDirectory;
            if (_currentWorkingDir.HasValue())
                startingDirectory = _currentWorkingDir;

            if (_userInput.StartsWith("./"))
            {
                startingDirectory = _projectDirectory;
                _userInput = _userInput.Replace("./", "");
            }

            var (directory, fileName) = BuildFullPath(startingDirectory, _userInput);
            string projectParent = GetParentDirectory(_projectDirectory);
            string relativePath = directory.Replace(projectParent, "./");
            string nameSpaceValue = GenerateNameSpace(relativePath);

            return new PathBuilderResult()
            {
                Directory = directory,
                RelativePath = relativePath,
                Namespace = nameSpaceValue,
                FileName = Path.GetFileNameWithoutExtension(fileName)
            };
        }

        private (string Directory, string FileName) BuildFullPath(string startingDir, string userInput)
        {
            string fullPath = Path
                .Combine(startingDir, userInput)
                .Replace("//", "/");

            fullPath = HandleDirectoryUpCharacter(fullPath);

            string directory = GetParentDirectory(fullPath);
            string fileName = Path.GetFileNameWithoutExtension(fullPath) + ".cs";

            return (directory, fileName);
        }

        private string HandleDirectoryUpCharacter(string fullPath)
        {
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
                fullPath = JoinFilePath(result.ToArray());
            }

            return fullPath;
        }

        private string GenerateNameSpace(string relativePath)
        {
            var nameSpaceBlocks = new List<string>();
            string directory = GetParentDirectory(relativePath);
            SplitPath(directory)
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

        private string GetParentDirectory(string path)
        {
            var items = SplitPath(path).SkipLast(1);
            string result = JoinFilePath(items.ToArray());
            return AddEndSlash(result);
        }

        private string AddEndSlash(string directory)
        {
            if (directory.EndsWith("/") == false)
                directory += "/";

            return directory;
        }

        private string JoinFilePath(string[] paths)
            => string.Join("/", paths);
    }
}
