using Codge.BasicModel.CS.Serialisation;
using System.Xml;
using Types.XsdBasedModel;

namespace Serialisers.XsdBasedModel
{
	public class recursiveType : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.XsdBasedModel.recursiveType)o;

			Utils.SerialiseIfHasValue(writer, "recursiveType", obj.recursiveType1, context);
		}

	}
}


