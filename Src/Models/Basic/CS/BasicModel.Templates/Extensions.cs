using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codge.DataModel;

namespace BasicModel.Templates.CS
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
                switch(type.Name)
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


        public static string GetNativeType(this CompositeType.Field field)
        {
            //TODO hacks
            if (field.IsCollection)
                return field.Type.GetFullName(".") + "[]";
            if (field.IsOptional() && field.Type.IsBuiltIn() && field.Type.Name != "string")
                return field.Type.GetFullName(".") + "?";
            return field.Type.GetFullName(".");
        }

        public static string GetMemberName(this CompositeType type, CompositeType.Field field)
        {
            if(type.Name == field.Name)
            {
                return field.Name + "1";//TODO hack
            }
            return field.Name;
        }

        public static string GetParameterName(this CompositeType.Field field)
        {
            return "_" + field.Name;
        }

        public static string GetCtorParamters(this CompositeType type)
        {
            return string.Join(",", type.Fields.Select(f => f.GetNativeType() + " " + GetParameterName(f)));
        }

        private static class Names
        {
            public const string IsAttribute = "isAttribute";
            public const string IsOptional = "isOptional";
            public const string IsContent = "isContent";
        }

        public static bool IsAttribute(this CompositeType.Field field)
        {
            return field.GetAttachedData<bool>(Names.IsAttribute);
        }

        public static void SetIsAttribute(this CompositeType.Field field, bool value)
        {
            field.SetAttachedData(Names.IsAttribute, value);
        }

        public static bool IsContent(this CompositeType.Field field)
        {
            return field.GetAttachedData<bool>(Names.IsContent);
        }


        public static bool IsOptional(this CompositeType.Field field)
        {
            return field.GetAttachedData<bool>(Names.IsOptional);
        }
        public static void SetIsOptional(this CompositeType.Field field, bool value)
        {
            field.SetAttachedData(Names.IsOptional, value);
        }


        private static T GetAttachedData<T>(this CompositeType.Field field, string name)
        {
            object value;
            if (field.AttachedData.TryGetValue(name, out value) && value is T)
            {
                return (T)value;
            }
            return default(T);
        }

        private static void SetAttachedData<T>(this CompositeType.Field field, string name, T value)
        {
            field.AttachedData[name] = value;
        }
    }
}
