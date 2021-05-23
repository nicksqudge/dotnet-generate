using System;
using System.IO;

namespace DotnetGenerate
{
    public class PathGenerator
    {
        private string _cwd = string.Empty;
        private string _projectPath = string.Empty;
        private string _nameSpace = string.Empty;
        private string _userInput = string.Empty;

        public PathGeneratorResult Generate()
        {
            string directory = GetDirectory();

            return new PathGeneratorResult()
            {
                DisplayPath  = GenerateDisplayPath(),
                FileName = GenerateFileName(),
                FullFilePath = GenerateFullFilePath(),
                Namespace = GenerateNamespace()
            };
        }

        public PathGenerator SetCurrentWorkingDirectory(string path)
        {
            _cwd = Path.GetDirectoryName(path);
            return this;
        }

        public PathGenerator SetProjectPath(string projectPath)
        {
            _projectPath = Path.GetDirectoryName(projectPath);
            return this;
        }

        public PathGenerator SetNamespace(string value)
        {
            _nameSpace = value;
            return this;
        }

        public PathGenerator SetUserInput(string userInput)
        {
            _userInput = userInput;
            return this;
        }

        private string GetDirectory()
        {

        }

        private string GetFileName()
        {
            
        }

        private string GenerateNamespace()
        {
            throw new NotImplementedException();
        }

        private string GenerateFullFilePath()
        {
            throw new NotImplementedException();
        }

        private string GenerateFileName()
        {
            throw new NotImplementedException();
        }

        private string GenerateDisplayPath()
        {
            throw new NotImplementedException();
        }
    }

    public class PathGeneratorResult
    {
        public string Namespace { get; set; }
        public string FullFilePath { get; set; }
        public string FileName { get; set; }
        public string DisplayPath { get; set; }
    }
}
