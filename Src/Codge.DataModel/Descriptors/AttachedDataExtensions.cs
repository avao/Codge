﻿namespace Codge.DataModel.Descriptors
{
    //TODO copy-past
    public static class AttachedDataExtensions
    {
        private static class Names
        {
            public const string IsAttribute = "isAttribute";
            public const string IsOptional = "isOptional";
            public const string IsContent = "isContent";
            public const string IsCData = "isCData";
        }

        public static bool IsAttribute(this FieldDescriptor field)
        {
            return field.GetAttachedData<bool>(Names.IsAttribute);
        }

        public static void SetIsAttribute(this FieldDescriptor field, bool value)
        {
            field.SetAttachedData(Names.IsAttribute, value);
        }

        public static bool IsContent(this FieldDescriptor field)
        {
            return field.GetAttachedData<bool>(Names.IsContent);
        }

        public static bool IsCData(this FieldDescriptor field)
        {
            return field.GetAttachedData<bool>(Names.IsCData);
        }

        public static bool IsOptional(this FieldDescriptor field)
        {
            return field.GetAttachedData<bool>(Names.IsOptional);
        }
        public static void SetIsOptional(this FieldDescriptor field, bool value)
        {
            field.SetAttachedData(Names.IsOptional, value);
        }


        private static T GetAttachedData<T>(this FieldDescriptor field, string name)
        {
            object value;
            if (field.AttachedData.TryGetValue(name, out value) && value is T)
            {
                return (T)value;
            }
            return default(T);
        }

        private static void SetAttachedData<T>(this FieldDescriptor field, string name, T value)
        {
            field.AttachedData[name] = value;
        }
    }
}