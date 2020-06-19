using DynamicCode.SourceGenerator.Metadata.Interfaces;
using DynamicCode.SourceGenerator.Metadata.Roslyn;
using DynamicCode.SourceGenerator.Models.Config;
using DynamicCode.SourceGenerator.Models.CodeGeneration.Implementation;

namespace DynamicCode.SourceGenerator.Models.Rendering
{
    public class RenderModel : Object
    {
        public static Object FromNamedItem(CodeGenerationConfigBuilder builder, INamedItem namedItem)
        {
            Object returnItem = null;
            if (namedItem is RoslynClassMetadata classMetadata && ClassImpl.FromMetadata(classMetadata, null) is Class @class)
            {
                returnItem = @class;
            }
            if (namedItem is RoslynEnumMetadata enumMetadata && EnumImpl.FromMetadata(enumMetadata, null) is Enum @enum)
            {
                returnItem = @enum;
            }
            if (namedItem is RoslynInterfaceMetadata interfaceMetadata && InterfaceImpl.FromMetadata(interfaceMetadata, null) is Interface @interface)
            {
                returnItem = @interface;
            }
            if (returnItem != null)
            {
                returnItem.Template = builder.Template;
            }
            return returnItem;
        }
    }
}
