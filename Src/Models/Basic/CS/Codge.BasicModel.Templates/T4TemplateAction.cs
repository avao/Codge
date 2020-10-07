using Codge.Models.Common;

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
