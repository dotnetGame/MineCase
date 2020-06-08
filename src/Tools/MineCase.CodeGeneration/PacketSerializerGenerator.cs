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

            var codeWriter = new StringBuilder();

            var compilation = context.Compilation;
            var clsAttributeSymbol = compilation.GetTypeByMetadataName("MineCase.Serialization.GenerateSerializerAttribute");
            var fieldAttributeSymbol = compilation.GetTypeByMetadataName("MineCase.Serialization.SerializeAsAttribute");

            foreach (var cls in receiver.CandidateClasses)
            {
                var model = compilation.GetSemanticModel(cls.SyntaxTree);
                var clsSymbol = model.GetDeclaredSymbol(cls) as ITypeSymbol;
                var clsAttribute = clsSymbol.GetAttributes().FirstOrDefault(x => x.AttributeClass.Equals(clsAttributeSymbol, SymbolEqualityComparer.Default));
                if (clsAttribute != null)
                {
                    var methods = clsAttribute.NamedArguments.FirstOrDefault(x => x.Key == "Methods").Value.Value;
                    var mdValue = methods == null ? GenerateSerializerMethods.Both : (GenerateSerializerMethods)(int)methods;

                    ProcessClass(clsSymbol, mdValue, codeWriter, compilation, fieldAttributeSymbol);
                }
            }

            var source = SourceText.From(
                @"using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

" + codeWriter.ToString(), Encoding.UTF8);

            File.WriteAllText("obj/packet.g.cs", source.ToString());
            context.AddSource("packet.g.cs", source);
        }

        private void ProcessClass(ITypeSymbol cls, GenerateSerializerMethods mdValue, StringBuilder codeWriter, Compilation compilation, INamedTypeSymbol fieldAttributeSymbol)
        {
            codeWriter.AppendLine($"namespace {cls.ContainingNamespace.ToDisplayString()}");
            codeWriter.AppendLine("{");
            codeWriter.Ident(1).AppendLine($"public partial class {cls.Name}");
            codeWriter.Ident(1).AppendLine("{");

            try
            {
                var fields = from fSymbol in cls.GetMembers().OfType<IFieldSymbol>()
                             let attr = fSymbol.GetAttributes().FirstOrDefault(x => x.AttributeClass.Equals(fieldAttributeSymbol, SymbolEqualityComparer.Default))
                             where attr != null
                             let dataType = (DataType)(int)attr.ConstructorArguments[0].Value
                             select new
                             {
                                 Symbol = fSymbol,
                                 DataType = dataType
                             };

                // 1. Serializer
                if (mdValue == GenerateSerializerMethods.Serialize || mdValue == GenerateSerializerMethods.Both)
                {
                    codeWriter.Ident(2).AppendLine("public void Serialize(BinaryWriter bw)");
                    codeWriter.Ident(2).AppendLine("{");

                    foreach (var field in fields)
                    {
                        var code = field.DataType switch
                        {
                            DataType.Angle => $"WriteAsAngle({field.Symbol.Name})",
                            DataType.Boolean => $"WriteAsBoolean({field.Symbol.Name})",
                            DataType.Byte => $"WriteAsByte({field.Symbol.Name})",
                            DataType.ByteArray => $"WriteAsByteArray({field.Symbol.Name})",
                            DataType.Chat => $"WriteAsChat({field.Symbol.Name})",
                            DataType.Double => $"WriteAsDouble({field.Symbol.Name})",
                            DataType.EntityMetadata => throw new NotSupportedException(),
                            DataType.Float => $"WriteAsFloat({field.Symbol.Name})",
                            DataType.Int => $"WriteAsInt({field.Symbol.Name})",
                            DataType.IntArray => $"WriteAsIntArray({field.Symbol.Name})",
                            DataType.Long => $"WriteAsLong({field.Symbol.Name})",
                            DataType.NbtArray => $"WriteAsNbtArray({field.Symbol.Name})",
                            DataType.NBTTag => $"WriteAsNBTTag({field.Symbol.Name})",
                            DataType.Position => $"WriteAsPosition({field.Symbol.Name})",
                            DataType.Short => $"WriteAsShort({field.Symbol.Name})",
                            DataType.Slot => $"WriteAsSlot({field.Symbol.Name})",
                            DataType.String => $"WriteAsString({field.Symbol.Name})",
                            DataType.UnsignedByte => $"WriteAsUnsignedByte({field.Symbol.Name})",
                            DataType.UnsignedShort => $"WriteAsUnsignedShort({field.Symbol.Name})",
                            DataType.UUID => $"WriteAsUUID({field.Symbol.Name})",
                            DataType.VarInt => $"WriteAsVarInt({field.Symbol.Name}, out _)",
                            DataType.VarLong => $"WriteAsVarLong({field.Symbol.Name}, out _)",
                            _ => ""
                        };

                        codeWriter.Ident(3).AppendLine($"bw.{code};");
                    }

                    codeWriter.Ident(2).AppendLine("}");
                }
            }
            catch (Exception ex)
            {
                codeWriter.AppendLine(ex.ToString());
            }

            codeWriter.Ident(1).AppendLine("}");
            codeWriter.AppendLine("}");
            codeWriter.AppendLine();
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

        private enum GenerateSerializerMethods
        {
            Serialize = 0,
            Deserialize = 1,
            Both = 2
        }

        private enum DataType
        {
            Boolean,
            Byte,
            UnsignedByte,
            Short,
            UnsignedShort,
            Int,
            Long,
            Float,
            Double,
            String,
            Chat,
            VarInt,
            VarLong,
            EntityMetadata,
            Slot,
            NBTTag,
            Position,
            Angle,
            UUID,
            ByteArray,
            IntArray,
            NbtArray,
            Array
        }
    }

    static class CodeWriterExtensions
    {
        public static StringBuilder Ident(this StringBuilder sb, int count)
        {
            for (int i = 0; i < count; i++)
                sb.Append("    ");
            return sb;
        }
    }
}
