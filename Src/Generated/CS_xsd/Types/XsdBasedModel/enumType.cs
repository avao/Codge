

namespace Types.XsdBasedModel
{
	public enum enumType
	{

		item1
		,item2
		,_1itemStartsWithDigit
		,item_With_Spaces
	
	}

    public static class enumTypeConverter
	{
        private static string[] _values = new string[4];

        static enumTypeConverter()
        {
		    _values[(int)enumType.item1] = "item1";
		    _values[(int)enumType.item2] = "item2";
		    _values[(int)enumType._1itemStartsWithDigit] = "1itemStartsWithDigit";
		    _values[(int)enumType.item_With_Spaces] = "item With Spaces";
        }

        public static string ConvertToString(enumType value)
        {
            return _values[(int)value];
	    }
	}
}


