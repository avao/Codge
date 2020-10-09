using Codge.BasicModel.CS.Serialisation;
using System.Xml;
using Types.XsdBasedModel;

namespace Serialisers.XsdBasedModel
{
	public class complexTypeWithInlineElement : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.XsdBasedModel.complexTypeWithInlineElement)o;
			var baseSerialiser = context;

			Utils.SerialiseEnumAsStringIfHasValue(writer, "OptionalEnumeration", obj.OptionalEnumeration, OptionalEnumerationConverter.ConvertToString, context);
		}

	}
}


