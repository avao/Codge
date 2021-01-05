using System.Collections.Generic;

namespace Codge.Generator.Presentations.Xsd
{
    public static class AttachedDataExtensions
    {
        public static IDictionary<string, object> SetIsAttribute(this IDictionary<string, object> attachedData)
        {
            attachedData.Add("isAttribute", true);
            return attachedData;
        }

        public static IDictionary<string, object> SetIsContent(this IDictionary<string, object> attachedData)
        {
            attachedData.Add("isContent", true);
            return attachedData;
        }
    }
}
