using System.Collections;
using System.Collections.Generic;
using PSharp.CodeAnalysis.Diagnostics;
using PSharp.CodeAnalysis.Syntax.Kind;

namespace PSharp.CodeAnalysis.Syntax.Green;

internal class GreenNodeList : GreenNode, IEnumerable<GreenNode>
{
    private readonly List<GreenNode> _nodes;

    public GreenNodeList(List<GreenNode> nodes)
        : base(SyntaxKind.GreenNodeList)
    {
        _nodes = nodes ?? new List<GreenNode>();
    }

    public int Count => _nodes.Count;
    public GreenNode this[int index] => _nodes[index];

    public override int SlotCount => _nodes.Count;
    public override GreenNode? GetSlot(int index) => _nodes[index];

    public void Add(GreenNode node)
    {
        if (node != null)
            _nodes.Add(node);
    }

    protected override GreenNode CreateWithDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        var node = new GreenNodeList(_nodes);
        node.Diagnostics = diagnostics;
        return node;
    }

    public override string ToFullString()
    {
        var sb = new System.Text.StringBuilder();
        foreach (var child in _nodes)
            sb.Append(child.ToFullString());
        return sb.ToString();
    }

    // IEnumerable<GreenNode> implementation
    public IEnumerator<GreenNode> GetEnumerator() => _nodes.GetEnumerator();

    // Non-generic IEnumerable implementation
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}