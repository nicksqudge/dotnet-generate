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

        public virtual int WriteFile(PathBuilder pathBuilder, FileWriteOptions fileWriteOptions, OpenCommandHandler openCommandHandler)
        {
            pathBuilder.SetFileNameTransform(TransformFileName);

            return FileWriter.Write(
                fileData: Template(),
                schematicName: LongName,
                visibility,
                isAbstract,
                isStatic,
                inherits
            );
        }
    }
}
