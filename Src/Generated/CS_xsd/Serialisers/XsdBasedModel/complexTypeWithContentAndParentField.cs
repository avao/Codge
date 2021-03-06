using Codge.BasicModel.CS.Serialisation;
using System.Xml;
using Types.XsdBasedModel;

namespace Serialisers.XsdBasedModel
{
	public class complexTypeWithContentAndParentField : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.XsdBasedModel.complexTypeWithContentAndParentField)o;
			var baseSerialiser = context;

            if(obj.aField != null)
                writer.WriteAttributeString("aField", obj.aField.ToString());
			Utils.SerialiseValue(writer, "anElement", obj.anElement, context);
		}

	}
}


