using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Codge.DataModel.Descriptors.Serialisation
{
    public static class DescriptorStringSerialisationExtensions
    {
        public static string ToXmlString(this FieldDescriptor field)
        {
            var builder = new StringBuilder();
            using (var writer = XmlWriter.Create(builder))
            {
                field.ToXml(writer);
            }
            return builder.ToString();
        }
    }
}
