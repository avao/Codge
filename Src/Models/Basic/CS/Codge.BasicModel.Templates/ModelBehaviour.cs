using Codge.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codge.BasicModel.Templates.CS
{
    public class ModelBehaviour
    {
        private readonly HashSet<string> _reservedWords;
        private readonly IDictionary<string, IDictionary<string, string>> _enumItemsCache;

        public ModelBehaviour(HashSet<string> reservedWords)
        {
            _reservedWords = reservedWords;
            _enumItemsCache = new Dictionary<string, IDictionary<string, string>>();
        }

        public string GetMemberName(CompositeType.Field field)
        {
            if (field.ContainingType.Name == field.Name)
            {
                return field.Name + "1";//TODO hack
            }

            if (_reservedWords.Contains(field.Name))
            {
                return field.Name + "_";
            }

            return field.Name;
        }


        public string GetParameterName(CompositeType.Field field)
        {
            return "_" + field.Name;
        }

        public string GetEnumItemName(EnumerationType enumType, EnumerationType.Item item)
        {
            IDictionary<string, string> mappedValues;
            string typeName = enumType.GetFullName(".");
            if (!_enumItemsCache.TryGetValue(typeName, out mappedValues))
            {
                mappedValues = new Dictionary<string, string>();

                var reversedMappedValues = new Dictionary<string, string>();

                foreach (var it in enumType.Items)
                {
                    string value = MapItemName(it.Name);
                    string clashedItemName;
                    if (reversedMappedValues.TryGetValue(value, out clashedItemName))
                    {//clash can be resolved by appending a hash to all values 
                        mappedValues[clashedItemName] = AddHash(MapItemName(clashedItemName), clashedItemName);
                        value = AddHash(value, it.Name);
                    }

                    reversedMappedValues.Add(value, it.Name);
                    mappedValues.Add(it.Name, value);
                }

                _enumItemsCache.Add(typeName, mappedValues);
            }

            return mappedValues[item.Name];
        }

        private static string AddHash(string current, string name)
        {
            return current + "_" + Math.Abs(name.GetHashCode());
        }

        private static string MapItemName(string name)
        {
            var builder = new StringBuilder(name.Length);
            if (Char.IsDigit(name[0]))
                builder.Append('_');

            foreach (char c in name)
            {
                if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9'))
                {
                    builder.Append(c);
                }
                else
                {
                    builder.Append('_');
                }
            }

            return builder.ToString();
        }


        public string GetNativeType(CompositeType.Field field)
        {
            if (field.Type.IsPrimitive())
            {
                if (field.IsCollection)
                    return "string[]";
                else
                    return "string";
            }

            //TODO other hacks
            if (field.IsCollection)
                return field.Type.GetFullName(".") + "[]";
            if (field.IsOptional() && ((field.Type.IsBuiltIn() && field.Type.Name != "string") || field.Type.IsEnum()))
                return field.Type.GetFullName(".") + "?";
            return field.Type.GetFullName(".");
        }

        public string GetCtorParamters(CompositeType type)
        {
            return string.Join(", ", type.GetAllFields().Select(f => GetNativeType(f) + " " + GetParameterName(f)));
        }
    }
}
