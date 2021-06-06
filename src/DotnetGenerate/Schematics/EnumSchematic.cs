using System;
using System.Linq;

namespace DotnetGenerate.Schematics
{
    public class EnumSchematic : Schematic
    {
        public override string LongName => "enum";

        public override string ShortName => "e";

        public override string Description => "Create a C# Enum";

        public override string Template()
        {
            return @"using System;

namespace {{namespace}}
{
    {{modifiers}}enum {{name}}{{inherits}}
    {

    }  
}";
        }
    }
}