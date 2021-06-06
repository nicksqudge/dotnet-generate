using System.IO;
using System;
using System.Linq;

namespace DotnetGenerate.Schematics
{
    public class InterfaceAndClassSchematic : Schematic
    {
        public override string LongName => "interfaceclass";

        public override string ShortName => "ic";

        public override string Description => "Create an interface and a class in the same directory that inherits the interface";

        public override string Template()
        {
            throw new NotImplementedException();
        }

        public override int WriteFile(PathBuilderResult path, string visibility, bool isAbstract, bool isStatic, string inherits)
        {
            var interfaceSchematic = new InterfaceSchematic();
            var interfaceResult = FileWriter.Write(
                interfaceSchematic.Template(),
                interfaceSchematic.LongName,
                Path.Combine()
            );
            if (interfaceResult == Program.Fail)
                return Program.Fail;

            string inheritsValue = interfaceSchematic
                .TransformFileName(Path.GetFileNameWithoutExtension(path.FullPath))
                .Replace(".cs", "");

            var classSchematic = new ClassSchematic();
            return classSchematic.WriteFile(path, visibility, isAbstract, false, inheritsValue);
        }
    }
}