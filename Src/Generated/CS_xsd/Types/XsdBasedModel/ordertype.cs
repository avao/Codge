
namespace Types.XsdBasedModel
{
	public class ordertype
	{
	    public ordertype()
		{}


		public ordertype(string _status, string _customer, XsdBasedModel.elementWithEmbededTypeInPlace _elementWithEmbededTypeInPlace, int? _billto, string _shipto, XsdBasedModel.elementWithEmbededType _elementWithEmbededType, string[] _shortStringCollection, XsdBasedModel.simpleType _elemSimpleType, XsdBasedModel.elemEmptyType_EmptyComplex _elemEmptyType, XsdBasedModel.enumType? _enumField, XsdBasedModel.enumType[] _enumFieldCollection, string _elem1, decimal? _elem2)
		{
			status = _status;
			customer = _customer;
			elementWithEmbededTypeInPlace = _elementWithEmbededTypeInPlace;
			billto = _billto;
			shipto = _shipto;
			elementWithEmbededType = _elementWithEmbededType;
			shortStringCollection = _shortStringCollection;
			elemSimpleType = _elemSimpleType;
			elemEmptyType = _elemEmptyType;
			enumField = _enumField;
			enumFieldCollection = _enumFieldCollection;
			elem1 = _elem1;
			elem2 = _elem2;
		}


		public	string status {get; set;}
		public	string customer {get; set;}
		public	XsdBasedModel.elementWithEmbededTypeInPlace elementWithEmbededTypeInPlace {get; set;}
		public	int? billto {get; set;}
		public	string shipto {get; set;}
		public	XsdBasedModel.elementWithEmbededType elementWithEmbededType {get; set;}
		public	string[] shortStringCollection {get; set;}
		public	XsdBasedModel.simpleType elemSimpleType {get; set;}
		public	XsdBasedModel.elemEmptyType_EmptyComplex elemEmptyType {get; set;}
		public	XsdBasedModel.enumType? enumField {get; set;}
		public	XsdBasedModel.enumType[] enumFieldCollection {get; set;}
		public	string elem1 {get; set;}
		public	decimal? elem2 {get; set;}
	}
}


