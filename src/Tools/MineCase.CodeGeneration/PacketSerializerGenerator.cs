using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace MineCase.CodeGeneration
{
    [Generator]
    public class PacketSerializerGenerator : ISourceGenerator
    {
        public void Execute(SourceGeneratorContext context)
        {
            if (!(context.SyntaxReceiver is SyntaxReceiver receiver))
                return;

            var compilation = context.Compilation;
            var clsAttributeSymbol = compilation.GetTypeByMetadataName("MineCase.Serialization.GenerateSerializerAttribute");

            foreach (var cls in receiver.CandidateClasses)
            {
                var model = compilation.GetSemanticModel(cls.SyntaxTree);
                var clsSymbol = model.GetDeclaredSymbol(cls) as ITypeSymbol;
                var clsAttribute = clsSymbol.GetAttributes().FirstOrDefault(x => x.AttributeClass.Equals(clsAttributeSymbol, SymbolEqualityComparer.Default));
                if (clsAttribute != null)
                {
                }
            }

            var source = SourceText.From(
                $"namespace MineCase {{  }}", Encoding.UTF8);
            File.WriteAllText("obj/packet.g.cs", source.ToString());
            context.AddSource("packet.g.cs", source);
        }

        public void Initialize(InitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        private class SyntaxReceiver : ISyntaxReceiver
        {
            public List<ClassDeclarationSyntax> CandidateClasses { get; } = new List<ClassDeclarationSyntax>();

            public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
            {
                if (syntaxNode is ClassDeclarationSyntax classDeclarationSyntax
                    && classDeclarationSyntax.AttributeLists.Count > 0)
                {
                    CandidateClasses.Add(classDeclarationSyntax);
                }
            }
        }
    }
}
