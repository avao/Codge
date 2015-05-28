

namespace Types.XsdBasedModel
{
	public class ordertype
	{
	    public ordertype()
		{}


		public ordertype(string _status,string _customer,string _orderdetails,int? _billto,string _shipto,XsdBasedModel.elementWithEmbededType _elementWithEmbededType,XsdBasedModel.simpleType? _elemSimpleType,XsdBasedModel.elemEmptyType_EmptyComplex _elemEmptyType,XsdBasedModel.enumType? _enumField)
		{
			status = _status;
			customer = _customer;
			orderdetails = _orderdetails;
			billto = _billto;
			shipto = _shipto;
			elementWithEmbededType = _elementWithEmbededType;
			elemSimpleType = _elemSimpleType;
			elemEmptyType = _elemEmptyType;
			enumField = _enumField;
		}


		public	string status {get; set;}
		public	string customer {get; set;}
		public	string orderdetails {get; set;}
		public	int? billto {get; set;}
		public	string shipto {get; set;}
		public	XsdBasedModel.elementWithEmbededType elementWithEmbededType {get; set;}
		public	XsdBasedModel.simpleType? elemSimpleType {get; set;}
		public	XsdBasedModel.elemEmptyType_EmptyComplex elemEmptyType {get; set;}
		public	XsdBasedModel.enumType? enumField {get; set;}
	}
}


