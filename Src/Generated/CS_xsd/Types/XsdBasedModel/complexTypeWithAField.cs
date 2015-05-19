

namespace Types.XsdBasedModel
{
	public class complexTypeWithAField
	{
	    public complexTypeWithAField()
		{}


		public complexTypeWithAField(string _aField)
		{
			aField = _aField;
		}


		public	string aField {get; set;}
	}
}


