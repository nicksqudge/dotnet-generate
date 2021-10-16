using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace DotnetGenerate
{
    public class FindProject
    {
        private readonly IDirectoryHandler _directoryHandler;
        private readonly ICsProjectFileReader _csProjectReader;

        public FindProject(IDirectoryHandler dirHandler, ICsProjectFileReader csProjectReader)
        {
            _directoryHandler = dirHandler;
            _csProjectReader = csProjectReader;
        }

        private readonly List<string> ExclusionList = new List<string>()
        {
            "Unity.",
            "UnityEngine."
        };

        public FindProjectResult InDirectory(DirectoryInfo currentWorkingDir)
        {
            var csProjFile = CheckInDirectory(currentWorkingDir);

            if (csProjFile == null)
                return null;

            string rootNamespace = GetFromCsProject(csProjFile);

            return new FindProjectResult()
            {
                ProjectPath = csProjFile.FullName,
                RootNamespace = rootNamespace
            };
        }

        private string GetFromCsProject(FileInfo projFile)
        {
            string rootNamespace = _csProjectReader.CheckForRootNamespace(projFile);

            if (rootNamespace == "")
                rootNamespace = Path.GetFileNameWithoutExtension(projFile.Name);

            return rootNamespace;
        }

        private FileInfo CheckInDirectory(DirectoryInfo dir)
        {
            var file = FindCsProjFile(dir);

            if (file != null)
                return file;

            if (dir.Parent != null)
                return CheckInDirectory(dir.Parent);

            return null;
        }

        private FileInfo FindCsProjFile(DirectoryInfo directory)
        {
            var files = _directoryHandler.GetFiles(directory);

            return files
                .Where(f => IsInExclusionList(f.FullName) == false)
                .FirstOrDefault(f => f.FullName.EndsWith(".csproj"));
        }

        private bool IsInExclusionList(string filePath)
        {
            return this.ExclusionList.Any(e => filePath.StartsWith(e));
        }
    }

    public class FindProjectResult
    {
        public string ProjectPath { get; set; }
        public string RootNamespace { get; set; }
    }
}
