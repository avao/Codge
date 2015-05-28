using BasicModel.CS.Serialisation;
using System.Xml;

namespace Serialisers.XsdBasedModel
{
	public class ordertype : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.XsdBasedModel.ordertype)o;

            if(obj.status != null)
                writer.WriteAttributeString("status", obj.status.ToString());
			Utils.SerialiseIfHasValue(writer, "customer", obj.customer, context);
			Utils.SerialiseIfHasValue(writer, "orderdetails", obj.orderdetails, context);
			Utils.SerialiseIfHasValue(writer, "billto", obj.billto, context);
			Utils.SerialiseIfHasValue(writer, "shipto", obj.shipto, context);
			Utils.SerialiseIfHasValue(writer, "elementWithEmbededType", obj.elementWithEmbededType, context);
			Utils.SerialiseIfHasValue(writer, "elemSimpleType", obj.elemSimpleType, context);
			Utils.SerialiseIfHasValue(writer, "elemEmptyType", obj.elemEmptyType, context);
			Utils.SerialiseIfHasValue(writer, "enumField", obj.enumField, context);
		}

	}
}


