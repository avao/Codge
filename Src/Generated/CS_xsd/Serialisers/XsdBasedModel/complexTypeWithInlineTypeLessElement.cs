using Codge.BasicModel.CS.Serialisation;
using System.Xml;
using Types.XsdBasedModel;

namespace Serialisers.XsdBasedModel
{
	public class complexTypeWithInlineTypeLessElement : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.XsdBasedModel.complexTypeWithInlineTypeLessElement)o;

			Utils.Serialise(writer, "TypeLess", obj.TypeLess, context);
		}

	}
}


