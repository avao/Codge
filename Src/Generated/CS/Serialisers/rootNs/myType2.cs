using Codge.BasicModel.CS.Serialisation;
using System.Xml;
using Types.rootNs;

namespace Serialisers.rootNs
{
	public class myType2 : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.rootNs.myType2)o;

			Utils.SerialiseValue(writer, "IntField", obj.IntField, context);
			Utils.SerialiseValue(writer, "BoolField", obj.BoolField, context);
			Utils.SerialiseBuiltInCollection(writer, "CollectionOfInt", obj.CollectionOfInt, context);
			Utils.SerialiseCollection(writer, "CollectionOfComposite", obj.CollectionOfComposite, context);
		}

	}
}


