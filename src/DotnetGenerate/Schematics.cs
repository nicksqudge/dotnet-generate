using System.IO;
using System;
using System.Collections.Generic;

namespace DotnetGenerate
{
    public abstract class Schematic
    {
        public abstract string LongName { get; } 
        public abstract string ShortName { get; }
        public abstract string Description { get; }

        public virtual string TransformFileName(string fileNameWithoutExtension)
        {
            return fileNameWithoutExtension+".cs";
        }

        public abstract string Template();

        public virtual string WriteFile(PathBuilderResult path, FileWriteOptions fileWriteOptions)
        {
            string fileName = TransformFileName(path.FileName);

            string fileData = FileWriter.BuildFileData(
                name: Path.GetFileNameWithoutExtension(fileName),
                namespaceValue: path.Namespace,
                templateFileData: Template(),
                fileWriteOptions
            );

            int result = FileWriter.Write(
                filePath: Path.Combine(path.Directory, fileName),
                fileData,
                fileDisplayName: Path.Combine(path.RelativePath, fileName),
                fileWriteOptions
            );

            if (result == Program.Fail)
                return "";

            return fileName;
        }
    }
}
