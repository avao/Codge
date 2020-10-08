using Codge.DataModel.Descriptors;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codge.DataModel.Framework
{
    public class ModelProcessor
    {
        private readonly ILogger _logger;

        public ModelProcessor(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ModelProcessor>();
        }

        public Model Compile(TypeSystem typeSystem, ModelDescriptor model)
        {
            var ns = typeSystem.GetOrCreateNamespace(model.RootNamespace.Name);

            var compiledModel = new Model(ns);

            //first pass - create type stubs
            TypeSystemWalker.Walk(model.RootNamespace, new TypeSystemCompileHandlerFirstPass(compiledModel.Namespace));

            //second pass - fill composite types with fields
            TypeSystemWalker.Walk(model.RootNamespace, new TypeSystemCompileHandlerSecondPass(compiledModel.Namespace));

            return compiledModel;
        }

        public ModelDescriptor MergeToLhs(ModelDescriptor lhs, ModelDescriptor rhs)
        {
            TypeSystemWalker.Walk(rhs.RootNamespace, new ModelMergeTypeSystemEventHandler(lhs.RootNamespace, _logger));
            return lhs;
        }

        public ModelDescriptor MergeToLhs(IEnumerable<ModelDescriptor> descriptors)
        {
            var model = descriptors.First();
            descriptors.Skip(1).Aggregate(model, MergeToLhs);
            return model;
        }

        static int id = 3000;
        private static int GetId(string name)
        {
            return id++;//TODO
        }

        public class TypeSystemCompileHandlerFirstPass
            : NamespaceTracingTypeSystemEventHandler<Namespace>
            , IAtomicNodeEnventHandler<PrimitiveTypeDescriptor>
            , IAtomicNodeEnventHandler<CompositeTypeDescriptor>
            , IAtomicNodeEnventHandler<EnumerationTypeDescriptor>
        {
            public TypeSystemCompileHandlerFirstPass(Namespace ns)
                : base(ns, (n, descriptor) => n.GetOrCreateNamespace(descriptor.Name))
            {
            }


            public void Handle(PrimitiveTypeDescriptor primitive)
            {
                Namespace.CreatePrimitiveType(GetId(primitive.Name), primitive.Name);
            }

            public void Handle(CompositeTypeDescriptor composite)
            {
                Namespace.CreateCompositeType(GetId(composite.Name), composite.Name);
            }

            public void Handle(EnumerationTypeDescriptor enumeration)
            {
                var descriptor = Namespace.CreateEnumerationType(GetId(enumeration.Name), enumeration.Name);
                foreach (var item in enumeration.Items)
                {
                    if (item.Value.HasValue)
                        descriptor.AddItem(item.Name, item.Value.Value);
                    else
                        descriptor.AddItem(item.Name);
                }
            }
        }

        public class TypeSystemCompileHandlerSecondPass
            : NamespaceTracingTypeSystemEventHandler<Namespace>
            , IAtomicNodeEnventHandler<CompositeTypeDescriptor>
        {
            public TypeSystemCompileHandlerSecondPass(Namespace ns)
                : base(ns, (n, descriptor) => n.GetOrCreateNamespace(descriptor.Name))
            {
            }

            public void Handle(CompositeTypeDescriptor compositeDescriptor)
            {
                var compositeType = Namespace.GetType<CompositeType>(compositeDescriptor.Name);

                if (compositeDescriptor.BaseTypeName != null)
                {
                    var baseType = Namespace.findTypeByPartialName(compositeDescriptor.BaseTypeName);
                    compositeType.AddBase((CompositeType)baseType);
                }

                foreach (var field in compositeDescriptor.Fields)
                {
                    var fieldType = Namespace.findTypeByPartialName(field.TypeName);
                    if (fieldType == null)
                    {
                        throw new Exception("Field [" + field.Name + "] is of unknown type [" + field.TypeName + "]");
                    }

                    if (field.IsCollection)
                        compositeType.AddCollectionField(field.Name, fieldType, field.AttachedData);
                    else
                        compositeType.AddField(field.Name, fieldType, field.AttachedData);
                }
            }
        }
    }
}
