using Codge.BasicModel.CS.Serialisation;
using System.Xml;
using Types.XsdBasedModel;

namespace Serialisers.XsdBasedModel
{
	public class complexTypeWithContent : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.XsdBasedModel.complexTypeWithContent)o;

			Utils.SerialiseValue(writer, "anElement", obj.anElement, context);
		}

	}
}


