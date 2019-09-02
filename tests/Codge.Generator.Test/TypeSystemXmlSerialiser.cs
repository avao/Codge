using Codge.DataModel;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Codge.Generator.Test
{
    public class TypeSystemXmlSerialiser
    {
        public static string ToString(TypeSystem typeSystem)
        {
            var builder = new StringBuilder();
            using (var writer = XmlWriter.Create(builder))
            {
                WriteTo(typeSystem, writer);
            }
            return builder.ToString();
        }

        public static void WriteTo(TypeSystem typeSystem, XmlWriter writer)
        {
            writer.WriteStartElement("TypeSystem");

            WriteTypes(writer, typeSystem.Types);
            WriteNamespaces(writer, typeSystem.Namespaces);

            writer.WriteEndElement();
        }


        private static void WriteTypes(XmlWriter writer, IEnumerable<TypeBase> types)
        {
            //writer.WriteStartElement("Types");
            foreach (var type in types)
            {
                WriteType(writer, type);
            }
            //writer.WriteEndElement();
        }

        private static void WriteType(XmlWriter writer, TypeBase type)
        {
            writer.WriteStartElement("Type");
            writer.WriteAttributeString("name", type.Name);
            var composite = type as CompositeType;
            if (composite != null)
            {
                WriteCompositeType(writer, composite);
            }
            else
            {
                var enumeration = type as EnumerationType;
                if (enumeration != null)
                {
                    WriteEnumerationType(writer, enumeration);
                }
            }
            writer.WriteEndElement();
        }

        private static void WriteEnumerationType(XmlWriter writer, EnumerationType enumeration)
        {
            foreach (var item in enumeration.Items)
            {
                writer.WriteStartElement("Item");
                writer.WriteAttributeString("name", item.Name);
                writer.WriteAttributeString("value", item.Value.ToString());
                writer.WriteEndElement();
            }
        }

        private static void WriteCompositeType(XmlWriter writer, CompositeType composite)
        {
            foreach (var field in composite.Fields)
            {
                writer.WriteStartElement("Field");
                writer.WriteAttributeString("name", field.Name);
                writer.WriteAttributeString("type", field.Type.GetFullName("."));
                if (field.IsCollection)
                {
                    writer.WriteAttributeString("isCollection", "true");
                }
                foreach (var kvp in field.AttachedData)
                {
                    writer.WriteElementString(kvp.Key, kvp.Value.ToString());
                }
                writer.WriteEndElement();
            }
        }

        private static void WriteNamespaces(XmlWriter writer, IEnumerable<Namespace> namespaces)
        {
            //writer.WriteStartElement("Namespaces");
            foreach (var ns in namespaces)
            {
                WriteNamespace(writer, ns);
            }
            //writer.WriteEndElement();

        }

        private static void WriteNamespace(XmlWriter writer, Namespace ns)
        {
            writer.WriteStartElement("Namespace");
            writer.WriteAttributeString("name", ns.Name);

            WriteTypes(writer, ns.Types);
            WriteNamespaces(writer, ns.Namespaces);

            writer.WriteEndElement();
        }
    }
}
