using BasicModel.CS.Serialisation;
using System.Xml;
using Types.XsdBasedModel;

namespace Serialisers.XsdBasedModel
{
	public class emptyComplexType : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.XsdBasedModel.emptyComplexType)o;

		}

	}
}


