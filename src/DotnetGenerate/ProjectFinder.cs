using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace DotnetGenerate
{
    public class ProjectFinder
    {
        public ProjectFinderResult Find(DirectoryInfo currentWorkingDir)
        {
            var csProjFile = CheckInDirectory(currentWorkingDir);

            if (csProjFile == null)
                return null;

            string rootNamespace = CheckForRootNamespace(csProjFile);

            return new ProjectFinderResult()
            {
                ProjectPath = csProjFile.FullName,
                RootNamespace = rootNamespace
            };
        }

        private string CheckForRootNamespace(FileInfo projFile)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(projFile.FullName);

                XmlElement root = doc.DocumentElement;
                XmlNode node = root.SelectSingleNode("//Project/PropertyGroup/RootNamespace");
                return node.InnerText;
            }
            catch
            {
                return Path.GetFileNameWithoutExtension(projFile.Name);
            }
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
            var files = directory.GetFiles();

            return files.FirstOrDefault(f => f.FullName.EndsWith(".csproj"));
        }
    }

    public class ProjectFinderResult
    {
        public string ProjectPath { get; set; }
        public string RootNamespace { get; set; }
    }
}
