using Codge.BasicModel.CS.Serialisation;
using System.Xml;
using Types.XsdBasedModel;

namespace Serialisers.XsdBasedModel
{
	public class complexTypeWithInlineTypeLessElement_TypeLess : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.XsdBasedModel.complexTypeWithInlineTypeLessElement_TypeLess)o;

			Utils.SerialiseValue(writer, "element", obj.element, context);
		}

	}
}


