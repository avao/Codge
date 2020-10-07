using Codge.DataModel;
using System;

namespace Codge.BasicModel.Templates.CS
{
    internal static class Extensions
    {
        public static string SerialiserQName(this TypeBase type)
        {
            return QName(type, "Serialisers");
        }

        public static string TypesQName(this TypeBase type)
        {
            return QName(type, "Types");
        }

        public static string QName(this TypeBase type)
        {
            return type.GetFullName(".");
        }

        private static string QName(this TypeBase type, string prefix)
        {
            if (type.IsBuiltIn())
            {
                return type.Name;
            }

            if (type.IsComposite() || type.IsPrimitive() || type.IsEnum())
            {
                return prefix + "." + type.GetFullName(".");
            }

            throw new NotSupportedException("Not supported type name [" + type.Name + "]");
        }
    }
}
