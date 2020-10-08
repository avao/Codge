
namespace Types.XsdBasedModel
{
	public class complexTypeWithASequence
	{
	    public complexTypeWithASequence()
		{}


		public complexTypeWithASequence(string _anAttribute, string _sequenceElement)
		{
			anAttribute = _anAttribute;
			sequenceElement = _sequenceElement;
		}


		public	string anAttribute {get; set;}
		public	string sequenceElement {get; set;}
	}
}


