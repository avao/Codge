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

			Utils.SerialiseIfHasValue(writer, "anyUri", obj.anyUri, context);
			Utils.SerialiseIfHasValue(writer, "base64Binary", obj.base64Binary, context);
			Utils.SerialiseIfHasValue(writer, "boolean", obj.boolean, context);
			Utils.SerialiseIfHasValue(writer, "byte", obj.byte_, context);
			Utils.SerialiseIfHasValue(writer, "date", obj.date, context);
			Utils.SerialiseIfHasValue(writer, "dateTime", obj.dateTime, context);
			Utils.SerialiseIfHasValue(writer, "decimal", obj.decimal_, context);
			Utils.SerialiseIfHasValue(writer, "double", obj.double_, context);
			Utils.SerialiseIfHasValue(writer, "duration", obj.duration, context);
			Utils.SerialiseIfHasValue(writer, "Entities", obj.Entities, context);
			Utils.SerialiseIfHasValue(writer, "Entity", obj.Entity, context);
			Utils.SerialiseIfHasValue(writer, "float", obj.float_, context);
			Utils.SerialiseIfHasValue(writer, "gDay", obj.gDay, context);
			Utils.SerialiseIfHasValue(writer, "gMonth", obj.gMonth, context);
			Utils.SerialiseIfHasValue(writer, "gMonthDay", obj.gMonthDay, context);
			Utils.SerialiseIfHasValue(writer, "gYear", obj.gYear, context);
			Utils.SerialiseIfHasValue(writer, "gYearMonth", obj.gYearMonth, context);
			Utils.SerialiseIfHasValue(writer, "hexBinary", obj.hexBinary, context);
			Utils.SerialiseIfHasValue(writer, "id", obj.id, context);
			Utils.SerialiseIfHasValue(writer, "idRef", obj.idRef, context);
			Utils.SerialiseIfHasValue(writer, "idRefs", obj.idRefs, context);
			Utils.SerialiseIfHasValue(writer, "int", obj.int_, context);
			Utils.SerialiseIfHasValue(writer, "integer", obj.integer, context);
			Utils.SerialiseIfHasValue(writer, "language", obj.language, context);
			Utils.SerialiseIfHasValue(writer, "long", obj.long_, context);
			Utils.SerialiseIfHasValue(writer, "Name", obj.Name, context);
			Utils.SerialiseIfHasValue(writer, "ncName", obj.ncName, context);
			Utils.SerialiseIfHasValue(writer, "negativeInteger", obj.negativeInteger, context);
			Utils.SerialiseIfHasValue(writer, "nmToken", obj.nmToken, context);
			Utils.SerialiseIfHasValue(writer, "nmTokens", obj.nmTokens, context);
			Utils.SerialiseIfHasValue(writer, "nonNegativeInteger", obj.nonNegativeInteger, context);
			Utils.SerialiseIfHasValue(writer, "nonPositiveInteger", obj.nonPositiveInteger, context);
			Utils.SerialiseIfHasValue(writer, "normalizedString", obj.normalizedString, context);
			Utils.SerialiseIfHasValue(writer, "positiveInteger", obj.positiveInteger, context);
			Utils.SerialiseIfHasValue(writer, "QName", obj.QName, context);
			Utils.SerialiseIfHasValue(writer, "short", obj.short_, context);
			Utils.SerialiseIfHasValue(writer, "string", obj.string_, context);
			Utils.SerialiseIfHasValue(writer, "time", obj.time, context);
			Utils.SerialiseIfHasValue(writer, "token", obj.token, context);
			Utils.SerialiseIfHasValue(writer, "unsignedByte", obj.unsignedByte, context);
			Utils.SerialiseIfHasValue(writer, "unsignedIint", obj.unsignedIint, context);
			Utils.SerialiseIfHasValue(writer, "unsignedLong", obj.unsignedLong, context);
			Utils.SerialiseIfHasValue(writer, "unsignedShort", obj.unsignedShort, context);
		}

	}
}


