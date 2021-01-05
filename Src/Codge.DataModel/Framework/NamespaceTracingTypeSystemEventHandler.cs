using Codge.DataModel.Descriptors;
using System;
using System.Collections.Generic;

namespace Codge.DataModel.Framework
{
    public class NamespaceTracingTypeSystemEventHandler<T>
        : ICompositeNodeEventHandler<NamespaceDescriptor>
    {
        private readonly Stack<T> _namespaces;
        private Func<T, NamespaceDescriptor, T> _converterFunc;

        public NamespaceTracingTypeSystemEventHandler(T ns, Func<T, NamespaceDescriptor, T> converterFunc)
        {
            _namespaces = new Stack<T>();
            _namespaces.Push(ns);
            _converterFunc = converterFunc;
        }

        public void OnEnter(NamespaceDescriptor node)
        {
            _namespaces.Push(_converterFunc(Namespace, node));
        }

        public void OnLeave(NamespaceDescriptor node)
        {
            _namespaces.Pop();
        }

        protected T Namespace { get { return _namespaces.Peek(); } }
    }
}
