using System.Collections.Generic;
using System.Linq;

namespace Codge.DataModel
{
    public static class NamespaceExtensions
    {
        public static IEnumerable<Namespace> GetParentNamespaces(this Namespace descriptor)
        {
            var namespaces = new Stack<Namespace>();
            var ns = descriptor;
            while (ns != null && ns.Name != null)
            {
                namespaces.Push(ns);
                ns = ns.ParentNamespace;
            }
            return namespaces;
        }

        public static IEnumerable<TypeBase> AllTypes(this Namespace ns)
        {
            var types = new List<TypeBase>(ns.Types);
            foreach (var nestedNs in ns.Namespaces)
            {
                types.AddRange(nestedNs.AllTypes());
            }
            return types;
        }


        public static string GetFullName(this Namespace ns, string separator)
        {
            var namespaces = ns.GetParentNamespaces();
            return string.Join(separator, namespaces.Select(v => v.Name));
        }


        internal static TypeBase findTypeByPartialName(this Namespace ns, string name)
        {
            var type = ns.findTypeByQName(name);
            if (type == null && ns.ParentNamespace != null)
            {
                type = ns.ParentNamespace.findTypeByPartialName(name);
            }
            return type;
        }

        internal static TypeBase findTypeByQName(this Namespace ns, string name)
        {
            int pos = name.IndexOf('.');
            if (pos == -1)
            {
                return ns.Types.FirstOrDefault(item => item.Name == name);
            }
            else
            {
                string nsName = name.Substring(0, pos);
                var nestedNs = ns.Namespaces.FirstOrDefault(item => item.Name == nsName);
                if (nestedNs != null)
                    return findTypeByQName(nestedNs, name.Substring(pos + 1));
            }

            return null;
        }

        public static bool IsGlobal(this Namespace ns)
        {
            return ns.Name == null;
        }
    }

    public class Namespace
    {
        protected IList<TypeBase> _types;
        public IEnumerable<TypeBase> Types => _types;

        private IList<Namespace> _namespaces;
        public IEnumerable<Namespace> Namespaces => _namespaces;

        public string Name { get; }

        public Namespace ParentNamespace { get; }

        public TypeSystem TypeSystem { get; protected set; }

        internal Namespace(string name, Namespace parentNs)
            : this(name, parentNs.TypeSystem, parentNs)
        { }

        internal Namespace(string name, TypeSystem typeSystem)
            : this(name, typeSystem, null)
        { }

        protected Namespace()
        { //for TypeSystem
            _types = new List<TypeBase>();
            _namespaces = new List<Namespace>();
        }

        private Namespace(string name, TypeSystem typeSystem, Namespace parentNs)
            : this()
        {
            Name = name;
            ParentNamespace = parentNs;
            TypeSystem = typeSystem;
        }

        internal CompositeType CreateCompositeType(string name)
        {
            return CreateCompositeType(name, null);
        }

        internal CompositeType CreateCompositeType(string name, CompositeType baseType)
        {
            var type = new CompositeType(name, this);
            _types.Add(type);
            return type;
        }

        internal PrimitiveType CreatePrimitiveType(string name)
        {
            var type = new PrimitiveType(name, this);
            _types.Add(type);
            return type;
        }

        internal EnumerationType CreateEnumerationType(string name)
        {
            var type = new EnumerationType(name, this);
            _types.Add(type);
            return type;
        }


        internal T GetType<T>(string name)
            where T : TypeBase
        {
            return Types.SingleOrDefault(_ => _.Name == name) as T;
        }

        internal Namespace GetOrCreateNamespace(string name)
        {
            var ns = _namespaces.FirstOrDefault(_ => _.Name == name);
            if (ns == null)
            {
                ns = new Namespace(name, this);
                _namespaces.Add(ns);
            }
            return ns;
        }

        internal Namespace GetOrCreateNamespaceByQName(string name)
        {
            var parts = name.Split(new[] { '.' });
            var ns = Namespaces.FirstOrDefault(_ => _.Name == parts[0]);
            if (ns == null)
            {
                ns = new Namespace(name, this);
                _namespaces.Add(ns);
            }
            return ns;
        }
    }
}
