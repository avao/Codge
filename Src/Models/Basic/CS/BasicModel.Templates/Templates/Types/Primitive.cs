﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 11.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace BasicModel.Templates.CS.Templates.Types
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
    
    #line 1 "D:\work\CodeGen\Models\Basic\CS\BasicModel.Templates\Templates\Types\Primitive.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "11.0.0.0")]
    public partial class Primitive : T4TemplateAction<TypeBase>
    {
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("\r\n\r\nnamespace Types.");
            
            #line 11 "D:\work\CodeGen\Models\Basic\CS\BasicModel.Templates\Templates\Types\Primitive.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Type.Namespace.GetFullName(".")));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n\tpublic class ");
            
            #line 13 "D:\work\CodeGen\Models\Basic\CS\BasicModel.Templates\Templates\Types\Primitive.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Type.Name));
            
            #line default
            #line hidden
            this.Write("\r\n\t{\r\n\r\n\t\r\n\t}\r\n}\r\n\r\n\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 21 "D:\work\CodeGen\Models\Basic\CS\BasicModel.Templates\Templates\Types\Primitive.tt"

	public PrimitiveType Type{get; private set;}

	public Primitive(TypeBase type)
	{
		Type = type as PrimitiveType;
	}

	public override bool IsApplicable()
	{
		return Type!=null;
	}

	public override PathAndContent Execute(Context context)
	{
		return new PathAndContent(Utils.GetOutputPath(Type, "Types", "cs"), TransformText());
	}

        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
}
