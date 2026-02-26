using System.Xml.Linq;

namespace CodeGenerator;

public class ExpressionFactoryGenerator
{
    private class NodeInfo
    {
        public string Name { get; set; }
        public string Kind { get; set; }
        public string OperatorKind { get; set; }
        public List<string> Interfaces { get; set; } = new();
        public List<(string Name, string Type)> Properties { get; set; } = new();
    }

    public static void Run(string[] args)
    {
        string inputFile = args.Length > 0 ? args[0] : "Source/Expressions.xml";
        string factoryFile = args.Length > 1 ? args[1] : "Generated/ExpressionFactory.cs";
        string parserFile = args.Length > 2 ? args[2] : "Generated/LanguageParser.Expressions.cs";

        var doc = XDocument.Load(inputFile);
        var nodes = doc.Root.Elements("SyntaxNode")
            .Where(e => e.Attribute("Category")?.Value == "Expression")
            .Select(ParseNode)
            .ToList();

        Directory.CreateDirectory(Path.GetDirectoryName(factoryFile)!);
        Directory.CreateDirectory(Path.GetDirectoryName(parserFile)!);

        GenerateExpressionFactory(nodes, factoryFile);
        GenerateParserExpressions(nodes, parserFile);

        Console.WriteLine("Parser generation complete.");
    }

    private static void GenerateExpressionFactory(List<NodeInfo> nodes, string outputFile)
    {
        var binaryNodes = GetNodesByInterface(nodes, "IBinaryExpression");
        var comparisonNodes = GetNodesByInterface(nodes, "IComparisonExpression");
        var logicalNodes = GetNodesByInterface(nodes, "ILogicalExpression");
        var unaryNodes = GetNodesByInterface(nodes, "IUnaryExpression");
        var assignmentNodes = GetNodesByInterface(nodes, "IAssignmentExpression");
        var literalNodes = GetNodesByInterface(nodes, "ILiteralExpression");

        using var writer = new StreamWriter(outputFile);
        writer.WriteLine("using Minsk.CodeAnalysis.Syntax.Green.Expressions;");
        writer.WriteLine("using Minsk.CodeAnalysis.Syntax.InternalSyntax;");
        writer.WriteLine("using Minsk.CodeAnalysis.Syntax.Kind;");
        writer.WriteLine("using System;");
        writer.WriteLine();
        writer.WriteLine("namespace Minsk.CodeAnalysis.Syntax.Parser");
        writer.WriteLine("{");
        writer.WriteLine("    internal static partial class ExpressionFactory");
        writer.WriteLine("    {");

        // Binary
        if (binaryNodes.Any())
        {
            writer.WriteLine("        public static GreenExpression CreateBinary(GreenExpression left, GreenToken operatorToken, GreenExpression right)");
            writer.WriteLine("        {");
            writer.WriteLine("            return operatorToken.Kind switch");
            writer.WriteLine("            {");
            foreach (var node in binaryNodes)
            {
                string greenName = "Green" + StripSyntax(node.Name);
                writer.WriteLine($"                SyntaxKind.{node.OperatorKind} => new {greenName}(left, operatorToken, right),");
            }
            writer.WriteLine("                _ => throw new InvalidOperationException($\"Unexpected binary operator: {operatorToken.Kind}\")");
            writer.WriteLine("            };");
            writer.WriteLine("        }");
            writer.WriteLine();
        }

        // Comparison
        if (comparisonNodes.Any())
        {
            writer.WriteLine("        public static GreenExpression CreateComparison(GreenExpression left, GreenToken operatorToken, GreenExpression right)");
            writer.WriteLine("        {");
            writer.WriteLine("            return operatorToken.Kind switch");
            writer.WriteLine("            {");
            foreach (var node in comparisonNodes)
            {
                string greenName = "Green" + StripSyntax(node.Name);
                writer.WriteLine($"                SyntaxKind.{node.OperatorKind} => new {greenName}(left, operatorToken, right),");
            }
            writer.WriteLine("                _ => throw new InvalidOperationException($\"Unexpected comparison operator: {operatorToken.Kind}\")");
            writer.WriteLine("            };");
            writer.WriteLine("        }");
            writer.WriteLine();
        }

        // Logical
        if (logicalNodes.Any())
        {
            writer.WriteLine("        public static GreenExpression CreateLogical(GreenExpression left, GreenToken operatorToken, GreenExpression right)");
            writer.WriteLine("        {");
            writer.WriteLine("            return operatorToken.Kind switch");
            writer.WriteLine("            {");
            foreach (var node in logicalNodes)
            {
                string greenName = "Green" + StripSyntax(node.Name);
                writer.WriteLine($"                SyntaxKind.{node.OperatorKind} => new {greenName}(left, operatorToken, right),");
            }
            writer.WriteLine("                _ => throw new InvalidOperationException($\"Unexpected logical operator: {operatorToken.Kind}\")");
            writer.WriteLine("            };");
            writer.WriteLine("        }");
            writer.WriteLine();
        }

        // Unary — Pre and Post need separate handling
        if (unaryNodes.Any())
        {
            var preNodes = unaryNodes.Where(n => !n.Name.StartsWith("Post")).ToList();
            var postNodes = unaryNodes.Where(n => n.Name.StartsWith("Post")).ToList();

            writer.WriteLine("        public static GreenExpression CreatePrefixUnary(GreenToken operatorToken, GreenExpression operand)");
            writer.WriteLine("        {");
            writer.WriteLine("            return operatorToken.Kind switch");
            writer.WriteLine("            {");
            foreach (var node in preNodes)
            {
                string greenName = "Green" + StripSyntax(node.Name);
                writer.WriteLine($"                SyntaxKind.{node.OperatorKind} => new {greenName}(operatorToken, operand),");
            }
            writer.WriteLine("                _ => throw new InvalidOperationException($\"Unexpected prefix unary operator: {operatorToken.Kind}\")");
            writer.WriteLine("            };");
            writer.WriteLine("        }");
            writer.WriteLine();

            writer.WriteLine("        public static GreenExpression CreatePostfixUnary(GreenExpression operand, GreenToken operatorToken)");
            writer.WriteLine("        {");
            writer.WriteLine("            return operatorToken.Kind switch");
            writer.WriteLine("            {");
            foreach (var node in postNodes)
            {
                string greenName = "Green" + StripSyntax(node.Name);
                writer.WriteLine($"                SyntaxKind.{node.OperatorKind} => new {greenName}(operand, operatorToken),");
            }
            writer.WriteLine("                _ => throw new InvalidOperationException($\"Unexpected postfix unary operator: {operatorToken.Kind}\")");
            writer.WriteLine("            };");
            writer.WriteLine("        }");
            writer.WriteLine();
        }

        // Assignment
        if (assignmentNodes.Any())
        {
            writer.WriteLine("        public static GreenExpression CreateAssignment(GreenToken identifierToken, GreenToken operatorToken, GreenExpression expression)");
            writer.WriteLine("        {");
            writer.WriteLine("            return operatorToken.Kind switch");
            writer.WriteLine("            {");
            foreach (var node in assignmentNodes)
            {
                string greenName = "Green" + StripSyntax(node.Name);
                writer.WriteLine($"                SyntaxKind.{node.OperatorKind} => new {greenName}(identifierToken, operatorToken, expression),");
            }
            writer.WriteLine("                _ => throw new InvalidOperationException($\"Unexpected assignment operator: {operatorToken.Kind}\")");
            writer.WriteLine("            };");
            writer.WriteLine("        }");
            writer.WriteLine();
        }

        // Literal
        if (literalNodes.Any())
        {
            writer.WriteLine("        public static GreenExpression CreateLiteral(GreenToken token)");
            writer.WriteLine("        {");
            writer.WriteLine("            return token.Kind switch");
            writer.WriteLine("            {");
            foreach (var node in literalNodes)
            {
                string greenName = "Green" + StripSyntax(node.Name);
                string tokenKind = node.Kind.Replace("Expression", "Token");
                writer.WriteLine($"                SyntaxKind.{tokenKind} => new {greenName}(token),");
            }
            writer.WriteLine("                _ => throw new InvalidOperationException($\"Invalid literal token kind: {token.Kind}\")");
            writer.WriteLine("            };");
            writer.WriteLine("        }");
            writer.WriteLine();
        }

        writer.WriteLine("    }");
        writer.WriteLine("}");

        Console.WriteLine($"Generated: {outputFile}");
    }

    private static void GenerateParserExpressions(List<NodeInfo> nodes, string outputFile)
    {
        var literalNodes = GetNodesByInterface(nodes, "ILiteralExpression");
        var otherNodes = nodes.Where(n => !n.Interfaces.Any()).ToList();

        using var writer = new StreamWriter(outputFile);
        writer.WriteLine("using Minsk.CodeAnalysis.Syntax.Green.Expressions;");
        writer.WriteLine("using Minsk.CodeAnalysis.Syntax.InternalSyntax;");
        writer.WriteLine("using Minsk.CodeAnalysis.Syntax.Kind;");
        writer.WriteLine();
        writer.WriteLine("namespace Minsk.CodeAnalysis.Syntax.Parser");
        writer.WriteLine("{");
        writer.WriteLine("    internal partial class LanguageParser");
        writer.WriteLine("    {");

        // ParseAssignmentExpression template
        writer.WriteLine("        private GreenExpression ParseAssignmentExpression()");
        writer.WriteLine("        {");
        writer.WriteLine("            if (PeekToken(0).Kind == SyntaxKind.IdentifierToken &&");
        writer.WriteLine("                SyntaxFacts.IsAssignmentOperator(PeekToken(1).Kind))");
        writer.WriteLine("            {");
        writer.WriteLine("                var identifierToken = EatToken();");
        writer.WriteLine("                var operatorToken = EatToken();");
        writer.WriteLine("                var right = ParseAssignmentExpression();");
        writer.WriteLine("                return ExpressionFactory.CreateAssignment(identifierToken, operatorToken, right);");
        writer.WriteLine("            }");
        writer.WriteLine("            return ParseOperatorExpression();");
        writer.WriteLine("        }");
        writer.WriteLine();

        // ParseOperatorExpression template
        writer.WriteLine("        private GreenExpression ParseOperatorExpression(int parentPrecedence = 0)");
        writer.WriteLine("        {");
        writer.WriteLine("            GreenExpression left;");
        writer.WriteLine("            var unaryPrecedence = CurrentToken.Kind.GetUnaryOperatorPrecedence();");
        writer.WriteLine("            if (unaryPrecedence != 0 && unaryPrecedence >= parentPrecedence)");
        writer.WriteLine("            {");
        writer.WriteLine("                var operatorToken = EatToken();");
        writer.WriteLine("                var operand = ParseOperatorExpression(unaryPrecedence);");
        writer.WriteLine("                left = ExpressionFactory.CreatePrefixUnary(operatorToken, operand);");
        writer.WriteLine("            }");
        writer.WriteLine("            else");
        writer.WriteLine("            {");
        writer.WriteLine("                left = ParsePrimaryExpression();");
        writer.WriteLine("            }");
        writer.WriteLine("            while (true)");
        writer.WriteLine("            {");
        writer.WriteLine("                var precedence = CurrentToken.Kind.GetBinaryOperatorPrecedence();");
        writer.WriteLine("                if (precedence == 0 || precedence <= parentPrecedence)");
        writer.WriteLine("                    break;");
        writer.WriteLine("                var operatorToken = EatToken();");
        writer.WriteLine("                var right = ParseOperatorExpression(precedence);");
        writer.WriteLine("                left = CurrentToken.Kind.IsComparisonOperator()");
        writer.WriteLine("                    ? ExpressionFactory.CreateComparison(left, operatorToken, right)");
        writer.WriteLine("                    : CurrentToken.Kind.IsLogicalOperator()");
        writer.WriteLine("                        ? ExpressionFactory.CreateLogical(left, operatorToken, right)");
        writer.WriteLine("                        : ExpressionFactory.CreateBinary(left, operatorToken, right);");
        writer.WriteLine("            }");
        writer.WriteLine("            return left;");
        writer.WriteLine("        }");
        writer.WriteLine();

        // ParsePrimaryExpression dispatch
        writer.WriteLine("        private GreenExpression ParsePrimaryExpression()");
        writer.WriteLine("        {");
        writer.WriteLine("            return CurrentToken.Kind switch");
        writer.WriteLine("            {");
        // Literals
        foreach (var node in literalNodes)
        {
            string methodName = "Parse" + StripSyntax(node.Name);
            string tokenKind = node.Kind.Replace("Expression", "Token");
            writer.WriteLine($"                SyntaxKind.{tokenKind} => {methodName}(),");
        }
        // Name and parenthesized
        writer.WriteLine("                SyntaxKind.IdentifierToken => ParseNameExpression(),");
        writer.WriteLine("                SyntaxKind.OpenParenthesisToken => ParseParenthesizedExpression(),");
        writer.WriteLine("                _ => ParseNameExpression() // fallback");
        writer.WriteLine("            };");
        writer.WriteLine("        }");
        writer.WriteLine();

        // Generate individual Parse* methods for literals and other nodes
        foreach (var node in literalNodes.Concat(otherNodes))
        {
            string greenName = "Green" + StripSyntax(node.Name);
            string methodName = "Parse" + StripSyntax(node.Name);

            writer.WriteLine($"        private {greenName} {methodName}()");
            writer.WriteLine("        {");

            var argList = new List<string>();
            foreach (var (propName, propType) in node.Properties)
            {
                string varName = LowerFirst(propName);
                switch (propType)
                {
                    case "Token":
                        string tokenKind = node.Kind.Replace("Expression", "Token");
                        writer.WriteLine($"            var {varName} = EatToken(SyntaxKind.{tokenKind});");
                        break;
                    case "Expression":
                        writer.WriteLine($"            var {varName} = ParseExpression();");
                        break;
                    case "Statement":
                        writer.WriteLine($"            var {varName} = ParseStatement();");
                        break;
                    default:
                        writer.WriteLine($"            var {varName} = Parse{propType}();");
                        break;
                }
                argList.Add(varName);
            }

            writer.WriteLine($"            return new {greenName}({string.Join(", ", argList)});");
            writer.WriteLine("        }");
            writer.WriteLine();
        }

        writer.WriteLine("    }");
        writer.WriteLine("}");

        Console.WriteLine($"Generated: {outputFile}");
    }

    private static NodeInfo ParseNode(XElement elem) => new NodeInfo
    {
        Name = elem.Attribute("Name").Value,
        Kind = elem.Attribute("Kind").Value,
        OperatorKind = elem.Attribute("OperatorKind")?.Value,
        Interfaces = (elem.Attribute("Interfaces")?.Value ?? "")
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .ToList(),
        Properties = elem.Elements("Property")
            .Select(p => (p.Attribute("Name").Value, p.Attribute("Type").Value))
            .ToList()
    };

    private static List<NodeInfo> GetNodesByInterface(List<NodeInfo> nodes, string interfaceName)
        => nodes.Where(n => n.Interfaces.Contains(interfaceName)).ToList();

    private static string StripSyntax(string name) =>
        name.EndsWith("Syntax") ? name[..^6] : name;

    private static string LowerFirst(string s) =>
        char.ToLowerInvariant(s[0]) + s[1..];
}