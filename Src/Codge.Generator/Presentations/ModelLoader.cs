using Codge.DataModel.Descriptors;
using Microsoft.Extensions.Logging;
using Qart.Core.Xml;
using Qart.Core.Xsd;
using System.Collections.Generic;
using System.Linq;

namespace Codge.Generator.Presentations
{
    public class ModelLoader
    {
        private ILogger _logger;
        public ModelLoader(ILogger logger)
        {
            _logger = logger;
        }

        public ModelDescriptor LoadModel(IReadOnlyCollection<string> paths, string modelName)
        {
            _logger.LogInformation("Loading model [{0}]", string.Join(", ", paths));

            var firstPath = paths.First();
            ModelDescriptor model = null;
            if (firstPath.ToLower().EndsWith(".xsd"))
            {//loader type selection
                var schemas = paths.Select(SchemaLoader.Load).ToList();
                model = Xsd.ModelLoader.Load(schemas, modelName);
            }
            else
            {
                XmlReaderUtils.UsingXmlReader(firstPath, reader => model = Xml.ModelLoader.Load(reader));
            }

            return model;
        }
    }
}
