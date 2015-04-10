using BasicModel.CS;
using System.Xml;

namespace Serialisers.rootNs.nestedNs
{
	public class typeInNestedNs : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o)
        {
            var obj = (Types.rootNs.nestedNs.typeInNestedNs)o;

		}

	}
}


