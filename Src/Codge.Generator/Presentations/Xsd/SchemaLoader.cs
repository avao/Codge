using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Codge.Generator.Presentations.Xsd
{
    public static class SchemaLoader
    {
        public static XmlSchema Load(Stream stream)
        {
            return XmlSchema.Read(stream, null);
        }

        public static XmlSchema Load(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return Load(stream);
            }
        }
    }
}
