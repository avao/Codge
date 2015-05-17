using BasicModel.CS.Serialisation;
using System.Xml;

namespace Serialisers.XsdBasedModel
{
	public class elementWithEmbededType : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.XsdBasedModel.elementWithEmbededType)o;

			Utils.SerialiseIfHasValue(writer, "aSubElement", obj.aSubElement, context);
		}

	}
}


