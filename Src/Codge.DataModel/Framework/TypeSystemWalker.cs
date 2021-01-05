﻿using Codge.DataModel.Descriptors;
using System;

namespace Codge.DataModel.Framework
{
    public interface INodeEventHandler { }

    public interface ICompositeNodeEventHandler<T> : INodeEventHandler
    {
        void OnEnter(T node);
        void OnLeave(T node);
    }

    public interface IAtomicNodeEnventHandler<T> : INodeEventHandler
    {
        void Handle(T node);
    }

    public static class NodeEventHandlerExtensions
    {
        public static void InvokeAtomicEventHandler<T>(this INodeEventHandler handler, T item)
        {
            if (handler is IAtomicNodeEnventHandler<T> atomicEventHandler)
            {
                atomicEventHandler.Handle(item);
            }
        }
    }


    public class TypeSystemWalker
    {
        public static void Walk(NamespaceDescriptor namespaceDescriptor, INodeEventHandler nodeEventHandler)
        {
            foreach (var t in namespaceDescriptor.Types)
            {
                switch (t)
                {
                    case CompositeTypeDescriptor composite:
                        nodeEventHandler.InvokeAtomicEventHandler(composite);
                        break;
                    case PrimitiveTypeDescriptor primitive:
                        nodeEventHandler.InvokeAtomicEventHandler(primitive);
                        break;
                    case EnumerationTypeDescriptor enumeration:
                        nodeEventHandler.InvokeAtomicEventHandler(enumeration);
                        break;
                    default:
                        throw new Exception("Unknown type");
                }
            }

            var compositeHander = nodeEventHandler as ICompositeNodeEventHandler<NamespaceDescriptor>;
            foreach (var n in namespaceDescriptor.Namespaces)
            {
                if (compositeHander != null)
                    compositeHander.OnEnter(n);

                Walk(n, nodeEventHandler);

                if (compositeHander != null)
                    compositeHander.OnLeave(n);
            }
        }
    }
}
