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
writer.WriteLine("using PSharp.CodeAnalysis;");
writer.WriteLine("using PSharp.CodeAnalysis.Diagnostics;");
writer.WriteLine("using PSharp.CodeAnalysis.Syntax.Green;");
writer.WriteLine("using PSharp.CodeAnalysis.Syntax.Kind;");
writer.WriteLine("using PSharp.CodeAnalysis.Syntax.Nodes;");
writer.WriteLine("using PSharp.CodeAnalysis.Syntax.Nodes.Interfaces;");
        writer.WriteLine();
        writer.WriteLine("namespace PSharp.CodeAnalysis.Syntax.Parser");
        writer.WriteLine("{");
        writer.WriteLine("    internal static partial class ExpressionFactory");
        writer.WriteLine("    {");

        // Binary
        EmitFactory(writer, "CreateBinary",
            "GreenExpression left, GreenToken operatorToken, GreenExpression right",
            "Unexpected binary operator",
            binaryNodes,
            n => $"new Green{StripSyntax(n.Name)}(left, operatorToken, right)",
            n => n.OperatorKind);

        // Comparison
        EmitFactory(writer, "CreateComparison",
            "GreenExpression left, GreenToken operatorToken, GreenExpression right",
            "Unexpected comparison operator",
            comparisonNodes,
            n => $"new Green{StripSyntax(n.Name)}(left, operatorToken, right)",
            n => n.OperatorKind);

        // Logical
        EmitFactory(writer, "CreateLogical",
            "GreenExpression left, GreenToken operatorToken, GreenExpression right",
            "Unexpected logical operator",
            logicalNodes,
            n => $"new Green{StripSyntax(n.Name)}(left, operatorToken, right)",
            n => n.OperatorKind);

        // Prefix Unary
        var preNodes = unaryNodes.Where(n => !n.Name.StartsWith("Post")).ToList();
        var postNodes = unaryNodes.Where(n => n.Name.StartsWith("Post")).ToList();

        EmitFactory(writer, "CreatePrefixUnary",
            "GreenToken operatorToken, GreenExpression operand",
            "Unexpected prefix unary operator",
            preNodes,
            n => $"new Green{StripSyntax(n.Name)}(operatorToken, operand)",
            n => n.OperatorKind);

        // Postfix Unary
        EmitFactory(writer, "CreatePostfixUnary",
            "GreenExpression operand, GreenToken operatorToken",
            "Unexpected postfix unary operator",
            postNodes,
            n => $"new Green{StripSyntax(n.Name)}(operand, operatorToken)",
            n => n.OperatorKind);

        // Assignment
        EmitFactory(writer, "CreateAssignment",
            "GreenToken identifierToken, GreenToken operatorToken, GreenExpression expression",
            "Unexpected assignment operator",
            assignmentNodes,
            n => $"new Green{StripSyntax(n.Name)}(identifierToken, operatorToken, expression)",
            n => n.OperatorKind);

        // Literal
        EmitFactory(writer, "CreateLiteral",
            "GreenToken token",
            "Invalid literal token kind",
            literalNodes,
            n => $"new Green{StripSyntax(n.Name)}(token)",
            n => n.Kind.Replace("Expression", "Token"));

        writer.WriteLine("    }");
        writer.WriteLine("}");

        Console.WriteLine($"Generated: {outputFile}");
    }

    private static void EmitFactory(
        StreamWriter writer,
        string methodName,
        string parameters,
        string errorMessage,
        List<NodeInfo> nodes,
        Func<NodeInfo, string> bodyExpr,
        Func<NodeInfo, string> kindSelector)
    {
        if (!nodes.Any()) return;

        string switchTarget = parameters.Split(',')[0].Trim().Split(' ').Last();

        writer.WriteLine($"        public static GreenExpression {methodName}({parameters})");
        writer.WriteLine("        {");
        writer.WriteLine($"            return {switchTarget}.Kind switch");
        writer.WriteLine("            {");
        foreach (var node in nodes)
            writer.WriteLine($"                SyntaxKind.{kindSelector(node)} => {bodyExpr(node)},");
        writer.WriteLine($"                _ => throw new InvalidOperationException($\"{errorMessage}: {{{switchTarget}.Kind}}\")");
        writer.WriteLine("            };");
        writer.WriteLine("        }");
        writer.WriteLine();
    }

    private static void GenerateParserExpressions(List<NodeInfo> nodes, string outputFile)
    {
        var literalNodes = GetNodesByInterface(nodes, "ILiteralExpression");
        var otherNodes = nodes.Where(n => !n.Interfaces.Any()).ToList();

        using var writer = new StreamWriter(outputFile);
        writer.WriteLine("using PSharp.CodeAnalysis.Syntax.Green.Expressions;");
        writer.WriteLine("using PSharp.CodeAnalysis.Syntax.InternalSyntax;");
        writer.WriteLine("using PSharp.CodeAnalysis.Syntax.Kind;");
        writer.WriteLine();
        writer.WriteLine("namespace PSharp.CodeAnalysis.Syntax.Parser");
        writer.WriteLine("{");
        writer.WriteLine("    internal partial class LanguageParser");
        writer.WriteLine("    {");

        // ParseAssignmentExpression
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

        // ParseOperatorExpression
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
        writer.WriteLine("                left = operatorToken.Kind.IsComparisonOperator()");
        writer.WriteLine("                    ? ExpressionFactory.CreateComparison(left, operatorToken, right)");
        writer.WriteLine("                    : operatorToken.Kind.IsLogicalOperator()");
        writer.WriteLine("                        ? ExpressionFactory.CreateLogical(left, operatorToken, right)");
        writer.WriteLine("                        : ExpressionFactory.CreateBinary(left, operatorToken, right);");
        writer.WriteLine("            }");
        writer.WriteLine("            return left;");
        writer.WriteLine("        }");
        writer.WriteLine();

        // ParsePrimaryExpression
        writer.WriteLine("        private GreenExpression ParsePrimaryExpression()");
        writer.WriteLine("        {");
        writer.WriteLine("            return CurrentToken.Kind switch");
        writer.WriteLine("            {");
        foreach (var node in literalNodes)
        {
            string methodName = "Parse" + StripSyntax(node.Name);
            string tokenKind = node.Kind.Replace("Expression", "Token");
            writer.WriteLine($"                SyntaxKind.{tokenKind} => {methodName}(),");
        }
        writer.WriteLine("                SyntaxKind.IdentifierToken => ParseNameExpression(),");
        writer.WriteLine("                SyntaxKind.OpenParenthesisToken => ParseParenthesizedExpression(),");
        writer.WriteLine("                _ => ParseNameExpression() // fallback");
        writer.WriteLine("            };");
        writer.WriteLine("        }");
        writer.WriteLine();

        // Individual Parse* methods for literals and other nodes
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
                        // Use property name to derive token kind
                        writer.WriteLine($"            var {varName} = EatToken(SyntaxKind.{propName});");
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