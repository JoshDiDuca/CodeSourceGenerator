﻿using DynamicCode.SourceGenerator.Models;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DynamicCode.SourceGenerator.Metadata.Interfaces;
using DynamicCode.SourceGenerator.Metadata.Roslyn;

namespace DynamicCode.SourceGenerator.Visitors
{
    class SourceFileSymbolVisitor : SymbolVisitor
    {
        public ConcurrentBag<INamedItem> Objects { get; set; } = new ConcurrentBag<INamedItem>();

        public SourceFileSymbolVisitor() : base()
        {
        }

        public override void Visit(ISymbol symbol)
        {
            base.Visit(symbol);
        }


        public override void VisitNamespace(INamespaceSymbol symbol)
        {
            Parallel.ForEach(symbol.GetMembers(), s => s.Accept(this));
        }

        public override void VisitNamedType(INamedTypeSymbol symbol)
        {
            INamedItem namedItem = null;
            namedItem = (symbol.TypeKind switch
            {
                TypeKind.Class => new RoslynClassMetadata(symbol),
                TypeKind.Enum => new RoslynEnumMetadata(symbol),
                TypeKind.Interface => new RoslynInterfaceMetadata(symbol),
                _ => null
            });
            if (namedItem != null)
            {
                Objects.Add(namedItem);
            }
            foreach (var childSymbol in symbol.GetTypeMembers())
            {
                base.Visit(childSymbol);
            }
        }

        public List<INamedItem> QueryObjects(string query, List<string> assemblies)
        {
            if (Objects == null || string.IsNullOrEmpty(query))
                return null;
            return Objects.Where(e => 
                    !string.IsNullOrEmpty(e?.Name) && 
                    assemblies.Any(a => e.FullName.Contains(a)) && 
                    Regex.IsMatch(e.Name, query)
                  ).ToList();
        }
    }
}