using Codge.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.BasicModel.Templates.CS
{
    public abstract class T4TemplateAction<T> : T4TemplateActionBase<T>
    {
        public ModelBehaviour ModelBehaviour;

        public T4TemplateAction(ModelBehaviour modelBehaviour)
        {
            ModelBehaviour = modelBehaviour;
        }
    }
}
