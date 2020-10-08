
namespace Types.XsdBasedModel
{
	public class complexTypeWithContentWithAttribute
		: emptyComplexType
	{
	    public complexTypeWithContentWithAttribute()
		{}


		public complexTypeWithContentWithAttribute(string _anAttribute)
			: base()
		{
			anAttribute = _anAttribute;
		}


		public	string anAttribute {get; set;}
	}
}


