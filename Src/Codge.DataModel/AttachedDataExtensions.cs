using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.DataModel
{
    public static class AttachedDataExtensions
    {
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
