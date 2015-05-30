using BasicModel.CS.Serialisation;

namespace Serialisers.rootNs
{

	public static class Registrar
	{
		public static void RegisterSerialisers(SerialisationContext context)
		{
			context.RegisterSerialiser<Types.rootNs.myType2>(new Serialisers.rootNs.myType2());
			context.RegisterSerialiser<Types.rootNs.nestedNs.typeInNestedNs>(new Serialisers.rootNs.nestedNs.typeInNestedNs());
		}
	}
}

