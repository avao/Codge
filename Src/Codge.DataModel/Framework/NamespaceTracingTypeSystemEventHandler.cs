using Codge.DataModel.Descriptors;
using System;
using System.Collections.Generic;

namespace Codge.DataModel.Framework
{
    public class NamespaceTracingTypeSystemEventHandler<T>
    : ICompositeNodeEventHandler<NamespaceDescriptor>
    {
        private readonly Stack<T> _namespaces;
        private Func<T, NamespaceDescriptor, T> _converter;
        public NamespaceTracingTypeSystemEventHandler(T ns, Func<T, NamespaceDescriptor, T> converter)
        {
            _namespaces = new Stack<T>();
            _namespaces.Push(ns);
            _converter = converter;
        }

        public void OnEnter(NamespaceDescriptor node)
        {
            _namespaces.Push(_converter(Namespace, node));
        }

        public void OnLeave(NamespaceDescriptor node)
        {
            _namespaces.Pop();
        }

        protected T Namespace { get { return _namespaces.Peek(); } }
    }
}
