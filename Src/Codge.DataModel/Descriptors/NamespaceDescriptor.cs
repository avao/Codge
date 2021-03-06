﻿using Qart.Core.Validation;
using System.Collections.Generic;
using System.Linq;

namespace Codge.DataModel.Descriptors
{
    public class NamespaceDescriptor
    {
        public string Name { get; }

        private IList<NamespaceDescriptor> _namespaces;
        public IEnumerable<NamespaceDescriptor> Namespaces { get { return _namespaces; } }

        private IList<TypeDescriptor> _types;
        public IEnumerable<TypeDescriptor> Types { get { return _types; } }

        public NamespaceDescriptor(string name)
        {
            Require.DoesNotContain(name, ".");

            _namespaces = new List<NamespaceDescriptor>();
            _types = new List<TypeDescriptor>();

            Name = name;
        }

        public NamespaceDescriptor GetOrCreateNamespace(string fullyQualifiedName)
        {
            //TODO validation
            return GetOrCreateNamespace(fullyQualifiedName.Split(new[] { '.' }));
        }

        public CompositeTypeDescriptor CreateCompositeType(string name)
        {
            return CreateCompositeType(name, null);
        }

        public CompositeTypeDescriptor CreateCompositeType(string name, string baseTypeName)
        {
            var type = new CompositeTypeDescriptor(name, this, baseTypeName, "TODO");//TODO
            _types.Add(type);
            return type;
        }

        //TODO supply base type
        public PrimitiveTypeDescriptor CreatePrimitiveType(string name)
        {
            var type = new PrimitiveTypeDescriptor(name, this);
            _types.Add(type);
            return type;
        }

        public EnumerationTypeDescriptor CreateEnumerationType(string name)
        {
            var type = new EnumerationTypeDescriptor(name, this);
            _types.Add(type);
            return type;
        }

        public void Add(NamespaceDescriptor ns)
        {
            Require.That(() => !_namespaces.Any(_ => _.Name == ns.Name), () => "Namespace with name " + ns.Name + "] already exists");
            _namespaces.Add(ns);
        }


        #region Implementation details

        private NamespaceDescriptor GetOrCreateNamespace(IEnumerable<string> parts)
        {
            if (!parts.Any())
                return this;

            string name = parts.First();

            var ns = _namespaces.FirstOrDefault(_ => _.Name == name);
            if (ns == null)
            {//does not exist 
                ns = new NamespaceDescriptor(name);
                _namespaces.Add(ns);
            }

            return ns.GetOrCreateNamespace(parts.Skip(1));
        }

        #endregion

    }
}
