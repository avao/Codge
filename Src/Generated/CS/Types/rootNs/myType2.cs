

namespace Types.rootNs
{
	public class myType2
	{
		public myType2(int _IntField,bool _BoolField,int[] _CollectionOfInt)
		{
			IntField = _IntField;
			BoolField = _BoolField;
			CollectionOfInt = _CollectionOfInt;
		}


		public	int IntField {get; private set;}
		public	bool BoolField {get; private set;}
		public	int[] CollectionOfInt {get; private set;}
	}
}


