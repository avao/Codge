using BasicModel.CS.Serialisation;
using System.Xml;

namespace Serialisers.XsdBasedModel
{
	public class complexTypeWithContent : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.XsdBasedModel.complexTypeWithContent)o;

			Utils.SerialiseIfHasValue(writer, "anElement", obj.anElement, context);
		}

	}
}


