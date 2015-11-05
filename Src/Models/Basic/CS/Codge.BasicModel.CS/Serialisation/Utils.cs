using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Codge.BasicModel.CS.Serialisation
{
    public static class Utils
    {
        public static void Serialise(XmlWriter writer, object o, SerialisationContext context)
        {
            var serialiser = context.GetSerialiser(o.GetType().FullName);
            serialiser.Serialize(writer, o, context);
        }

        public static string Serialise(object o, string rootTag, SerialisationContext context)
        {
            XmlWriterSettings settings = new XmlWriterSettings {
                    OmitXmlDeclaration = true
                };

            var sb = new StringBuilder();
            using (var writer = XmlWriter.Create(sb, settings))
            {
                Serialise(writer, rootTag, o, context);
            }
            return sb.ToString();
        }

        public static void SerialiseBuiltInCollection<T>(XmlWriter writer, string name, IEnumerable<T> items, SerialisationContext context)
        {
            if (items == null)
                return;
            foreach (var item in items)
            {
                SerialiseValue<T>(writer, name, item, context);
            }
        }

        public static void SerialiseEnumCollectionAsString<T>(XmlWriter writer, string name, IEnumerable<T> items, Func<T, string> mapValueFunc, SerialisationContext context)
        {
            if (items == null)
                return;
            foreach (var item in items)
            {
                SerialiseValue(writer, name, mapValueFunc(item), context);
            }
        }

        public static void SerialiseCollection<T>(XmlWriter writer, string name, IEnumerable<T> items, SerialisationContext context)
        {
            if (items == null)
                return;
            foreach (var item in items)
            {
                Serialise(writer, name, item, context);
            }
        }


        public static void Serialise<T>(XmlWriter writer, string tag, T o, SerialisationContext context)
        {
            writer.WriteStartElement(tag);
            if (o != null)
            {
                Serialise(writer, o, context);
            }
            writer.WriteEndElement();
        }

        public static void SerialiseValue(XmlWriter writer, string tag, string value, SerialisationContext context)
        {
            writer.WriteStartElement(tag);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SerialiseValue<T>(XmlWriter writer, string tag, T value, SerialisationContext context)
        {
            writer.WriteStartElement(tag);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SerialiseEnumAsName<T>(XmlWriter writer, string tag, T value, SerialisationContext context)
            where T : struct
        {
            SerialiseValue(writer, tag, Enum.GetName(typeof(T), value), context);
        }


        public static void SerialiseEnumAsNameIfHasValue<T>(XmlWriter writer, string tag, Nullable<T> value, SerialisationContext context)
            where T : struct
        {
            if (value.HasValue)
            {
                SerialiseEnumAsName(writer, tag, value.Value, context);
            }
        }

        public static void SerialiseEnumAsString<T>(XmlWriter writer, string tag, T value, Func<T, string> mapValueFunc, SerialisationContext context)
            where T : struct
        {
            SerialiseValue(writer, tag, mapValueFunc(value), context);
        }


        public static void SerialiseEnumAsStringIfHasValue<T>(XmlWriter writer, string tag, Nullable<T> value, Func<T, string> mapValueFunc, SerialisationContext context)
            where T : struct
        {
            if (value.HasValue)
            {
                SerialiseEnumAsString(writer, tag, value.Value, mapValueFunc, context);
            }
        }

        public static void SerialiseIfHasValue<T>(XmlWriter writer, string tag, Nullable<T> value, SerialisationContext context)
            where T : struct
        {
            if (value.HasValue)
            {
                SerialiseValue(writer, tag, value.Value, context);
            }
        }

        public static void SerialiseIfHasValue<T>(XmlWriter writer, string tag, T value, SerialisationContext context)
            where T : class
        {
            if (value != null)
            {
                Serialise(writer, tag, value, context);
            }
        }

        public static void SerialiseIfHasValue(XmlWriter writer, string tag, string value, SerialisationContext context)
        {
            if (!string.IsNullOrEmpty(value))
            {
                SerialiseValue(writer, tag, value, context);
            }
        }

        public static void SerialiseCDataIfHasValue(XmlWriter writer, string tag, string value, SerialisationContext context)
        {
            if (!string.IsNullOrEmpty(value))
            {
                writer.WriteStartElement(tag);
                writer.WriteCData(value);
                writer.WriteEndElement();
            }
        }
    }
}
