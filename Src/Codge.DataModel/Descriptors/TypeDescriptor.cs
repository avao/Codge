﻿using System.Collections.Generic;
using System.Linq;

namespace Codge.DataModel.Descriptors
{
    public abstract class TypeDescriptor
    {
        public string Name { get; }
        public NamespaceDescriptor Namespace { get; }

        protected TypeDescriptor(string name, NamespaceDescriptor nameSpace)
        {
            Name = name;
            Namespace = nameSpace;
        }
    }

    public static class TypeDescriptorExtensions
    {
        public static TypeDescriptor FindByName(this IEnumerable<TypeDescriptor> types, string name)
        {
            return types.SingleOrDefault(_ => _.Name == name);
        }
    }
}
