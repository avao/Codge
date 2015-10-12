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
        public static string ToXml(this FieldDescriptor field)
        {
            return ToXml(_ => field.ToXml(_));
        }

        public static string ToXml(this ItemDescriptor item)
        {
            return ToXml(_ => item.ToXml(_));
        }

        private static string ToXml(Action<XmlWriter> action)
        {
            var builder = new StringBuilder();
            using (var writer = XmlWriter.Create(builder))
            {
                action(writer);
            }
            return builder.ToString();
        }
    }
}
