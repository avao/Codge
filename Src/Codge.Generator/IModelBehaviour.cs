using Codge.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.Generator
{
    public interface IModelBehaviour
    {
        string GetMemberName(CompositeType.Field field);
        string GetParameterName(CompositeType.Field field);
        string GetNativeType(CompositeType.Field field);
        string GetEnumItemName(EnumerationType.Item item);
    }

    public static class ModelBehaviourExtensions
    {
        public static string GetCtorParamters(this IModelBehaviour modelBehaviour, CompositeType type)
        {
            return string.Join(",", type.Fields.Select(f => modelBehaviour.GetNativeType(f) + " " + modelBehaviour.GetParameterName(f)));
        }
    }

    public class ModelBehaviour : IModelBehaviour
    {
        private readonly HashSet<string> _reservedWords;
        public ModelBehaviour(HashSet<string> reservedWords)
        {
            _reservedWords = reservedWords;
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

        public string GetEnumItemName(EnumerationType.Item item)
        {
            var builder = new StringBuilder(item.Name.Length);
            if (Char.IsDigit(item.Name[0]))
                builder.Append('_');

            foreach (char c in item.Name)
            {
                if (Char.IsLetterOrDigit(c))
                    builder.Append(c);
                else
                    builder.Append('_');
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
    }
}
