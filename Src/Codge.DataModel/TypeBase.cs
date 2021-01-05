using System.Collections.Generic;

namespace Codge.DataModel
{
    public static class TypeBaseExtensions
    {
        public static string GetFullName(this TypeBase type, string separator)
        {
            return type.Namespace.IsGlobal()
                ? type.Name
                : type.Namespace.GetFullName(separator) + separator + type.Name;
        }

        public static string GetFullName(this TypeBase type, string prefix, string separator)
        {
            return prefix + separator + type.GetFullName(separator);
        }

        public static bool IsComposite(this TypeBase type) => type is CompositeType;
        public static bool IsPrimitive(this TypeBase type) => type is PrimitiveType;
        public static bool IsBuiltIn(this TypeBase type) => type is BuiltInType;
        public static bool IsEnum(this TypeBase type) => type is EnumerationType;
    }

    public abstract class TypeBase
    {
        public string Name { get; }
        public Namespace Namespace { get; }

        abstract public IEnumerable<TypeBase> Dependencies { get; }

        protected TypeBase(string name, Namespace nameSpace)
        {
            Namespace = nameSpace;
            Name = name;
        }
    }
}
