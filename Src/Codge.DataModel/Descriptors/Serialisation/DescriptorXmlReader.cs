using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Codge.DataModel.Descriptors.Serialisation
{
    public static class DescriptorXmlReader
    {
        public static ModelDescriptor Read(XmlReader reader, string modelName, string rootNamespace)
        {
            reader.ReadToDescendant("Namespace");
            var ns = ReadNamespace(reader);
            ReadEndElement(reader);//Model
            return new ModelDescriptor(modelName, ns);
        }

        private static NamespaceDescriptor ReadNamespace(XmlReader reader)
        {
            bool bEmpty = reader.IsEmptyElement;
            reader.MoveToFirstAttribute();
            string name = reader.Value;
            var ns = new NamespaceDescriptor(name);
            if (ReadToDescendantElement(reader))
            {
                ReadNamespaceItem(reader, ns);
                while (reader.NodeType == XmlNodeType.Element)
                {
                    ReadNamespaceItem(reader, ns);
                }
            }
            if (!bEmpty)
                ReadEndElement(reader);

            return ns;
        }

        private static void ReadNamespaceItem(XmlReader reader, NamespaceDescriptor ns)
        {
            switch (reader.Name)
            {
                case "Enumeration":
                    ReadEnum(reader, ns);
                    break;
                case "Primitive":
                    ReadPrimitive(reader, ns);
                    break;
                case "Composite":
                    ReadComposite(reader, ns);
                    break;
                case "Namespace":
                    ns.Add(ReadNamespace(reader));
                    break;
                default:
                    throw new NotSupportedException("Unknown tag " + reader.Name);
            }

            MoveToNonWhitespace(reader);
        }

        private static void ReadPrimitive(XmlReader reader, NamespaceDescriptor namespaceDescriptor)
        {
            bool bEmpty = reader.IsEmptyElement;
            reader.MoveToFirstAttribute();
            var descriptor = namespaceDescriptor.CreatePrimitiveType(reader.Value);
            reader.Read();

            if (!bEmpty)
                ReadEndElement(reader);
        }

        private static void MoveToNonWhitespace(XmlReader reader)
        {
            while (reader.NodeType == XmlNodeType.Whitespace)
            {
                reader.Read();
            }
        }

        private static void ReadEndElement(XmlReader reader)
        {
            if (reader.IsEmptyElement)
                reader.Read();//TODO
            else
                reader.ReadEndElement();
        }

        private static void ReadComposite(XmlReader reader, NamespaceDescriptor namespaceDescriptor)
        {
            reader.MoveToFirstAttribute();
            var descriptor = namespaceDescriptor.CreateCompositeType(reader.Value);
            if (ReadToDescendantElement(reader))
            {
                ReadField(reader, descriptor);
                while (reader.ReadToNextSibling("Field"))
                {
                    ReadField(reader, descriptor);
                }
            }
            ReadEndElement(reader);
        }

        private static void ReadField(XmlReader reader, CompositeTypeDescriptor composite)
        {
            bool bEmpty = reader.IsEmptyElement;

            string name = null;
            string type = null;
            string isCollection = null;
            while (reader.MoveToNextAttribute())
            {
                switch (reader.Name)
                {
                    case "name":
                        name = reader.Value;
                        break;
                    case "type":
                        type = reader.Value;
                        break;
                    case "isCollection":
                        isCollection = reader.Value;
                        break;
                    default:
                        throw new NotSupportedException("Unknow tag " + reader.Name);
                }
            }

            var descriptor = composite.AddField(name, type, isCollection != null ? bool.Parse(isCollection) : false);
            reader.Read();
            if (!bEmpty)
            {
                if(reader.NodeType == XmlNodeType.Element)
                {
                    reader.readEle
                }
                ReadEndElement(reader);
            }
        }

        private static void ReadEnum(XmlReader reader, NamespaceDescriptor namespaceDescriptor)
        {
            reader.MoveToFirstAttribute();
            var descriptor = namespaceDescriptor.CreateEnumerationType(reader.Value);
            if (ReadToDescendantElement(reader))
            {
                ReadEnumItem(reader, descriptor);
                while (reader.ReadToNextSibling("item"))
                {
                    ReadEnumItem(reader, descriptor);
                }
            }
            ReadEndElement(reader);
        }

        private static void ReadEnumItem(XmlReader reader, EnumerationTypeDescriptor descriptor)
        {
            var item = ReadEnumItem(reader);
            if (item.Value.HasValue)
                descriptor.AddItem(item.Key, item.Value.Value);
            else
                descriptor.AddItem(item.Key);
        }

        private static KeyValuePair<string, int?> ReadEnumItem(XmlReader reader)
        {
            string name = string.Empty;
            int? value = null;
            reader.MoveToFirstAttribute();

            if (reader.Name == "name")
            {
                name = reader.Value;
            }
            else if (reader.Name == "value")
            {
                value = int.Parse(reader.Value);
            }

            if (reader.MoveToNextAttribute())
            {//TODO copy-paste
                if (reader.Name == "name")
                {
                    name = reader.Value;
                }
                else if (reader.Name == "value")
                {
                    value = int.Parse(reader.Value);
                }
            }
            return new KeyValuePair<string, int?>(name, value);
        }

        private static bool ReadToDescendantElement(XmlReader reader)
        {
            //TODO check long and short form of empty elements
            var r = reader.Read();
            if (!r)
                return false;
            while (reader.NodeType != XmlNodeType.Element && reader.NodeType != XmlNodeType.EndElement)
            {
                if (!reader.Read())
                    return false;
            }

            return reader.NodeType == XmlNodeType.Element;
        }

    }
}
