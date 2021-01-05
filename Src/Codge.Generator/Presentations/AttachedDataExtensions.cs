using System.Collections.Generic;

namespace Codge.Generator.Presentations
{
    public static class AttachedDataExtensions
    {
        public static IDictionary<string, object> NewAttachedData()
        {
            return new Dictionary<string, object>();
        }

        public static IDictionary<string, object> SetIsOptional(this IDictionary<string, object> attachedData)
        {
            attachedData.Add("isOptional", true);
            return attachedData;
        }
    }
}
