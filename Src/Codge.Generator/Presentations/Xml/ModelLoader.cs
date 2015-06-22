using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Codge.Generator.Presentations;
using Codge.DataModel;
using Codge.DataModel.Descriptors;

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

        static int id = 3000;
        private static int GetId(string typeName)
        {//|TODO
            return id++;
        }

        private static void processNamespace(NamespaceDesc ns, NamespaceDescriptor namespaceDescriptor)
        {
            if (ns.Items != null)
            {
                foreach (var t in ns.Items)
                {
                    var composite = t as Composite;
                    if (composite != null)
                    {
                        var descriptor = namespaceDescriptor.CreateCompositeType(composite.name);
                        if (composite.Field != null)
                        {
                            foreach (var field in composite.Field)
                            {
                                descriptor.AddField(field.name, field.type, field.isCollectionSpecified && field.isCollection);
                            }
                        }
                    }
                    else
                    {
                        var primitive = t as Primitive;

                        if (primitive != null)
                        {
                            var descriptor = namespaceDescriptor.CreatePrimitiveType(primitive.name);
                        }
                        else
                        {
                            var enumeration = t as Enumeration;
                            if (enumeration != null)
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
