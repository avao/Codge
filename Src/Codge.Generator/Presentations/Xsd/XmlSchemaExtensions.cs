using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

namespace Codge.Generator.Presentations.Xsd
{
    public static class XmlSchemaExtensions
    {
        public static bool IsEmptyType(this XmlSchemaComplexType type)
        {
            return type.ContentModel == null && type.Attributes.Count == 0 && type.ContentType.ToString() == "Empty";
        }

        public static IEnumerable<XmlSchemaEnumerationFacet> GetEnumerationFacets(this XmlSchemaSimpleType simpleType)
        {
            if (simpleType.Content is XmlSchemaSimpleTypeRestriction restriction
                && restriction.Facets?.Count > 0)
            {
                foreach (var facet in restriction.Facets)
                {
                    if (facet is XmlSchemaEnumerationFacet item)
                    {
                        yield return item;
                    }
                }
            }

            yield break;
        }

        public static bool IsEnumeration(this XmlSchemaSimpleType simpleType)
        {
            return GetEnumerationFacets(simpleType).Any();
        }

        public static string GetFirstParentWithName(this XmlSchemaObject schemaObject)
        {
            while (schemaObject.Parent is { } parent)
            {
                if (parent is XmlSchemaType schemaType
                    && !string.IsNullOrEmpty(schemaType.Name))
                {
                    return schemaType.Name;
                }

                schemaObject = parent;
            }

            return null;
        }
    }
}
