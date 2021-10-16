using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace DotnetGenerate
{
    public class CsProjectFileReader : ICsProjectFileReader
    {
        public string CheckForRootNamespace(FileInfo projFile)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(projFile.FullName);

                XmlElement root = doc.DocumentElement;
                XmlNode node = root.SelectSingleNode("//Project/PropertyGroup/RootNamespace");
                return node.InnerText;
            }
            catch
            {
                return "";
            }
        }
    }  
}