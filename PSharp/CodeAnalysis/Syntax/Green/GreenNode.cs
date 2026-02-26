using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;
using System.Collections.Generic;

namespace PSharp.CodeAnalysis.Syntax.Green
{
    public abstract class GreenNode
    {
        public virtual SyntaxKind Kind { get; }
        public int FullWidth { get; protected set; } // length including trivia
        public int Width => FullWidth - LeadingWidth - TrailingWidth;
        public virtual int LeadingWidth { get; protected set; } // leading trivia length
        public virtual int TrailingWidth { get; protected set; } // trailing trivia length

        public virtual int SlotCount => 0;
        public virtual GreenNode? GetSlot(int index) => null;

        public DiagnosticInfo[]? Diagnostics { get; protected set; }
        public bool ContainsDiagnostics => Diagnostics != null && Diagnostics.Length > 0;
        public virtual bool IsEquivalentTo(GreenNode other)
            => other != null && Kind == other.Kind && Text == other.Text;
        protected GreenNode(SyntaxKind kind)
        {
            Kind = kind;
        }

        // For tokens that have text
        public virtual string Text => "";

        // NEW: Child enumeration using slots (default implementation)
        public virtual IEnumerable<GreenNode> GetChildren()
        {
            for (int i = 0; i < SlotCount; i++)
            {
                var child = GetSlot(i);
                if (child != null)
                {
                    yield return child;
                }
            }
        }

        public GreenNode WithDiagnostics(params DiagnosticInfo[] diagnostics)
            => CreateWithDiagnostics(diagnostics);

        public DiagnosticInfo[] GetDiagnostics() => Diagnostics ?? Array.Empty<DiagnosticInfo>();

        protected abstract GreenNode CreateWithDiagnostics(DiagnosticInfo[]? diagnostics);

        public abstract string ToFullString();

        private static void PrettyPrint(TextWriter writer, GreenNode node, string indent = "", bool isLast = true)
        {
            // |__
            // |--
            // |

            var isToConsole = writer == Console.Out;

            var marker = isLast ? "└──" : "├──";

            if (isToConsole)
                Console.ForegroundColor = ConsoleColor.DarkGray;

            writer.Write($"{indent}");

            writer.Write($"{marker}");

            if (isToConsole)
                Console.ForegroundColor = node is SyntaxToken ? ConsoleColor.Blue : ConsoleColor.Cyan;

            writer.Write($"{node.Kind}");

            if (node is GreenToken t && t.Value != null)
            {
                writer.Write($" ");
                writer.Write(t.Value);
            }

            if (isToConsole)
                Console.ResetColor();

            writer.WriteLine();

            indent += isLast ? "   " : "│  ";


            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
            {
                PrettyPrint(writer, child, indent, child == lastChild);
            }

        }

        public void WriteTo(TextWriter writer)
        {
            PrettyPrint(writer, this);
        }

        public override string ToString()
        {
            using (var writer = new StringWriter())
            {
                WriteTo(writer);
                return writer.ToString();
            }
        }
    }
}
