

namespace Types.XsdBasedModel
{
	public enum simpleType
	{

		anEnumerationValue
	
	}

    public static class simpleTypeConverter
	{
        private static string[] _values = new string[1];

        static simpleTypeConverter()
        {
		    _values[(int)simpleType.anEnumerationValue] = "anEnumerationValue";
        }

        public static string ConvertToString(simpleType value)
        {
            return _values[(int)value];
	    }
	}
}


