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
}
