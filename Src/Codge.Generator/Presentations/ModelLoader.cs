using Codge.DataModel.Descriptors;
using Common.Logging;
using Qart.Core.Io;
using Qart.Core.Xml;
using Qart.Core.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.Generator.Presentations
{
    public class ModelLoader
    {
        private ILog _logger;
        public ModelLoader(ILog logger)
        {
            _logger = logger;
        }

        public ModelDescriptor LoadModel(string path, string modelName)
        {
            _logger.InfoFormat("Loading model [{0}]", path);

            ModelDescriptor model = null;
            if (path.ToLower().EndsWith(".xsd"))
            {//loader type selection
                var schema = SchemaLoader.Load(path);
                model = Codge.Generator.Presentations.Xsd.ModelLoader.Load(schema, modelName);
            }
            else
            {
                XmlReaderUtils.UsingXmlReader(path, reader => model = Codge.Generator.Presentations.Xml.ModelLoader.Load(reader));
            }

            return model;
        }
    }
}
