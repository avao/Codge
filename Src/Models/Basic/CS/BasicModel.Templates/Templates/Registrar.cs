﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 12.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Codge.BasicModel.Templates.CS.Templates
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using Codge.DataModel;
    using Codge.Generator;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Work\Projects\codge\Src\Models\Basic\CS\BasicModel.Templates\Templates\Registrar.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public partial class Registrar : T4TemplateAction<Model>
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("using Codge.BasicModel.CS.Serialisation;\r\n\r\nnamespace Serialisers.");
            
            #line 11 "C:\Work\Projects\codge\Src\Models\Basic\CS\BasicModel.Templates\Templates\Registrar.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.Namespace.GetFullName(".")));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n\r\n\tpublic static class Registrar\r\n\t{\r\n\t\tpublic static void RegisterSerialise" +
                    "rs(SerialisationContext context)\r\n\t\t{\r\n");
            
            #line 18 "C:\Work\Projects\codge\Src\Models\Basic\CS\BasicModel.Templates\Templates\Registrar.tt"

					foreach(var type in Model.Namespace.AllTypes().Where(t => t is CompositeType))
					{

            
            #line default
            #line hidden
            this.Write("\t\t\tcontext.RegisterSerialiser<");
            
            #line 22 "C:\Work\Projects\codge\Src\Models\Basic\CS\BasicModel.Templates\Templates\Registrar.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(type.QName()));
            
            #line default
            #line hidden
            this.Write(">(new ");
            
            #line 22 "C:\Work\Projects\codge\Src\Models\Basic\CS\BasicModel.Templates\Templates\Registrar.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(type.SerialiserQName()));
            
            #line default
            #line hidden
            this.Write("());\r\n");
            
            #line 23 "C:\Work\Projects\codge\Src\Models\Basic\CS\BasicModel.Templates\Templates\Registrar.tt"
		
					}

            
            #line default
            #line hidden
            this.Write("\t\t}\r\n\t}\r\n}\r\n\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 30 "C:\Work\Projects\codge\Src\Models\Basic\CS\BasicModel.Templates\Templates\Registrar.tt"

	public Model Model{get; private set;}

	public Registrar(Model model)
	{
		Model = model;
	}

	public override bool IsApplicable()
	{
		return true;
	}

	public override PathAndContent Execute(Context context)
	{
		return new PathAndContent("Registrar.cs", TransformText());
	}

        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
}
