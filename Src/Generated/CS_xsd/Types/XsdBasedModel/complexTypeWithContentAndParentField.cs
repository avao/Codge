
namespace Types.XsdBasedModel
{
	public class complexTypeWithContentAndParentField
	{
	    public complexTypeWithContentAndParentField()
		{}


		public complexTypeWithContentAndParentField(string _aField,string _anElement)
		{
			aField = _aField;
			anElement = _anElement;
		}


		public	string aField {get; set;}
		public	string anElement {get; set;}
	}
}


