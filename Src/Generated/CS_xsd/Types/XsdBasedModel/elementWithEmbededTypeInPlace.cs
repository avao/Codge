
namespace Types.XsdBasedModel
{
	public class elementWithEmbededTypeInPlace
		: complexTypeWithAField
	{
	    public elementWithEmbededTypeInPlace()
		{}


		public elementWithEmbededTypeInPlace(decimal? _aField, string _extraField)
			: base(_aField)
		{
			extraField = _extraField;
		}


		public	string extraField {get; set;}
	}
}


