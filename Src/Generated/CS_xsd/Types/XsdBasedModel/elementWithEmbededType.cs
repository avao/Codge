

namespace Types.XsdBasedModel
{
	public class elementWithEmbededType
	{
	    public elementWithEmbededType()
		{}


		public elementWithEmbededType(string _aSubElement)
		{
			aSubElement = _aSubElement;
		}


		public	string aSubElement {get; set;}
	}
}


