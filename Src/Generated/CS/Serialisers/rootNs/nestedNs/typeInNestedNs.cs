using BasicModel.CS.Serialisation;
using System.Xml;
using Types.rootNs.nestedNs;

namespace Serialisers.rootNs.nestedNs
{
	public class typeInNestedNs : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.rootNs.nestedNs.typeInNestedNs)o;

			Utils.SerialiseValue(writer, "StringField", obj.StringField, context);
		}

	}
}


