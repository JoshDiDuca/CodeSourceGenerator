using DynamicCode.SourceGenerator.Models;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DynamicCode.SourceGenerator.Visitors
{
    class SourceFileSymbolVisitor : SymbolVisitor
    {
        public List<BaseObject> Objects { get; set; } = new List<BaseObject>();

        public SourceFileSymbolVisitor() : base()
        {
        }



        public override void VisitNamespace(INamespaceSymbol symbol)
        {
            Parallel.ForEach(symbol.GetMembers(), s => s.Accept(this));
        }

        public override void VisitNamedType(INamedTypeSymbol symbol)
        {
            var newObject = BaseObject.FromNamedType(symbol);
            if (newObject != null)
            {
                Objects.Add(newObject);
            }
            foreach (var childSymbol in symbol.GetTypeMembers())
            {
                base.Visit(childSymbol);
            }
        }

        public List<BaseObject> QueryObjects(string query, List<string> assemblies)
        {
            if (Objects == null || string.IsNullOrEmpty(query))
                return null;
            return Objects.Where(e => 
                    !string.IsNullOrEmpty(e?.Name) && 
                    assemblies.Contains(e.NamedTypeSymbol?.ContainingAssembly?.Name) && 
                    Regex.IsMatch(e.Name, query)
                  ).ToList();
        }
    }
}
