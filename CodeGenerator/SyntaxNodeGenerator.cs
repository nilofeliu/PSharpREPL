using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CodeGenerator
{
    public class SyntaxNodeGenerator
    {
        private class SyntaxNode
        {
            public string Name { get; set; }
            public string Base { get; set; }
            public string Kind { get; set; }
            public string Category { get; set; }
            public string OperatorKind { get; set; }
            public List<string> Interfaces { get; set; } = new();
            public List<Property> Properties { get; set; } = new();
            public List<ComputedProperty> ComputedProperties { get; set; } = new();
        }

        private class Property
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public bool IsNullable { get; set; }
            public bool IsList => Type.EndsWith("List");
        }

        private class ComputedProperty
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public string Expression { get; set; }
        }

        public static void Run(string[] args)
        {
            string inputDir = args.Length > 0 ? args[0] : "Source";
            string outputDir = args.Length > 1 ? args[1] : "Generated";

            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            var allNodes = new List<SyntaxNode>();
            var xmlFiles = Directory.GetFiles(inputDir, "*.xml");

            foreach (var xmlFile in xmlFiles)
            {
                try
                {
                    Console.WriteLine($"Processing: {xmlFile}");
                    XDocument doc = XDocument.Load(xmlFile);
                    var nodes = doc.Root.Elements("SyntaxNode")
                        .Select(ParseSyntaxNode)
                        .ToList();

                    foreach (var node in nodes)
                    {
                        GenerateGreenClass(node, outputDir);
                        GenerateRedClass(node, outputDir);
                    }

                    allNodes.AddRange(nodes);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR in {xmlFile}: {ex.Message}");
                }
            }

            GenerateRedFactory(allNodes, outputDir);

            Console.WriteLine("Generation complete.");
        }

        private static void GenerateRedFactory(List<SyntaxNode> allNodes, string outputDir)
        {
            string filePath = Path.Combine(outputDir, "SyntaxFactory.cs");

            using var writer = new StreamWriter(filePath);
            writer.WriteLine("using Minsk.CodeAnalysis.Syntax.Kind;");
            writer.WriteLine("using Minsk.CodeAnalysis.Syntax.InternalSyntax;");
            writer.WriteLine("using Minsk.CodeAnalysis.Syntax.Nodes.Expressions;");
            writer.WriteLine("using Minsk.CodeAnalysis.Syntax.Nodes.Statements;");
            writer.WriteLine("using Minsk.CodeAnalysis.Syntax.Nodes.Declarations;");

            // Add green using directives per category
            foreach (var category in allNodes.Select(n => n.Category).Distinct())
            {
                writer.WriteLine($"using Minsk.CodeAnalysis.Syntax.Green.{category}s;");
            }

            writer.WriteLine("using System;");
            writer.WriteLine();
            writer.WriteLine("namespace Minsk.CodeAnalysis.Syntax");
            writer.WriteLine("{");
            writer.WriteLine("    internal static class SyntaxFactory");
            writer.WriteLine("    {");
            writer.WriteLine("        public static SyntaxNode CreateRed(GreenNode green, SyntaxNode? parent, int position)");
            writer.WriteLine("        {");
            writer.WriteLine("            return green.Kind switch");
            writer.WriteLine("            {");

            foreach (var node in allNodes)
            {
                string greenClassName = "Green" + StripSyntax(node.Name);
                writer.WriteLine($"                SyntaxKind.{node.Kind} => new {node.Name}(({greenClassName})green, parent, position),");
            }

            writer.WriteLine("                _ => throw new InvalidOperationException($\"Unknown SyntaxKind: {green.Kind}\")");
            writer.WriteLine("            };");
            writer.WriteLine("        }");
            writer.WriteLine("    }");
            writer.WriteLine("}");

            Console.WriteLine($"Generated: {filePath}");
        }

        private static SyntaxNode ParseSyntaxNode(XElement elem)
        {
            return new SyntaxNode
            {
                Name = elem.Attribute("Name").Value,
                Base = elem.Attribute("Base").Value,
                Kind = elem.Attribute("Kind").Value,
                Category = elem.Attribute("Category")?.Value ?? "Expression",
                Interfaces = (elem.Attribute("Interfaces")?.Value ?? "")
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(i => i.Trim())
                    .ToList(),
                Properties = elem.Elements("Property")
                    .Select(p => new Property
                    {
                        Name = p.Attribute("Name").Value,
                        Type = p.Attribute("Type").Value,
                        IsNullable = p.Attribute("Nullable")?.Value == "true"
                    }).ToList(),
                ComputedProperties = elem.Elements("ComputedProperty")
                    .Select(p => new ComputedProperty
                    {
                        Name = p.Attribute("Name").Value,
                        Type = p.Attribute("Type").Value,
                        Expression = p.Attribute("Expression").Value
                    }).ToList(),
                OperatorKind = elem.Attribute("OperatorKind")?.Value
            };
        }

        private static void GenerateGreenClass(SyntaxNode node, string outputDir)
        {
            string className = "Green" + StripSyntax(node.Name);
            string baseClass = "Green" + node.Base;
            string ns = $"Minsk.CodeAnalysis.Syntax.Green.{node.Category}s";
            string subDir = Path.Combine(outputDir, "Green", node.Category + "s");
            Directory.CreateDirectory(subDir);
            string filePath = Path.Combine(subDir, className + ".cs");
            string interfaceClause = node.Interfaces.Count > 0
                ? ", " + string.Join(", ", node.Interfaces)
                : "";

            using var writer = new StreamWriter(filePath);
            writer.WriteLine("using Minsk.CodeAnalysis.Syntax.Kind;");
            writer.WriteLine("using Minsk.CodeAnalysis.Syntax.InternalSyntax;");
            writer.WriteLine();
            writer.WriteLine($"namespace {ns}");
            writer.WriteLine("{");
            writer.WriteLine($"    internal sealed class {className} : {baseClass}{interfaceClause}");
            writer.WriteLine("    {");

            foreach (var prop in node.Properties)
            {
                string type = MapToGreenType(prop.Type) + (prop.IsNullable ? "?" : "");
                writer.WriteLine($"        public {type} {prop.Name} {{ get; }}");
            }
            writer.WriteLine();

            writer.WriteLine($"        public override int SlotCount => {node.Properties.Count};");
            writer.WriteLine();

            writer.WriteLine("        public override GreenNode? GetSlot(int index) => index switch");
            writer.WriteLine("        {");
            for (int i = 0; i < node.Properties.Count; i++)
                writer.WriteLine($"            {i} => {node.Properties[i].Name},");
            writer.WriteLine("            _ => null");
            writer.WriteLine("        };");
            writer.WriteLine();

            writer.WriteLine($"        public {className}(");
            for (int i = 0; i < node.Properties.Count; i++)
            {
                string type = MapToGreenType(node.Properties[i].Type) + (node.Properties[i].IsNullable ? "?" : "");
                string comma = i < node.Properties.Count - 1 ? "," : "";
                writer.WriteLine($"            {type} {LowerFirst(node.Properties[i].Name)}{comma}");
            }
            writer.WriteLine("        )");
            writer.WriteLine("        {");
            foreach (var prop in node.Properties)
                writer.WriteLine($"            {prop.Name} = {LowerFirst(prop.Name)};");
            writer.WriteLine("        }");
            writer.WriteLine();

            writer.WriteLine($"        public override SyntaxKind Kind => SyntaxKind.{node.Kind};");
            writer.WriteLine();

            foreach (var computed in node.ComputedProperties)
            {
                writer.WriteLine($"        public {computed.Type} {computed.Name}");
                writer.WriteLine($"            => {computed.Expression};");
                writer.WriteLine();
            }

            writer.WriteLine("    }");
            writer.WriteLine("}");

            Console.WriteLine($"Generated: {filePath}");
        }

        private static void GenerateRedClass(SyntaxNode node, string outputDir)
        {
            string className = node.Name;
            string baseClass = node.Base + "Syntax";
            string ns = $"Minsk.CodeAnalysis.Syntax.Nodes.{node.Category}s";
            string greenClassName = "Green" + StripSyntax(node.Name);
            string subDir = Path.Combine(outputDir, "Syntax", node.Category + "s");
            Directory.CreateDirectory(subDir);
            string filePath = Path.Combine(subDir, className + ".cs");
            string interfaceClause = node.Interfaces.Count > 0
                ? ", " + string.Join(", ", node.Interfaces)
                : "";

            using var writer = new StreamWriter(filePath);
            writer.WriteLine("using Minsk.CodeAnalysis.Syntax.Kind;");
            writer.WriteLine("using System.Collections.Immutable;");
            writer.WriteLine($"using Minsk.CodeAnalysis.Syntax.Green.{node.Category}s;");
            writer.WriteLine();
            writer.WriteLine($"namespace {ns}");
            writer.WriteLine("{");
            writer.WriteLine($"    public sealed class {className} : {baseClass}{interfaceClause}");
            writer.WriteLine("    {");
            writer.WriteLine($"        private readonly {greenClassName} _green;");
            writer.WriteLine();

            foreach (var prop in node.Properties)
            {
                if (!IsToken(prop.Type) && !prop.IsList)
                {
                    string redType = MapToRedType(prop.Type) + (prop.IsNullable ? "?" : "");
                    writer.WriteLine($"        private {redType} _{LowerFirst(prop.Name)};");
                }
            }
            if (node.Properties.Any(p => !IsToken(p.Type) && !p.IsList))
                writer.WriteLine();

            writer.WriteLine($"        internal {className}({greenClassName} green, SyntaxNode? parent, int position)");
            writer.WriteLine($"            : base(parent, green, position)");
            writer.WriteLine("        {");
            writer.WriteLine("            _green = green;");
            writer.WriteLine("        }");
            writer.WriteLine();

            writer.WriteLine($"        public override SyntaxKind Kind => SyntaxKind.{node.Kind};");
            writer.WriteLine();

            for (int i = 0; i < node.Properties.Count; i++)
            {
                var prop = node.Properties[i];

                if (IsToken(prop.Type))
                {
                    string tokenType = "SyntaxToken" + (prop.IsNullable ? "?" : "");
                    writer.WriteLine($"        public {tokenType} {prop.Name}");
                    writer.WriteLine($"            => new SyntaxToken(_green.{prop.Name}, this, GetChildPosition({i}));");
                }
                else if (prop.IsList)
                {
                    string elementType = GetListElementRedType(prop.Type);
                    writer.WriteLine($"        public ImmutableArray<{elementType}> {prop.Name}");
                    writer.WriteLine("        {");
                    writer.WriteLine("            get");
                    writer.WriteLine("            {");
                    writer.WriteLine($"                var builder = ImmutableArray.CreateBuilder<{elementType}>();");
                    writer.WriteLine($"                int pos = GetChildPosition({i});");
                    writer.WriteLine($"                foreach (var child in _green.{prop.Name})");
                    writer.WriteLine("                {");
                    writer.WriteLine($"                    builder.Add(({elementType})child.CreateRed(this, pos));");
                    writer.WriteLine("                    pos += child.FullWidth;");
                    writer.WriteLine("                }");
                    writer.WriteLine("                return builder.ToImmutable();");
                    writer.WriteLine("            }");
                    writer.WriteLine("        }");
                }
                else
                {
                    string redType = MapToRedType(prop.Type) + (prop.IsNullable ? "?" : "");
                    string fieldName = "_" + LowerFirst(prop.Name);
                    writer.WriteLine($"        public {redType} {prop.Name}");
                    writer.WriteLine("        {");
                    writer.WriteLine("            get");
                    writer.WriteLine("            {");
                    writer.WriteLine($"                if ({fieldName} == null)");
                    writer.WriteLine($"                    {fieldName} = ({MapToRedType(prop.Type)})SyntaxFactory.CreateRed(_green.{prop.Name}, this, GetChildPosition({i}));");
                    writer.WriteLine($"                return {fieldName};");
                    writer.WriteLine("            }");
                    writer.WriteLine("        }");
                }
                writer.WriteLine();
            }

            writer.WriteLine("    }");
            writer.WriteLine("}");

            Console.WriteLine($"Generated: {filePath}");
        }

        private static string MapToGreenType(string type) => type switch
        {
            "Token" => "GreenToken",
            "Expression" => "GreenExpression",
            "Statement" => "GreenStatement",
            "Block" => "GreenBlockStatement",
            "ElseClause" => "GreenElseClause",
            "SwitchLabel" => "GreenSwitchLabel",
            "EqualsValueClause" => "GreenEqualsValueClause",
            "VariableDeclaration" => "GreenVariableDeclaration",
            "VariableDeclarator" => "GreenVariableDeclarator",
            "ParameterList" => "GreenParameterList",
            "AccessorList" => "GreenAccessorList",
            "StatementList" => "GreenNodeList<GreenStatement>",
            "ExpressionList" => "GreenNodeList<GreenExpression>",
            "TokenList" => "GreenNodeList<GreenToken>",
            "SwitchLabelList" => "GreenNodeList<GreenSwitchLabel>",
            "VariableDeclaratorList" => "GreenNodeList<GreenVariableDeclarator>",
            "MemberDeclarationList" => "GreenNodeList<GreenMemberDeclaration>",
            "EnumMemberDeclarationList" => "GreenNodeList<GreenEnumMemberDeclaration>",
            "ParameterListItems" => "GreenNodeList<GreenParameter>",
            _ => "Green" + type
        };

        private static string MapToRedType(string type) => type switch
        {
            "Token" => "SyntaxToken",
            "Expression" => "ExpressionSyntax",
            "Statement" => "StatementSyntax",
            "Block" => "BlockStatementSyntax",
            "ElseClause" => "ElseClauseSyntax",
            "SwitchLabel" => "SwitchLabelSyntax",
            "EqualsValueClause" => "EqualsValueClauseSyntax",
            "VariableDeclaration" => "VariableDeclarationSyntax",
            "VariableDeclarator" => "VariableDeclaratorSyntax",
            "ParameterList" => "ParameterListSyntax",
            "AccessorList" => "AccessorListSyntax",
            "StatementList" => "ImmutableArray<StatementSyntax>",
            "ExpressionList" => "ImmutableArray<ExpressionSyntax>",
            "TokenList" => "ImmutableArray<SyntaxToken>",
            "SwitchLabelList" => "ImmutableArray<SwitchLabelSyntax>",
            "VariableDeclaratorList" => "ImmutableArray<VariableDeclaratorSyntax>",
            "MemberDeclarationList" => "ImmutableArray<MemberDeclarationSyntax>",
            "EnumMemberDeclarationList" => "ImmutableArray<EnumMemberDeclarationSyntax>",
            "ParameterListItems" => "ImmutableArray<ParameterSyntax>",
            _ => type + "Syntax"
        };

        private static string GetListElementRedType(string type) => type switch
        {
            "StatementList" => "StatementSyntax",
            "ExpressionList" => "ExpressionSyntax",
            "TokenList" => "SyntaxToken",
            "SwitchLabelList" => "SwitchLabelSyntax",
            "VariableDeclaratorList" => "VariableDeclaratorSyntax",
            "MemberDeclarationList" => "MemberDeclarationSyntax",
            "EnumMemberDeclarationList" => "EnumMemberDeclarationSyntax",
            "ParameterListItems" => "ParameterSyntax",
            _ => type.Replace("List", "Syntax")
        };

        private static bool IsToken(string type) => type == "Token";

        private static string StripSyntax(string name) =>
            name.EndsWith("Syntax") ? name[..^6] : name;

        private static string LowerFirst(string s) =>
            char.ToLowerInvariant(s[0]) + s[1..];
    }
}