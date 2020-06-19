using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using DynamicCode.SourceGenerator.Metadata.Interfaces;

namespace DynamicCode.SourceGenerator.Metadata.Roslyn
{
    public class RoslynMethodMetadata : IMethodMetadata
    {
        private readonly IMethodSymbol symbol;

        public RoslynMethodMetadata(IMethodSymbol symbol)
        {
            this.symbol = symbol;
        }

        public string DocComment => symbol.GetDocumentationCommentXml();
        public string Name => symbol.Name;
        public string FullName => symbol.GetFullName();
        public IEnumerable<IAttributeMetadata> Attributes => RoslynAttributeMetadata.FromAttributeData(symbol.GetAttributes());
        public ITypeMetadata Type => RoslynTypeMetadata.FromTypeSymbol(symbol.ReturnType);
        public bool IsAbstract => symbol.IsAbstract;
        public bool IsGeneric => symbol.IsGenericMethod;
        public bool IsPublic => symbol.DeclaredAccessibility == Accessibility.Public;
        public bool IsPrivate => symbol.DeclaredAccessibility == Accessibility.Private;
        public bool IsProtected => symbol.DeclaredAccessibility == Accessibility.Protected;
        public IEnumerable<ITypeParameterMetadata> TypeParameters => RoslynTypeParameterMetadata.FromTypeParameterSymbols(symbol.TypeParameters);
        public IEnumerable<IParameterMetadata> Parameters => RoslynParameterMetadata.FromParameterSymbols(symbol.Parameters);


        public static IEnumerable<IMethodMetadata> FromMethodSymbols(IEnumerable<IMethodSymbol> symbols)
        {
            return symbols.Where(s => s.MethodKind == MethodKind.Ordinary).Select(p => new RoslynMethodMetadata(p));
        }
    }
}