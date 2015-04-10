using BasicModel.CS;
using System.Xml;

namespace Serialisers.rootNs
{
	public class myType2 : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o)
        {
            var obj = (Types.rootNs.myType2)o;

            writer.WriteStartElement("IntField");
			writer.WriteValue(obj.IntField);
            writer.WriteEndElement();
            writer.WriteStartElement("BoolField");
			writer.WriteValue(obj.BoolField);
            writer.WriteEndElement();
            writer.WriteStartElement("CollectionOfInt");
			writer.WriteValue(obj.CollectionOfInt);
            writer.WriteEndElement();
		}

	}
}


