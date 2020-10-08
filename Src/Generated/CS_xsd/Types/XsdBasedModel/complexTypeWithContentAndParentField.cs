
namespace Types.XsdBasedModel
{
	public class complexTypeWithContentAndParentField
		: complexTypeWithAField
	{
	    public complexTypeWithContentAndParentField()
		{}


		public complexTypeWithContentAndParentField(decimal _aField, string _anElement)
			: base(_aField)
		{
			anElement = _anElement;
		}


		public	string anElement {get; set;}
	}
}


