using System;

namespace DotnetGenerate.Schematics
{
    public class InterfaceSchematic : Schematic
    {
        public override string LongName => "interface";

        public override string ShortName => "i";

        public override string TransformFileName(string fileNameWithoutExtension)
        {
            return $"I{fileNameWithoutExtension}.cs";
        }

        protected override string Template()
        {
            return @"using System;

namespace {{namespace}}
{
    {{modifiers}}interface {{name}}{{inherits}}
    {

    }  
}";
        }
    }
}
