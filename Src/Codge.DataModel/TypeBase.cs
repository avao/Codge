using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codge.DataModel
{
    public static class TypeBaseExtensions
    {
        public static string GetFullName(this TypeBase type, string separator)
        {
            if (type.Namespace.IsGlobal())
                return type.Name;
            return type.Namespace.GetFullName(separator) + separator + type.Name;
        }

        public static string GetFullName(this TypeBase type, string prefix, string separator)
        {
            return prefix + separator + type.GetFullName(separator);
        }

        public static bool IsComposite(this TypeBase type) { return type is CompositeType; }
        public static bool IsPrimitive(this TypeBase type) { return type is PrimitiveType; }
        public static bool IsBuiltIn(this TypeBase type) { return type is BuiltInType; }
        public static bool IsEnum(this TypeBase type) { return type is EnumerationType; } 
    }

    public abstract class TypeBase
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Namespace Namespace { get; private set; }
        
        abstract public IEnumerable<TypeBase> Dependencies { get; }

        protected TypeBase(int id, string name, Namespace nameSpace)
        {
            Namespace = nameSpace;
            Name = name;
            Id = id;
        }
                
    }
        
    
}
