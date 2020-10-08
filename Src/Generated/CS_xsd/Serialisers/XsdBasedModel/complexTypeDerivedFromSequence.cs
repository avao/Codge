using Codge.BasicModel.CS.Serialisation;
using System.Xml;
using Types.XsdBasedModel;

namespace Serialisers.XsdBasedModel
{
	public class complexTypeDerivedFromSequence : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.XsdBasedModel.complexTypeDerivedFromSequence)o;

            if(obj.anAttribute != null)
                writer.WriteAttributeString("anAttribute", obj.anAttribute.ToString());
			Utils.SerialiseValue(writer, "sequenceElement", obj.sequenceElement, context);
            if(obj.anotherAttribute != null)
                writer.WriteAttributeString("anotherAttribute", obj.anotherAttribute.ToString());
			Utils.SerialiseValue(writer, "anotherSequenceElement", obj.anotherSequenceElement, context);
		}

	}
}


