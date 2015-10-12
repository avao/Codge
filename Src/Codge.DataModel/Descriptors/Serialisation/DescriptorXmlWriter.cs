using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Codge.DataModel.Descriptors.Serialisation
{
    public static class DescriptorXmlWriter
    {
        public static void Write(XmlWriter writer, ModelDescriptor descriptor)
        {
            writer.WriteStartElement("Model", "http://codge/Model.xsd");
            writer.WriteAttributeString("name", descriptor.Name);
            Write(writer, descriptor.RootNamespace);
            writer.WriteEndElement();
        }

        public static void Write(XmlWriter writer, NamespaceDescriptor descriptor)
        {
            writer.WriteStartElement("Namespace");
            writer.WriteAttributeString("name", descriptor.Name);
            foreach(var type in descriptor.Types)
            {
                var composite = type as CompositeTypeDescriptor;
                if(composite != null)
                {
                    WriteComposite(writer, composite);
                    continue;
                }
                var enumeration = type as EnumerationTypeDescriptor;
                if(enumeration != null)
                {
                    WriteEnumeration(writer, enumeration);
                    continue;
                }
                var primitive = type as PrimitiveTypeDescriptor;
                if(primitive != null)
                {
                    WritePrimitive(writer, primitive);
                    continue;
                }
                throw new NotSupportedException("Not supported type " + type.GetType().Name);
            }

            foreach(var ns in descriptor.Namespaces)
            {
                Write(writer, ns);
            }
            writer.WriteEndElement();
        }

        public static void WritePrimitive(XmlWriter writer, PrimitiveTypeDescriptor descriptor)
        {
            writer.WriteStartElement("Primitive");
            writer.WriteAttributeString("name", descriptor.Name);
            writer.WriteEndElement();
        }

        public static void WriteEnumeration(XmlWriter writer, EnumerationTypeDescriptor descriptor)
        {
            writer.WriteStartElement("Enumeration");
            writer.WriteAttributeString("name", descriptor.Name);
            foreach (var item in descriptor.Items)
            {
                WriteItem(writer, item);
            }
            writer.WriteEndElement();
        }

        public static void WriteItem(XmlWriter writer, ItemDescriptor descriptor)
        {
            writer.WriteStartElement("Item");
            writer.WriteAttributeString("name", descriptor.Name);
            if (descriptor.Value.HasValue)
                writer.WriteAttributeString("value", descriptor.Value.Value.ToString());
            writer.WriteEndElement();
        }

        public static void WriteComposite(XmlWriter writer, CompositeTypeDescriptor descriptor)
        {
            writer.WriteStartElement("Composite");
            writer.WriteAttributeString("name", descriptor.Name);
            foreach (var field in descriptor.Fields)
            {
                WriteField(writer, field);
            }
            writer.WriteEndElement();
        }

        public static void WriteField(XmlWriter writer, FieldDescriptor descriptor)
        {
            writer.WriteStartElement("Field");
            writer.WriteAttributeString("name", descriptor.Name);
            writer.WriteAttributeString("type", descriptor.TypeName);
            if (descriptor.IsCollection)
                writer.WriteAttributeString("isCollection", "true");
            if(descriptor.AttachedData.Count > 0)
            {
                writer.WriteStartElement("AttachedData");
                foreach (var kvp in descriptor.AttachedData)
                {
                    writer.WriteStartElement("Item");
                    writer.WriteAttributeString("key", kvp.Key);
                    writer.WriteAttributeString("value", kvp.Value.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
    }
}
