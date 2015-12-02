
using System.Collections.Generic;

namespace Types.XsdBasedModel
{
	public enum enumType
	{

		item1
		,item2
		,_1itemStartsWithDigit_1724827033
		,item_With_Spaces_835378278
	
	}

    public static class enumTypeConverter
	{
        private static string[] _values = new string[4];
        private static IDictionary<string, enumType > _stringToEnum = new Dictionary<string, enumType >();

        static enumTypeConverter()
        {
		    _values[(int)enumType.item1] = "item1";
            _stringToEnum.Add("item1", enumType.item1);
		    _values[(int)enumType.item2] = "item2";
            _stringToEnum.Add("item2", enumType.item2);
		    _values[(int)enumType._1itemStartsWithDigit_1724827033] = "1itemStartsWithDigit";
            _stringToEnum.Add("1itemStartsWithDigit", enumType._1itemStartsWithDigit_1724827033);
		    _values[(int)enumType.item_With_Spaces_835378278] = "item With Spaces";
            _stringToEnum.Add("item With Spaces", enumType.item_With_Spaces_835378278);
        }

        public static string ConvertToString(enumType value)
        {
            return _values[(int)value];
	    }

        public static enumType ConvertToEnum(string value)
        {
            return _stringToEnum[value];
	    }
	}
}


