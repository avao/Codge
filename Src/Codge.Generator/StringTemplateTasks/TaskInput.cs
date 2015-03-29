using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.Generator.StringTemplateTasks
{
    public class TaskInput
    {
        public Antlr4.StringTemplate.Template OutputPath { get; private set; }

        public Antlr4.StringTemplate.Template Content { get; private set; }

        public Antlr4.StringTemplate.Template IsApplicable { get; private set; }

        public TaskInput(Antlr4.StringTemplate.Template relativePathTemplate, Antlr4.StringTemplate.Template isApplicable, Antlr4.StringTemplate.Template content)
        {
            OutputPath = relativePathTemplate;
            Content = content;
            IsApplicable = isApplicable;
        }

    }

}
