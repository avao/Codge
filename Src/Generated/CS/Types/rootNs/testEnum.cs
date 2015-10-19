
using System.Collections.Generic;

namespace Types.rootNs
{
	public enum testEnum
	{

		enumValue1
		,enumValue2
	
	}

    public static class testEnumConverter
	{
        private static string[] _values = new string[2];
        private static IDictionary<string, testEnum > _stringToEnum = new Dictionary<string, testEnum >();

        static testEnumConverter()
        {
		    _values[(int)testEnum.enumValue1] = "enumValue1";
            _stringToEnum.Add("enumValue1", testEnum.enumValue1);
		    _values[(int)testEnum.enumValue2] = "enumValue2";
            _stringToEnum.Add("enumValue2", testEnum.enumValue2);
        }

        public static string ConvertToString(testEnum value)
        {
            return _values[(int)value];
	    }

        public static testEnum ConvertToEnum(string value)
        {
            return _stringToEnum[value];
	    }
	}
}


