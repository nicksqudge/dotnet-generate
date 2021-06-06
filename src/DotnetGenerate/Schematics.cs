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

        public virtual int WriteFile(PathBuilder pathBuilder, FileWriteOptions fileWriteOptions)
        {
            pathBuilder.SetFileNameTransform(TransformFileName);

            var path = pathBuilder.Build();

            string fileData = FileWriter.BuildFileData(
                name: Path.GetFileNameWithoutExtension(path.FullPath),
                namespaceValue: path.Namespace,
                templateFileData: Template(),
                fileWriteOptions
            );

            return FileWriter.Write(
                path.FullPath,
                fileData,
                path.RelativePath,
                fileWriteOptions
            );
        }
    }
}
