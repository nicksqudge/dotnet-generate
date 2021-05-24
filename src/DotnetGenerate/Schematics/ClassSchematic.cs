using System;

namespace DotnetGenerate.Schematics
{
    public class ClassSchematic : Schematic
    {
        public override string LongName => "class";
        public override string ShortName => "c";
        
        protected override string Template()
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
