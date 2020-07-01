using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using CodeSourceGenerator.Metadata.Interfaces;

namespace CodeSourceGenerator.Metadata.Roslyn
{
    public class RoslynPropertyMetadata : IPropertyMetadata
    {
        private readonly IPropertySymbol symbol;

        private RoslynPropertyMetadata(IPropertySymbol symbol)
        {
            this.symbol = symbol;
        }

        public string DocComment => symbol.GetDocumentationCommentXml();
        public string Name => symbol.Name;
        public string FullName => symbol.ToDisplayString();
        public IEnumerable<IAttributeMetadata> Attributes => RoslynAttributeMetadata.FromAttributeData(symbol.GetAttributes());
        public ITypeMetadata Type => RoslynTypeMetadata.FromTypeSymbol(symbol.Type);
        public bool IsAbstract => symbol.IsAbstract;
        public bool HasGetter => symbol.GetMethod != null;
        public bool HasSetter => symbol.SetMethod != null;
        public bool IsPublic => symbol.DeclaredAccessibility == Accessibility.Public;
        public bool IsPrivate => symbol.DeclaredAccessibility == Accessibility.Private;
        public bool IsProtected => symbol.DeclaredAccessibility == Accessibility.Protected;

        public static IEnumerable<IPropertyMetadata> FromPropertySymbol(IEnumerable<IPropertySymbol> symbols)
        {
            return symbols.Select(p => new RoslynPropertyMetadata(p));
        }
    }
}