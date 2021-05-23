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
        private string _projectName = string.Empty;

        public PathHandler SetProjectPath(string projectPath)
        {
            _projectPath = projectPath;
            _projectDirectory = Path.GetDirectoryName(projectPath) + "/";
            _projectName = Path.GetFileNameWithoutExtension(projectPath);
            return this;
        }

        public PathHandlerResult Run(string userInput)
        {
            if (userInput.EndsWith(".cs") == false)
                userInput += ".cs";

            string fullPath = Path.Combine(_projectDirectory, userInput);
            string projectParent = Path.GetDirectoryName(_projectDirectory) + "/";
            string relativePath = fullPath.Replace(projectParent, "./");

            var nameSpaceBlocks = new List<string>();
            SplitPath(Path.GetDirectoryName(relativePath))
                .ToList()
                .ForEach(part => {
                    if (part.HasValue() && part.Contains(".") == false)
                        nameSpaceBlocks.Add(part);
                });

            string nameSpaceValue = _projectName;
            if (nameSpaceBlocks.Count() > 0)
                nameSpaceValue += "." + string.Join(".", nameSpaceBlocks);

            return new PathHandlerResult()
            {
                FullPath = fullPath,
                RelativePath = relativePath,
                Namespace = nameSpaceValue
            };
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
