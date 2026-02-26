using PSharp.CodeAnalysis.Syntax;
using PSharp.CodeAnalysis.Syntax.Green;
using PSharp.CodeAnalysis.Syntax.InternalSyntax;
using PSharp.CodeAnalysis.Syntax.Kind;
using PSharp.CodeAnalysis.Text;

namespace PSharp.CodeAnalysis
{
    public abstract class SyntaxNode
    {
        private readonly GreenNode _green;
        private readonly SyntaxNode? _parent;
        private readonly int _position;

        protected SyntaxNode()
        { }

        protected SyntaxNode(SyntaxNode? parent, GreenNode green, int position)
        {
            _parent = parent;
            _green = green;
            _position = position;
        }

        public abstract SyntaxKind Kind { get; }
        public int Position => _position;
        public int FullWidth => _green.FullWidth;
        public int Width => _green.Width;
        public SyntaxNode? Parent => _parent;
        public GreenNode Green => _green;

        public virtual TextSpan Span => new TextSpan(_position + _green.LeadingWidth, _green.Width);
        public TextSpan FullSpan => new TextSpan(_position, _green.FullWidth);

        protected int GetChildPosition(int index)
        {
            int offset = _position;
            for (int i = 0; i < index; i++)
            {
                var child = _green.GetSlot(i);
                if (child != null)
                    offset += child.FullWidth;
            }
            return offset;
        }

        public IEnumerable<SyntaxNode> GetChildren()
        {
            for (int i = 0; i < _green.SlotCount; i++)
            {
                var greenChild = _green.GetSlot(i);
                if (greenChild != null)
                    yield return RedNodeFactory.CreateRed(greenChild, this, GetChildPosition(i));
            }
        }

        public IEnumerable<SyntaxToken> DescendantTokens()
        {
            foreach (var child in GetChildren())
            {
                if (child is SyntaxToken token)
                    yield return token;
                else
                    foreach (var descendant in child.DescendantTokens())
                        yield return descendant;
            }
        }

        public SyntaxToken GetLastToken()
        {
            if (this is SyntaxToken token)
                return token;
            return GetChildren().Last().GetLastToken();
        }

        public void WriteTo(TextWriter writer)
        {
           _green.WriteTo(writer);
        }
              

        public override string ToString()
        {
            using var writer = new StringWriter();
            WriteTo(writer);
            return writer.ToString();
        }
    }
}





//using PSharp.CodeAnalysis.Syntax.Kind;
//using PSharp.CodeAnalysis.Text;
//using System.Reflection;

//namespace PSharp.CodeAnalysis
//{
//    public abstract class SyntaxNode
//    {
//        public abstract SyntaxKind Kind { get; }

//        public virtual TextSpan Span
//        {
//            get
//            {
//                var first = GetChildren().First().Span;
//                var last = GetChildren().Last().Span;
//                return TextSpan.FromBounds(first.Start, last.End);
//            }
//        }

//        public IEnumerable<SyntaxNode> GetChildren()
//        {
//            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

//            foreach (var property in properties)
//            {
//                if (typeof(SyntaxNode).IsAssignableFrom(property.PropertyType))
//                {
//                    var child = (SyntaxNode?)property.GetValue(this);
//                    if (child != null)
//                        yield return child;
//                }
//                else if (typeof(IEnumerable<SyntaxNode>).IsAssignableFrom(property.PropertyType))
//                {
//                    var children = (IEnumerable<SyntaxNode>?)property.GetValue(this);
//                    if (children != null)
//                    {
//                        foreach (var child in children)
//                            if (child != null)
//                                yield return child;
//                    }
//                }
//                else if (property.PropertyType.IsGenericType &&
//                         property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
//                         typeof(IEnumerable<SyntaxNode>).IsAssignableFrom(Nullable.GetUnderlyingType(property.PropertyType)))
//                {
//                    var children = (IEnumerable<SyntaxNode>?)property.GetValue(this);
//                    if (children != null)
//                    {
//                        foreach (var child in children)
//                            if (child != null)
//                                yield return child;
//                    }
//                }
//            }
//        }

//        public IEnumerable<SyntaxToken> DescendantTokens()
//        {
//            foreach (var child in GetChildren())
//            {
//                if (child is SyntaxToken token)
//                    yield return token;
//                else
//                    foreach (var descendant in child.DescendantTokens())
//                        yield return descendant;
//            }
//        }

//        public SyntaxToken GetLastToken()
//        {
//            if (this is SyntaxToken token)
//                return token;

//            // A syntax node should always contain at least 1 token.
//            return GetChildren().Last().GetLastToken();
//        }

//        public void WriteTo(TextWriter writer)
//        {
//            PrettyPrint(writer, this);
//        }

//        private static void PrettyPrint(TextWriter writer, SyntaxNode node, string indent = "", bool isLast = true)
//        {
//            // |__
//            // |--
//            // |

//            var isToConsole = writer == Console.Out;

//            var marker = isLast ? "└──" : "├──";

//            if (isToConsole)
//                Console.ForegroundColor = ConsoleColor.DarkGray;

//            writer.Write($"{indent}");

//            writer.Write($"{marker}");

//            if (isToConsole)
//                Console.ForegroundColor = node is SyntaxToken ? ConsoleColor.Blue : ConsoleColor.Cyan;

//            writer.Write($"{node.Kind}");

//            if (node is SyntaxToken t && t.Value != null)
//            {
//                writer.Write($" ");
//                writer.Write(t.Value);
//            }

//            if (isToConsole)
//                Console.ResetColor();

//            writer.WriteLine();

//            indent += isLast ? "   " : "│  ";


//            var lastChild = node.GetChildren().LastOrDefault();

//            foreach (var child in node.GetChildren())
//            {
//                PrettyPrint(writer, child, indent, child == lastChild);
//            }

//        }

//        public override string ToString()
//        {
//            using (var writer = new StringWriter())
//            {
//                WriteTo(writer);
//                return writer.ToString();
//            }
//        }
//    }
//}
