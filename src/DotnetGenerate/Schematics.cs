using System;
using System.Collections.Generic;

namespace DotnetGenerate
{
    public abstract class Schematic
    {
        public abstract string LongName { get; } 
        public abstract string ShortName { get; }

        public virtual string TransformFileName(string fileNameWithoutExtension)
        {
            return fileNameWithoutExtension+".cs";
        }

        protected abstract string Template();

        public string TransformTemplate(Dictionary<string, string> values)
        {
            string template = Template();

            foreach(var val in values)
                template = template.Replace("{{" + val.Key + "}}", val.Value);

            return template;
        }
    }
}
