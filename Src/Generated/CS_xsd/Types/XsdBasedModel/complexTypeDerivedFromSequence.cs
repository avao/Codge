
namespace Types.XsdBasedModel
{
	public class complexTypeDerivedFromSequence
		: complexTypeWithASequence
	{
	    public complexTypeDerivedFromSequence()
		{}


		public complexTypeDerivedFromSequence(string _anAttribute, string _sequenceElement, string _anotherAttribute, string _anotherSequenceElement)
			: base(_anAttribute, _sequenceElement)
		{
			anotherAttribute = _anotherAttribute;
			anotherSequenceElement = _anotherSequenceElement;
		}


		public	string anotherAttribute {get; set;}
		public	string anotherSequenceElement {get; set;}
	}
}


