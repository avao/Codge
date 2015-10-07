using BasicModel.CS.Serialisation;
using System.Xml;
using Types.XsdBasedModel;

namespace Serialisers.XsdBasedModel
{
	public class allXsdTypes : IXmlSerialiser
	{

        public void Serialize(XmlWriter writer, object o, SerialisationContext context)
        {
            var obj = (Types.XsdBasedModel.allXsdTypes)o;

			Utils.SerialiseValue(writer, "anyUri", obj.anyUri, context);
			Utils.SerialiseValue(writer, "base64Binary", obj.base64Binary, context);
			Utils.SerialiseValue(writer, "boolean", obj.boolean, context);
			Utils.SerialiseValue(writer, "byte", obj.byte_, context);
			Utils.SerialiseValue(writer, "date", obj.date, context);
			Utils.SerialiseValue(writer, "dateTime", obj.dateTime, context);
			Utils.SerialiseValue(writer, "decimal", obj.decimal_, context);
			Utils.SerialiseValue(writer, "double", obj.double_, context);
			Utils.SerialiseValue(writer, "duration", obj.duration, context);
			Utils.SerialiseValue(writer, "Entities", obj.Entities, context);
			Utils.SerialiseValue(writer, "Entity", obj.Entity, context);
			Utils.SerialiseValue(writer, "float", obj.float_, context);
			Utils.SerialiseValue(writer, "gDay", obj.gDay, context);
			Utils.SerialiseValue(writer, "gMonth", obj.gMonth, context);
			Utils.SerialiseValue(writer, "gMonthDay", obj.gMonthDay, context);
			Utils.SerialiseValue(writer, "gYear", obj.gYear, context);
			Utils.SerialiseValue(writer, "gYearMonth", obj.gYearMonth, context);
			Utils.SerialiseValue(writer, "hexBinary", obj.hexBinary, context);
			Utils.SerialiseValue(writer, "id", obj.id, context);
			Utils.SerialiseValue(writer, "idRef", obj.idRef, context);
			Utils.SerialiseValue(writer, "idRefs", obj.idRefs, context);
			Utils.SerialiseValue(writer, "int", obj.int_, context);
			Utils.SerialiseValue(writer, "integer", obj.integer, context);
			Utils.SerialiseValue(writer, "language", obj.language, context);
			Utils.SerialiseValue(writer, "long", obj.long_, context);
			Utils.SerialiseValue(writer, "Name", obj.Name, context);
			Utils.SerialiseValue(writer, "ncName", obj.ncName, context);
			Utils.SerialiseValue(writer, "negativeInteger", obj.negativeInteger, context);
			Utils.SerialiseValue(writer, "nmToken", obj.nmToken, context);
			Utils.SerialiseValue(writer, "nmTokens", obj.nmTokens, context);
			Utils.SerialiseValue(writer, "nonNegativeInteger", obj.nonNegativeInteger, context);
			Utils.SerialiseValue(writer, "nonPositiveInteger", obj.nonPositiveInteger, context);
			Utils.SerialiseValue(writer, "normalizedString", obj.normalizedString, context);
			Utils.SerialiseValue(writer, "positiveInteger", obj.positiveInteger, context);
			Utils.SerialiseValue(writer, "QName", obj.QName, context);
			Utils.SerialiseValue(writer, "short", obj.short_, context);
			Utils.SerialiseValue(writer, "string", obj.string_, context);
			Utils.SerialiseValue(writer, "time", obj.time, context);
			Utils.SerialiseValue(writer, "token", obj.token, context);
			Utils.SerialiseValue(writer, "unsignedByte", obj.unsignedByte, context);
			Utils.SerialiseValue(writer, "unsignedIint", obj.unsignedIint, context);
			Utils.SerialiseValue(writer, "unsignedLong", obj.unsignedLong, context);
			Utils.SerialiseValue(writer, "unsignedShort", obj.unsignedShort, context);
		}

	}
}


