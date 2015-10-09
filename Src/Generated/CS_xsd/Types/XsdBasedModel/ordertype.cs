

namespace Types.XsdBasedModel
{
	public class ordertype
	{
	    public ordertype()
		{}


		public ordertype(string _status,string _customer,XsdBasedModel.elementWithEmbededTypeInPlace _elementWithEmbededTypeInPlace,int? _billto,string _shipto,XsdBasedModel.elementWithEmbededType _elementWithEmbededType,XsdBasedModel.simpleType _elemSimpleType,XsdBasedModel.elemEmptyType_EmptyComplex _elemEmptyType,XsdBasedModel.enumType? _enumField,XsdBasedModel.enumType[] _enumFieldCollection)
		{
			status = _status;
			customer = _customer;
			elementWithEmbededTypeInPlace = _elementWithEmbededTypeInPlace;
			billto = _billto;
			shipto = _shipto;
			elementWithEmbededType = _elementWithEmbededType;
			elemSimpleType = _elemSimpleType;
			elemEmptyType = _elemEmptyType;
			enumField = _enumField;
			enumFieldCollection = _enumFieldCollection;
		}


		public	string status {get; set;}
		public	string customer {get; set;}
		public	XsdBasedModel.elementWithEmbededTypeInPlace elementWithEmbededTypeInPlace {get; set;}
		public	int? billto {get; set;}
		public	string shipto {get; set;}
		public	XsdBasedModel.elementWithEmbededType elementWithEmbededType {get; set;}
		public	XsdBasedModel.simpleType elemSimpleType {get; set;}
		public	XsdBasedModel.elemEmptyType_EmptyComplex elemEmptyType {get; set;}
		public	XsdBasedModel.enumType? enumField {get; set;}
		public	XsdBasedModel.enumType[] enumFieldCollection {get; set;}
	}
}


