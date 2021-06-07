using System;

namespace DotnetGenerate.Schematics
{
    public class ClassSchematic : Schematic
    {
        public override string LongName => "class";
        public override string ShortName => "c";
        public override string Description => "Create a C# class";
        
        public override string Template()
        {
            return @"using System;
using System.Linq;

namespace {{namespace}}
{
    {{modifiers}}class {{name}}{{inherits}}
    {

    }  
}";
        }
    }
}
