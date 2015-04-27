using BasicModel.CS.Serialisation;
using System.Xml;

namespace Serialisers.rootNs.nestedNs
{
	public class typeInNestedNs : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.rootNs.nestedNs.typeInNestedNs)o;

            writer.WriteStartElement("StringField");
			writer.WriteValue(obj.StringField);
            writer.WriteEndElement();
		}

	}
}


