using Codge.DataModel.Descriptors;
using Qart.Core.Collections;
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
            foreach (var t in ns.Items.ToEmptyIfNull())
            {
                var descriptor = t switch
                {
                    Composite composite => CreateCompositeType(composite, namespaceDescriptor),
                    Primitive primitive => namespaceDescriptor.CreatePrimitiveType(primitive.name),
                    Enumeration enumeration => CreateEnumerationType(enumeration, namespaceDescriptor),
                    _ => throw new Exception("Unknown type")
                };
            }

            foreach (var n in ns.Namespace.ToEmptyIfNull())
            {
                processNamespace(n, namespaceDescriptor.GetOrCreateNamespace(n.name));
            }
        }

        private static TypeDescriptor CreateCompositeType(Composite composite, NamespaceDescriptor namespaceDescriptor)
        {
            var descriptor = namespaceDescriptor.CreateCompositeType(composite.name, composite.baseType);
            foreach (var field in composite.Field.ToEmptyIfNull())
            {
                var newField = descriptor.AddField(field.name, field.type, field.isCollectionSpecified && field.isCollection);
                if (field.AttachedData != null)
                {//TODO hack for boolean values
                    field.AttachedData.Select(_ => new KeyValuePair<string, object>(_.key, _.value == "True" ? (object)true : _.value)).ToList().ForEach(_ => newField.AttachedData.Add(_));
                }
            }
            return descriptor;
        }

        private static TypeDescriptor CreateEnumerationType(Enumeration enumeration, NamespaceDescriptor namespaceDescriptor)
        {
            var descriptor = namespaceDescriptor.CreateEnumerationType(enumeration.name);

            int i = 0;
            foreach (var item in enumeration.Item)
            {
                i = item.valueSpecified ? item.value : i;
                descriptor.AddItem(item.name, i);
                ++i;
            }
            return descriptor;
        }

    }
}
