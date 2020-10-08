
namespace Types.XsdBasedModel
{
	public class complexTypeWithSimpleContent
	{
	    public complexTypeWithSimpleContent()
		{}


		public complexTypeWithSimpleContent(string _anAttribute, string _Content)
		{
			anAttribute = _anAttribute;
			Content = _Content;
		}


		public	string anAttribute {get; set;}
		public	string Content {get; set;}
	}
}


