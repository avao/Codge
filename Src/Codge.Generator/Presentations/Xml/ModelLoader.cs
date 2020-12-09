using Codge.DataModel.Descriptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Codge.Generator.Presentations.Xml
{
    public class ModelLoader
    {
        public static ModelDescriptor Load(XmlReader reader)
        {
            var serialiser = new XmlSerializer(typeof(ModelDesc), "http://codge/Model.xsd");
            var modelrep = (ModelDesc)serialiser.Deserialize(reader);

            var model = new ModelDescriptor(modelrep.Namespace.name, modelrep.Namespace.name);
            processNamespace(modelrep.Namespace, model.RootNamespace);

            return model;
        }


        private static void processNamespace(NamespaceDesc ns, NamespaceDescriptor namespaceDescriptor)
        {
            if (ns.Items != null)
            {
                foreach (var t in ns.Items)
                {
                    if (t is Composite composite)
                    {
                        var descriptor = namespaceDescriptor.CreateCompositeType(composite.name, composite.baseType);
                        if (composite.Field != null)
                        {
                            foreach (var field in composite.Field)
                            {
                                var newField = descriptor.AddField(field.name, field.type, field.isCollectionSpecified && field.isCollection);
                                if (field.AttachedData != null)
                                {//TODO hack for boolean values
                                    field.AttachedData.Select(_ => new KeyValuePair<string, object>(_.key, _.value == "True" ? (object)true : _.value)).ToList().ForEach(_ => newField.AttachedData.Add(_));
                                }
                            }
                        }
                    }
                    else
                    {
                        if (t is Primitive primitive)
                        {
                            var descriptor = namespaceDescriptor.CreatePrimitiveType(primitive.name);
                        }
                        else
                        {
                            if (t is Enumeration enumeration)
                            {
                                var descriptor = namespaceDescriptor.CreateEnumerationType(enumeration.name);

                                int i = 0;
                                foreach (var item in enumeration.Item)
                                {
                                    if (item.valueSpecified)
                                    {
                                        descriptor.AddItem(item.name, item.value);
                                        i = item.value;
                                    }
                                    else
                                    {
                                        descriptor.AddItem(item.name, i);
                                    }
                                    ++i;
                                }

                            }
                            else
                            {
                                throw new Exception("Unknown type");
                            }
                        }
                    }
                }
            }

            if (ns.Namespace != null)
            {
                foreach (var n in ns.Namespace)
                {
                    processNamespace(n, namespaceDescriptor.GetOrCreateNamespace(n.name));
                }
            }
        }
    }
}
