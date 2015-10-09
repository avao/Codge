using Codge.DataModel.Descriptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.DataModel
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
        public static void TryInvokeAtomicEventHandler<T, TItem>(this INodeEventHandler handler, TItem item)
        where T : class
        {
            var typedItem = item as T;
            if (typedItem != null)
            {
                InvokeAtomicEventHandler<T>(handler, typedItem);
            }
        }

        public static void InvokeAtomicEventHandler<T>(this INodeEventHandler handler, T item)
        {
            var atomicEventHandler = handler as IAtomicNodeEnventHandler<T>;
            if (atomicEventHandler != null)
                atomicEventHandler.Handle(item);
        }
    }


    public class TypeSystemWalker
    {
        public static void Walk(NamespaceDescriptor namespaceDescriptor, INodeEventHandler nodeEventHandler)
        {
            foreach (var t in namespaceDescriptor.Types)
            {
                var composite = t as CompositeTypeDescriptor;
                if (composite != null)
                {
                    nodeEventHandler.InvokeAtomicEventHandler<CompositeTypeDescriptor>(composite);
                }
                else
                {
                    var primitive = t as PrimitiveTypeDescriptor;
                    if (primitive != null)
                    {
                        nodeEventHandler.InvokeAtomicEventHandler<PrimitiveTypeDescriptor>(primitive);
                    }
                    else
                    {
                        var enumeration = t as EnumerationTypeDescriptor;
                        if (enumeration != null)
                        {
                            nodeEventHandler.InvokeAtomicEventHandler<EnumerationTypeDescriptor>(enumeration);
                        }
                        else
                        {
                            throw new Exception("Unknown type");
                        }
                    }
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
