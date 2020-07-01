using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using CodeSourceGenerator.Metadata.Interfaces;

namespace CodeSourceGenerator.Metadata.Roslyn
{
    public class RoslynDelegateMetadata : IDelegateMetadata
    {
        private readonly INamedTypeSymbol _symbol;
        private readonly IMethodSymbol _methodSymbol;
        private readonly IFileMetadata _file;

        public RoslynDelegateMetadata(INamedTypeSymbol symbol, IFileMetadata file = null)
        {
            _symbol = symbol;
            _methodSymbol = symbol.DelegateInvokeMethod;
            _file = file;
        }

        public string DocComment => _symbol.GetDocumentationCommentXml();
        public string Name => _symbol.Name;
        public string FullName => _symbol.GetFullName();
        public IEnumerable<IAttributeMetadata> Attributes => RoslynAttributeMetadata.FromAttributeData(_symbol.GetAttributes());
        public ITypeMetadata Type => _methodSymbol == null ? null : RoslynTypeMetadata.FromTypeSymbol(_methodSymbol.ReturnType);
        public bool IsPublic => _symbol.DeclaredAccessibility == Accessibility.Public;
        public bool IsPrivate => _symbol.DeclaredAccessibility == Accessibility.Private;
        public bool IsProtected => _symbol.DeclaredAccessibility == Accessibility.Protected;
        public bool IsAbstract => false;
        public bool IsGeneric => _symbol.TypeParameters.Any();
        public IEnumerable<ITypeParameterMetadata> TypeParameters => RoslynTypeParameterMetadata.FromTypeParameterSymbols(_symbol.TypeParameters);
        public IEnumerable<IParameterMetadata> Parameters => _methodSymbol == null ? new IParameterMetadata[0] : RoslynParameterMetadata.FromParameterSymbols(_methodSymbol.Parameters);

        public static IEnumerable<IDelegateMetadata> FromNamedTypeSymbols(IEnumerable<INamedTypeSymbol> symbols, RoslynFileMetadata file = null)
        {
            return symbols.Select(s => new RoslynDelegateMetadata(s, file));
        }
    }
}