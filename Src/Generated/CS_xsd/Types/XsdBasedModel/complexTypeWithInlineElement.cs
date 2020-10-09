
namespace Types.XsdBasedModel
{
	public class complexTypeWithInlineElement
		: emptyComplexType
	{
	    public complexTypeWithInlineElement()
		{}


		public complexTypeWithInlineElement(XsdBasedModel.OptionalEnumeration? _OptionalEnumeration)
			: base()
		{
			OptionalEnumeration = _OptionalEnumeration;
		}


		public	XsdBasedModel.OptionalEnumeration? OptionalEnumeration {get; set;}
	}
}


