using Codge.BasicModel.CS.Serialisation;
using System.Xml;
using Types.XsdBasedModel;

namespace Serialisers.XsdBasedModel
{
	public class elementWithEmbededType : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.XsdBasedModel.elementWithEmbededType)o;

			Utils.SerialiseValue(writer, "aSubElement", obj.aSubElement, context);
		}

	}
}


