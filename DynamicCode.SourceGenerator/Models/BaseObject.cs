using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace DynamicCode.SourceGenerator.Models
{
    public partial class BaseObject
    {
        public string Name => NamedTypeSymbol?.Name ?? string.Empty;
        public TypeKind TypeKind => NamedTypeSymbol?.TypeKind ?? TypeKind.Unknown;
        
        public List<BaseObject> Members => GetMembers();

        public INamedTypeSymbol NamedTypeSymbol { get; set; }

        public BaseObject()
        {

        }

        public List<BaseObject> GetMembers()
        {
            List<BaseObject> members = new List<BaseObject>();
            if (NamedTypeSymbol != null) {
                foreach (var typeMember in NamedTypeSymbol.GetTypeMembers())
                {
                    var newObject = FromNamedType(typeMember);
                    if (newObject != null)
                    {
                        members.Add(newObject);
                    }
                } 
            }
            return members;
        }

        public static BaseObject FromNamedType(INamedTypeSymbol namedTypeSymbol)
        {
            if (namedTypeSymbol == null)
                return null;

            BaseObject newObject = null;
            switch (namedTypeSymbol.TypeKind)
            {
                case TypeKind.Array:
                    newObject = new ArrayObject();
                    break;
                case TypeKind.Class:
                    newObject = new ClassObject();
                    break;
                case TypeKind.Delegate:
                    newObject = new DelegateObject();
                    break;
                case TypeKind.Dynamic:
                    newObject = new DynamicObject();
                    break;
                case TypeKind.Enum:
                    newObject = new EnumObject();
                    break;
                case TypeKind.Interface:
                    newObject = new InterfaceObject();
                    break;
                case TypeKind.Unknown:
                case TypeKind.TypeParameter:
                default:
                    newObject = new UnknownObject();
                    break;
            }
            if (newObject != null)
            {
                newObject.NamedTypeSymbol = namedTypeSymbol;
            }
            return newObject;
        }

    }
}