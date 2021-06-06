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

        public virtual int WriteFile(PathHandlerResult path, string visibility, bool isAbstract, bool isStatic, string inherits)
        {
            return FileWriter.Write(
                fileData: Template(),
                schematicName: LongName,
                filePath: path.FullPath,
                namespaceValue: path.Namespace,
                relativePath: path.RelativePath,
                visibility,
                isAbstract,
                isStatic,
                inherits
            );
        }
    }
}
