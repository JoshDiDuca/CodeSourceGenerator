using System;
using System.Collections.Generic;
using System.Linq;
using CodeSourceGenerator.Models.Config;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using CodeSourceGenerator.Metadata.Interfaces;

namespace CodeSourceGenerator.Metadata.Roslyn
{
    public class RoslynFileMetadata : IFileMetadata
    {
        private Document _document;
        private SyntaxNode _root;
        private SemanticModel _semanticModel;

        public RoslynFileMetadata(Document document)
        {
            LoadDocument(document);
        }

        public string Name => _document.Name;
        public string FullName => _document.FilePath;

        public IEnumerable<IClassMetadata> Classes => RoslynClassMetadata.FromNamedTypeSymbols(GetNamespaceChildNodes<ClassDeclarationSyntax>(), this);
        public IEnumerable<IDelegateMetadata> Delegates => RoslynDelegateMetadata.FromNamedTypeSymbols(GetNamespaceChildNodes<DelegateDeclarationSyntax>(), this);
        public IEnumerable<IEnumMetadata> Enums => RoslynEnumMetadata.FromNamedTypeSymbols(GetNamespaceChildNodes<EnumDeclarationSyntax>(), this);
        public IEnumerable<IInterfaceMetadata> Interfaces => RoslynInterfaceMetadata.FromNamedTypeSymbols(GetNamespaceChildNodes<InterfaceDeclarationSyntax>(), this);

        private void LoadDocument(Document document)
        {
            _document = document;
            _semanticModel = document.GetSemanticModelAsync().Result;
            _root = _semanticModel.SyntaxTree.GetRoot();
        }

        private IEnumerable<INamedTypeSymbol> GetNamespaceChildNodes<T>() where T : SyntaxNode
        {
            IEnumerable<INamedTypeSymbol> symbols = _root.ChildNodes().OfType<T>().Concat(
                _root.ChildNodes().OfType<NamespaceDeclarationSyntax>().SelectMany(n => n.ChildNodes().OfType<T>()))
                .Select(c => _semanticModel.GetDeclaredSymbol(c) as INamedTypeSymbol);
            return symbols;
        }
    }
}
