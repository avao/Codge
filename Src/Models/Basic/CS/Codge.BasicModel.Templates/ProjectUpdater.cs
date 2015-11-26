using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codge.DataModel;
using Codge.Generator;

namespace Codge.BasicModel.Templates.CS
{
    public class ProjectUpdater : IOutputAction<Model>
    {
        public Model Model { get; private set; }

        public ProjectUpdater(Model model)
        {
            Model = model;
        }

        public PathAndContent Execute(Context context)
        {
            //TODO if file does not exist - create one from template
            string path = Directory.EnumerateFiles(context.BaseDir, "*.csproj", SearchOption.TopDirectoryOnly).First();

            string content = File.ReadAllText(path);

            string start = "<!-- GENERATED_ENTRIES_BEGIN -->";
            string end = "<!-- GENERATED_ENTRIES_END -->";

            int begin_pos = content.IndexOf(start);
            int end_pos = content.IndexOf(end);

            if (begin_pos != -1 && end_pos != -1)
            {
                

                //TODO should updated files be passed in?
                var builder = new StringBuilder(content.Substring(0, begin_pos));
                builder.AppendLine(start);

                foreach(var type in Model.Namespace.AllTypes())
                {
                    builder.AppendLine(string.Format("    <Compile Include=\"{0}\" />", Utils.GetOutputPath(type, "Types", "cs")));

                    if(type.IsComposite())
                        builder.AppendLine(string.Format("    <Compile Include=\"{0}\" />", Utils.GetOutputPath(type, "Serialisers", "cs")));
                    
                }

                //builder.AppendLine("<EmbeddedResource Include=\"pof.config\" />");
                builder.AppendLine("<Compile Include=\"Registrar.cs\" />");

                builder.Append(end);
                builder.Append(content.Substring(end_pos + end.Length));
                content = builder.ToString();
            }

            return new PathAndContent(new ItemInformation(path, ".csproj"), content);
        }

        public bool IsApplicable()
        {
            return true;
        }
    }
}
