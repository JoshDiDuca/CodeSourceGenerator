using DynamicCode.SourceGenerator.Metadata.Interfaces;
using DynamicCode.SourceGenerator.Metadata.Roslyn;
using DynamicCode.SourceGenerator.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DynamicCode.SourceGenerator.Models.Rendering
{
    public class RenderModel
    {
        public string Template { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Namespace { get; set; }
        public string DocComment { get; set; }
        public INamedItem O { get; set; }

        public List<IClassMetadata> NestedClasses { get; set; }
        public List<IInterfaceMetadata> NestedInterfaces { get; set; }
        public List<IEnumMetadata> NestedEnums { get; set; }
        public List<IDelegateMetadata> Delegates { get; set; }
        public List<IConstantMetadata> Constants { get; set; }
        public List<IEventMetadata> Events { get; set; }
        public List<IFieldMetadata> Fields { get; set; }
        public List<IInterfaceMetadata> Interfaces { get; set; }
        public List<IMethodMetadata> Methods { get; set; }
        public List<IPropertyMetadata> Properties { get; set; }
        public IClassMetadata ContainingClass { get; set; }
        public IClassMetadata BaseClass { get; set; }
        public ITypeMetadata Type { get; set; }
        public List<IAttributeMetadata> Attributes { get; set; }
        public List<IEnumValueMetadata> Values { get; set; }
        public List<ITypeParameterMetadata> TypeParameters { get; set; }
        public List<ITypeMetadata> TypeArguments { get; set; }
        public bool IsGeneric { get; set; }


        public static RenderModel FromNamedItem(CodeGenerationConfigBuilder builder, INamedItem namedItem)
        {
            var renderModel = new RenderModel { 
                O = namedItem,
                FullName = namedItem.FullName,
                Name = namedItem.Name,
                Template = builder.Template
            };

            if (namedItem is RoslynClassMetadata @class)
            {
                renderModel.NestedClasses = @class.NestedClasses.ToList();
                renderModel.NestedInterfaces = @class.NestedInterfaces.ToList();
                renderModel.NestedEnums = @class.NestedEnums.ToList();
                renderModel.Delegates = @class.Delegates.ToList();
                renderModel.Constants = @class.Constants.ToList();
                renderModel.Events = @class.Events.ToList();
                renderModel.Fields = @class.Fields.ToList();
                renderModel.Interfaces = @class.Interfaces.ToList();
                renderModel.Methods = @class.Methods.ToList();
                renderModel.Properties = @class.Properties.ToList();
                renderModel.Attributes = @class.Attributes.ToList();
                renderModel.ContainingClass = @class.ContainingClass;
                renderModel.BaseClass = @class.BaseClass;
                renderModel.Namespace = @class.Namespace;
                renderModel.Type = @class.Type;
                renderModel.IsGeneric = @class.IsGeneric;
                renderModel.DocComment = @class.DocComment;
                renderModel.TypeParameters = @class.TypeParameters.ToList();
                renderModel.TypeArguments = @class.TypeArguments.ToList();
            }
            if (namedItem is RoslynEnumMetadata @enum)
            {
                renderModel.Attributes = @enum.Attributes.ToList();
                renderModel.Values = @enum.Values.ToList();

                renderModel.ContainingClass = @enum.ContainingClass;
                renderModel.Namespace = @enum.Namespace;
                renderModel.Type = @enum.Type;
                renderModel.DocComment = @enum.DocComment;
            }
            if (namedItem is RoslynInterfaceMetadata @interface)
            {
                renderModel.Events = @interface.Events.ToList();
                renderModel.Interfaces = @interface.Interfaces.ToList();
                renderModel.Methods = @interface.Methods.ToList();
                renderModel.Properties = @interface.Properties.ToList();
                renderModel.Attributes = @interface.Attributes.ToList();
                renderModel.ContainingClass = @interface.ContainingClass;
                renderModel.Namespace = @interface.Namespace;
                renderModel.Type = @interface.Type;
                renderModel.IsGeneric = @interface.IsGeneric;
                renderModel.DocComment = @interface.DocComment;
                renderModel.TypeParameters = @interface.TypeParameters.ToList();
                renderModel.TypeArguments = @interface.TypeArguments.ToList();
            }

            return renderModel;
        }
    }
}
