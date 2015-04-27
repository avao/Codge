using BasicModel.CS.Serialisation;
using System.Xml;

namespace Serialisers.rootNs
{
	public class myType2 : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.rootNs.myType2)o;

            writer.WriteStartElement("IntField");
			writer.WriteValue(obj.IntField);
            writer.WriteEndElement();
            writer.WriteStartElement("BoolField");
			writer.WriteValue(obj.BoolField);
            writer.WriteEndElement();
			Utils.SerialiseBuiltInCollection(writer, "CollectionOfInt", obj.CollectionOfInt, context);
			Utils.SerialiseCollection(writer, "CollectionOfComposite", obj.CollectionOfComposite, context);
		}

	}
}


