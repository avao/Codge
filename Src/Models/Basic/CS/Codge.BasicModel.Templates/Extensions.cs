using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codge.DataModel;

namespace Codge.BasicModel.Templates.CS
{
    internal static class Extensions
    {
        public static string SerialiserQName(this TypeBase type)
        {
            return QName(type, "Serialisers");
        }

        public static string QName(this TypeBase type)
        {
            return QName(type, "Types");
        }

        public static string AssemblyName(this TypeBase type)
        {
            return "BasicModel";//TODO
        }


        private static string QName(this TypeBase type, string prefix)
        {
            if (type.IsComposite())
            {
                return prefix + "." + type.GetFullName(".");
            }
            else if (type.IsBuiltIn())
            {
                return type.Name;
            }
            else if (type.IsPrimitive() || type.IsEnum())
            {
                return prefix + "." + type.GetFullName(".");
            }

            throw new NotSupportedException("Not supported type name [" + type.Name + "]");
        }

        public static string CoherenceType(this CompositeType.Field field)
        {
            if (field.IsCollection)
            {
                return "Collection";
            }
            return field.Type.CoherenceType();
        }

        public static string CoherenceType(this TypeBase type)
        {
            if (type.IsComposite())
            {
                return "Object";
            }
            else if (type.IsBuiltIn())
            {
                switch (type.Name)
                {
                    case "bool":
                        return "Boolean";
                    case "string":
                        return "String";
                    case "int":
                        return "Int32";
                    case "double":
                        return "Double";
                }
            }
            else if (type.IsPrimitive())
            {
                return "Object";//TODO
            }
            else if (type.IsEnum())
            {
                return "Integer";
            }

            throw new NotSupportedException("Not supported type name [" + type.Name + "]");
        }
    }
}
