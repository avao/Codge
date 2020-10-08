using Codge.DataModel.Descriptors;
using Microsoft.Extensions.Logging;
using Qart.Core.Xml;
using Qart.Core.Xsd;

namespace Codge.Generator.Presentations
{
    public class ModelLoader
    {
        private ILogger _logger;
        public ModelLoader(ILogger logger)
        {
            _logger = logger;
        }

        public ModelDescriptor LoadModel(string path, string modelName)
        {
            _logger.LogInformation("Loading model [{path}]", path);

            ModelDescriptor model = null;
            if (path.ToLower().EndsWith(".xsd"))
            {//loader type selection
                var schema = SchemaLoader.Load(path);
                model = Xsd.ModelLoader.Load(schema, modelName);
            }
            else
            {
                XmlReaderUtils.UsingXmlReader(path, reader => model = Xml.ModelLoader.Load(reader));
            }

            return model;
        }
    }
}
