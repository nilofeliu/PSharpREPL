using Minsk.CodeAnalysis.Syntax.Parser;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Syntax.Nodes;
using PSharp.CodeAnalysis.Syntax.Parser;
using PSharp.CodeAnalysis.Text;
using System.Collections.Immutable;

namespace PSharp.CodeAnalysis
{
    public sealed class SyntaxTree
    {
        public SourceText Text { get; }
        public ImmutableArray<Diagnostic> Diagnostics { get; }
        public CompilationUnitSyntax Root { get; }
        public SyntaxToken EndOfFileToken { get; }
        
        public SyntaxTree(SourceText text) 
        {
            var parser = new SyntaxParser(text);
            var root = parser.ParseCompilationUnit();
            
            Text = text;
            //Diagnostics = parser.Diagnostics.ToImmutableArray(); 
            Diagnostics = parser.Diagnostics
                    .Concat(root.DescendantTokens().SelectMany(t => t.GetDiagnostics()))
                    .ToImmutableArray();
            Root = root;
                         
        }

        public static SyntaxTree Parse(string text)
        {
            var sourceText = SourceText.From(text);
            return Parse(sourceText);
        }
        public static SyntaxTree Parse(SourceText text)
        {            
            return new SyntaxTree(text);
        }
        public static ImmutableArray<SyntaxToken> ParseTokens(string text)
        {
            var sourceText = SourceText.From(text);
            return ParseTokens(sourceText);
        }

        public static ImmutableArray<SyntaxToken> ParseTokens(string text, out ImmutableArray<Diagnostic> diagnostics)
        {
            var sourceText = SourceText.From(text);
            return ParseTokens(sourceText, out diagnostics);
        }

        public static ImmutableArray<SyntaxToken> ParseTokens(SourceText text)
        {
            return ParseTokens(text, out _);
        }
        public static ImmutableArray<SyntaxToken> ParseTokens(SourceText text, out ImmutableArray<Diagnostic> diagnostics)
        {
            IEnumerable<SyntaxToken> LexTokens(Lexer lexer)
            {
                while (true)
                {
                    var token = lexer.Lex();
                    if (token.Kind == SyntaxKind.EndOfFileToken)
                    {
                        yield return token; // ← include EOF so its leading trivia (comments) gets rendered
                        break;
                    }
                    yield return token;
                }
            }

            var l = new Lexer(text);
            var result = LexTokens(l).ToImmutableArray();
            diagnostics = result.SelectMany(t => t.GetDiagnostics()).ToImmutableArray();
            return result;
        }
    }
}
