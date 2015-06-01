using BasicModel.CS.Serialisation;
using System.Xml;
using Types.XsdBasedModel;

namespace Serialisers.XsdBasedModel
{
	public class complexTypeWithSimpleContent : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.XsdBasedModel.complexTypeWithSimpleContent)o;

            if(obj.anAttribute != null)
                writer.WriteAttributeString("anAttribute", obj.anAttribute.ToString());
            if(obj.Content != null)
			    writer.WriteValue(obj.Content.ToString());
		}

	}
}


