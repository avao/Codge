
using System.Collections.Generic;

namespace Types.XsdBasedModel
{
	public enum simpleType
	{

		anEnumerationValue
	
	}

    public static class simpleTypeConverter
	{
        private static string[] _values = new string[1];
        private static IDictionary<string, simpleType > _stringToEnum = new Dictionary<string, simpleType >();

        static simpleTypeConverter()
        {
		    _values[(int)simpleType.anEnumerationValue] = "anEnumerationValue";
            _stringToEnum.Add("anEnumerationValue", simpleType.anEnumerationValue);
        }

        public static string ConvertToString(simpleType value)
        {
            return _values[(int)value];
	    }

        public static simpleType ConvertToEnum(string value)
        {
            return _stringToEnum[value];
	    }
	}
}


