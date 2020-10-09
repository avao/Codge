using Codge.BasicModel.CS.Serialisation;

namespace Serialisers.XsdBasedModel
{

	public static class Registrar
	{
		public static void RegisterSerialisers(SerialisationContext context)
		{
			context.RegisterSerialiser<Types.XsdBasedModel.complexTypeWithAField>(new Serialisers.XsdBasedModel.complexTypeWithAField());
			context.RegisterSerialiser<Types.XsdBasedModel.emptyComplexType>(new Serialisers.XsdBasedModel.emptyComplexType());
			context.RegisterSerialiser<Types.XsdBasedModel.ordertype>(new Serialisers.XsdBasedModel.ordertype());
			context.RegisterSerialiser<Types.XsdBasedModel.elementWithEmbededTypeInPlace>(new Serialisers.XsdBasedModel.elementWithEmbededTypeInPlace());
			context.RegisterSerialiser<Types.XsdBasedModel.elemEmptyType_EmptyComplex>(new Serialisers.XsdBasedModel.elemEmptyType_EmptyComplex());
			context.RegisterSerialiser<Types.XsdBasedModel.complexTypeWithContent>(new Serialisers.XsdBasedModel.complexTypeWithContent());
			context.RegisterSerialiser<Types.XsdBasedModel.complexTypeWithContentAndParentField>(new Serialisers.XsdBasedModel.complexTypeWithContentAndParentField());
			context.RegisterSerialiser<Types.XsdBasedModel.complexTypeWithASequence>(new Serialisers.XsdBasedModel.complexTypeWithASequence());
			context.RegisterSerialiser<Types.XsdBasedModel.complexTypeDerivedFromSequence>(new Serialisers.XsdBasedModel.complexTypeDerivedFromSequence());
			context.RegisterSerialiser<Types.XsdBasedModel.complexTypeWithContentWithAttribute>(new Serialisers.XsdBasedModel.complexTypeWithContentWithAttribute());
			context.RegisterSerialiser<Types.XsdBasedModel.complexTypeWithSimpleContent>(new Serialisers.XsdBasedModel.complexTypeWithSimpleContent());
			context.RegisterSerialiser<Types.XsdBasedModel.complexTypeWithInlineElement>(new Serialisers.XsdBasedModel.complexTypeWithInlineElement());
			context.RegisterSerialiser<Types.XsdBasedModel.recursiveType>(new Serialisers.XsdBasedModel.recursiveType());
			context.RegisterSerialiser<Types.XsdBasedModel.allXsdTypes>(new Serialisers.XsdBasedModel.allXsdTypes());
			context.RegisterSerialiser<Types.XsdBasedModel.elementWithEmbededType>(new Serialisers.XsdBasedModel.elementWithEmbededType());
		}
	}
}

