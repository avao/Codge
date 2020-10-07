using System;
using System.Text;
using System.Xml;

namespace Codge.DataModel.Descriptors.Serialisation
{
    public static class DescriptorStringSerialisationExtensions
    {
        public static string ToXml(this ModelDescriptor descriptor)
        {
            return ToXml(descriptor.ToXml);
        }

        public static string ToXml(this FieldDescriptor descriptor)
        {
            return ToXml(descriptor.ToXml);
        }

        public static string ToXml(this ItemDescriptor descriptor)
        {
            return ToXml(descriptor.ToXml);
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
