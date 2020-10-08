using Codge.BasicModel.CS.Serialisation;
using System.Xml;
using Types.XsdBasedModel;

namespace Serialisers.XsdBasedModel
{
	public class complexTypeWithASequence : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.XsdBasedModel.complexTypeWithASequence)o;

            if(obj.anAttribute != null)
                writer.WriteAttributeString("anAttribute", obj.anAttribute.ToString());
			Utils.SerialiseValue(writer, "sequenceElement", obj.sequenceElement, context);
		}

	}
}


