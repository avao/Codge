

namespace Types.XsdBasedModel
{
	public class elementWithEmbededType
	{
	    public elementWithEmbededType()
		{}


		public elementWithEmbededType(string _aSubElement,string _elementInAChoice1,string _elementInAChoice2)
		{
			aSubElement = _aSubElement;
			elementInAChoice1 = _elementInAChoice1;
			elementInAChoice2 = _elementInAChoice2;
		}


		public	string aSubElement {get; set;}
		public	string elementInAChoice1 {get; set;}
		public	string elementInAChoice2 {get; set;}
	}
}


