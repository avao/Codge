
namespace Types.XsdBasedModel
{
	public class complexTypeWithAField
	{
	    public complexTypeWithAField()
		{}


		public complexTypeWithAField(decimal _aField)
		{
			aField = _aField;
		}


		public	decimal aField {get; set;}
	}
}


