
namespace Types.XsdBasedModel
{
	public class complexTypeWithContent
		: emptyComplexType
	{
	    public complexTypeWithContent()
		{}


		public complexTypeWithContent(string _anElement)
			: base()
		{
			anElement = _anElement;
		}


		public	string anElement {get; set;}
	}
}


