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

        public override string WriteFile(PathBuilderResult path, FileWriteOptions fileWriteOptions)
        {
            var interfaceSchematic = new InterfaceSchematic();
            var interfaceResult = interfaceSchematic.WriteFile(
                path,
                new FileWriteOptions()
                {
                    Inherits = fileWriteOptions.Inherits,
                    IsDryRun = fileWriteOptions.IsDryRun,
                    UseForce = fileWriteOptions.UseForce,
                    Visibility = fileWriteOptions.Visibility
                }
            );
            if (interfaceResult.HasValue() == false)
                return "";

            var classSchematic = new ClassSchematic();
            return classSchematic.WriteFile(
                path,
                new FileWriteOptions()
                {
                    Inherits = Path.GetFileNameWithoutExtension(interfaceSchematic.TransformFileName(path.FileName)),
                    IsDryRun = fileWriteOptions.IsDryRun,
                    UseForce = fileWriteOptions.UseForce,
                    Visibility = fileWriteOptions.Visibility
                }
            );
        }
    }
}