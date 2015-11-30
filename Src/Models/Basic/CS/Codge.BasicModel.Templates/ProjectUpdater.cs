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
                var builder = new StringBuilder(content.Substring(0, begin_pos));
                builder.AppendLine(start);

                foreach(var file in context.Tracker.FilesSkipped.Concat(context.Tracker.FilesUpdated).OrderBy(_ => _))
                {
                    if(string.Compare(Path.GetExtension(file), ".cs", true)==0)
                    {
                        builder.AppendLine(string.Format("    <Compile Include=\"{0}\" />", file));
                    }
                }
                
                //builder.AppendLine("<EmbeddedResource Include=\"pof.config\" />");

                builder.Append(end);
                builder.Append(content.Substring(end_pos + end.Length));
                content = builder.ToString();
            }

            return new PathAndContent(new ItemInformation(path, string.Empty), content);
        }

        public bool IsApplicable()
        {
            return true;
        }
    }
}
