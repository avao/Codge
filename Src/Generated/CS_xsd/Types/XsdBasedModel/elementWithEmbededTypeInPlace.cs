

namespace Types.XsdBasedModel
{
	public class elementWithEmbededTypeInPlace
	{
	    public elementWithEmbededTypeInPlace()
		{}


		public elementWithEmbededTypeInPlace(string _aField,string _extraField)
		{
			aField = _aField;
			extraField = _extraField;
		}


		public	string aField {get; set;}
		public	string extraField {get; set;}
	}
}


