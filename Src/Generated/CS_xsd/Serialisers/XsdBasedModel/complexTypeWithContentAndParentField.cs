using BasicModel.CS.Serialisation;
using System.Xml;
using Types.XsdBasedModel;

namespace Serialisers.XsdBasedModel
{
	public class complexTypeWithContentAndParentField : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.XsdBasedModel.complexTypeWithContentAndParentField)o;

            if(obj.aField != null)
                writer.WriteAttributeString("aField", obj.aField.ToString());
			Utils.SerialiseIfHasValue(writer, "anElement", obj.anElement, context);
		}

	}
}


