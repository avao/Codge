using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codge.DataModel;
using Codge.Generator.StringTemplateTasks;

namespace Codge.Generator
{
    public class Config
    {
        public string BaseDir { get; private set; }

        private IList<ITask<Model>> _modelTasks;
        public IEnumerable<ITask<Model>> ModelTasks { get { return _modelTasks; } }

        private IList<ITask<Namespace>> _namespaceTasks;
        public IEnumerable<ITask<Namespace>> NamespaceTasks { get { return _namespaceTasks; } }

        private IList<ITask<TypeBase>> _typeTasks;
        public IEnumerable<ITask<TypeBase>> TypeTasks { get { return _typeTasks; } }

        public Config(string baseDir)
        {
            BaseDir = Path.GetFullPath(baseDir);
            _modelTasks = new List<ITask<Model>>();
            _namespaceTasks = new List<ITask<Namespace>>();
            _typeTasks = new List<ITask<TypeBase>>();
        }

        public void AddModelTask(ITask<Model> task)
        {
            _modelTasks.Add(task);
        }


        public void AddNamespaceTask(ITask<Namespace> task)
        {
            _namespaceTasks.Add(task);
        }


        public void AddTypeTask(ITask<TypeBase> task)
        {
            _typeTasks.Add(task);
        }
    }
}
