using System;

namespace DotnetGenerate.Schematics
{
    public class InterfaceSchematic : Schematic
    {
        public override string LongName => "interface";

        public override string ShortName => "i";

        public override string Description => "Create a C# Interface";

        public override string TransformFileName(string fileNameWithoutExtension)
        {
            return $"I{fileNameWithoutExtension}.cs";
        }

        public override string Template()
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
