using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codge.DataModel.Descriptors;

namespace Codge.DataModel
{
    public class ModelCompiler
    {
        public Model Compile(TypeSystem typeSystem, ModelDescriptor model)
        {
            var ns = typeSystem.GetOrCreateNamespace(model.RootNamespace.Name);

            var compiledModel = new Model(ns);

            //first pass - create type stubs
            processNamespaceFirstPass(compiledModel.Namespace, model.RootNamespace);

            //second pass - fill composite types with fields
            processNamespaceSecondPass(compiledModel.Namespace, model.RootNamespace);

            return compiledModel;
        }

        static int id = 3000;
        private static int GetId(string name)
        {
            return id++;//TODO
        }

        private static void processNamespaceFirstPass(Namespace ns, NamespaceDescriptor namespaceDescriptor)
        {
            foreach (var t in namespaceDescriptor.Types)
            {
                var composite = t as CompositeTypeDescriptor;
                if (composite != null)
                {
                    ns.CreateCompositeType(GetId(composite.Name), composite.Name);
                }
                else
                {
                    var primitive = t as PrimitiveTypeDescriptor;
                    if (primitive != null)
                    {
                        ns.CreatePrimitiveType(GetId(primitive.Name), primitive.Name);
                    }
                    else
                    {
                        var enumeration = t as EnumerationTypeDescriptor;
                        if (enumeration != null)
                        {
                            var descriptor = ns.CreateEnumerationType(GetId(enumeration.Name), enumeration.Name);
                            foreach (var item in enumeration.Items)
                            {
                                if (item.Value.HasValue)
                                    descriptor.AddItem(item.Name, item.Value.Value);
                                else
                                    descriptor.AddItem(item.Name);
                            }
                        }
                        else
                        {
                            throw new Exception("Unknown type");
                        }
                    }
                }
            }

            foreach (var n in namespaceDescriptor.Namespaces)
            {
                processNamespaceFirstPass(ns.GetOrCreateNamespace(n.Name), n);
            }
        }

        private static void processNamespaceSecondPass(Namespace ns, NamespaceDescriptor namespaceDescriptor)
        {
            foreach (var t in namespaceDescriptor.Types)
            {
                var composite = t as CompositeTypeDescriptor;
                if (composite != null)
                {
                    var descriptor = ns.GetType<CompositeType>(composite.Name);
                    foreach (var field in composite.Fields)
                    {
                        var fieldType = ns.findTypeByPartialName(field.TypeName);
                        if (fieldType == null)
                        {
                            throw new Exception("Field [" + field.Name + "] is of unknown type [" + field.TypeName + "]");
                        }

                        if(field.IsCollection)
                            descriptor.AddCollectionField(field.Name, fieldType);
                        else
                            descriptor.AddField(field.Name, fieldType);
                    }
                }
            }

            foreach (var n in namespaceDescriptor.Namespaces)
            {
                processNamespaceSecondPass(ns.GetOrCreateNamespace(n.Name), n);
            }
        }

    }
}
