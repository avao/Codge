using Codge.BasicModel.CS.Serialisation;
using System.Xml;
using Types.XsdBasedModel;

namespace Serialisers.XsdBasedModel
{
	public class ordertype : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.XsdBasedModel.ordertype)o;

            if(obj.status != null)
                writer.WriteAttributeString("status", obj.status.ToString());
			Utils.SerialiseValue(writer, "customer", obj.customer, context);
			Utils.Serialise(writer, "elementWithEmbededTypeInPlace", obj.elementWithEmbededTypeInPlace, context);
			Utils.SerialiseIfHasValue(writer, "billto", obj.billto, context);
			Utils.SerialiseValue(writer, "shipto", obj.shipto, context);
			Utils.Serialise(writer, "elementWithEmbededType", obj.elementWithEmbededType, context);
			Utils.SerialiseBuiltInCollection(writer, "shortStringCollection", obj.shortStringCollection, context);
			Utils.SerialiseEnumAsString(writer, "elemSimpleType", obj.elemSimpleType, simpleTypeConverter.ConvertToString, context);
			Utils.Serialise(writer, "elemEmptyType", obj.elemEmptyType, context);
			Utils.SerialiseEnumAsStringIfHasValue(writer, "enumField", obj.enumField, enumTypeConverter.ConvertToString, context);
			Utils.SerialiseEnumCollectionAsString(writer, "enumFieldCollection", obj.enumFieldCollection, enumTypeConverter.ConvertToString, context);
			Utils.SerialiseIfHasValue(writer, "elem1", obj.elem1, context);
			Utils.SerialiseIfHasValue(writer, "elem2", obj.elem2, context);
		}

	}
}


