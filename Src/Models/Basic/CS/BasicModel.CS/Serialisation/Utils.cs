using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BasicModel.CS.Serialisation
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
            var sb = new StringBuilder();
            using(var writer = XmlWriter.Create(sb))
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
                writer.WriteStartElement(name);
                writer.WriteValue(item);
                writer.WriteEndElement();
            }
        }

        public static void SerialiseCollection<T>(XmlWriter writer, string name, IEnumerable<T> items, SerialisationContext context)
        {
            if (items == null)
                return;
            foreach(var item in items)
            {
                Serialise(writer, name, item, context);
            }
        }

        public static void Serialise<T>(XmlWriter writer, string tag, T o, SerialisationContext context)
        {
            writer.WriteStartElement(tag);
            if(o != null)
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
            where T : struct
        {
            writer.WriteStartElement(tag);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SerialiseIfHasValue<T>(XmlWriter writer, string tag, Nullable<T> value, SerialisationContext context)
            where T : struct
        {
            if(value.HasValue)
            {
                writer.WriteStartElement(tag);
                writer.WriteValue(value.Value);
                writer.WriteEndElement();
            }
        }

        public static void SerialiseIfHasValue(XmlWriter writer, string tag, string value, SerialisationContext context)
        {
            if (!string.IsNullOrEmpty(value))
            {
                writer.WriteStartElement(tag);
                writer.WriteValue(value);
                writer.WriteEndElement();
            }
        }
     
    }
}
