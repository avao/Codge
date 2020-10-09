
using System.Collections.Generic;

namespace Types.XsdBasedModel
{
	public enum OptionalEnumeration
	{

		One
		,Two
		,Three
	
	}

    public static class OptionalEnumerationConverter
	{
        private static string[] _values = new string[3];
        private static IDictionary<string, OptionalEnumeration > _stringToEnum = new Dictionary<string, OptionalEnumeration >();

        static OptionalEnumerationConverter()
        {
		    _values[(int)OptionalEnumeration.One] = "One";
            _stringToEnum.Add("One", OptionalEnumeration.One);
		    _values[(int)OptionalEnumeration.Two] = "Two";
            _stringToEnum.Add("Two", OptionalEnumeration.Two);
		    _values[(int)OptionalEnumeration.Three] = "Three";
            _stringToEnum.Add("Three", OptionalEnumeration.Three);
        }

        public static string ConvertToString(OptionalEnumeration value)
        {
            return _values[(int)value];
	    }

        public static OptionalEnumeration ConvertToEnum(string value)
        {
            return _stringToEnum[value];
	    }

		public static OptionalEnumeration? TryConvertToEnum(string value)
        {
            OptionalEnumeration enumValue;
            if(!_stringToEnum.TryGetValue(value, out enumValue))
            {
                return null;
            }
            return enumValue;
	    }
	}
}


