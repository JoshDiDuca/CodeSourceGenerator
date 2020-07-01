using CodeSourceGenerator.Metadata.Interfaces;
using CodeSourceGenerator.Metadata.Roslyn;
using CodeSourceGenerator.Models.Config;
using CodeSourceGenerator.Models.CodeGeneration.Implementation;
using CodeSourceGenerator.Models.RenderModels;

namespace CodeSourceGenerator.Models.Rendering
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
                returnItem.Template = builder.Input.Template;
            }
            return returnItem;
        }
    }
}
