using System.Collections.Immutable;

namespace PSharp.CodeAnalysis.Binding.Semantics.Operators;

internal struct UnaryOperatorOverloadResolutionResult
{
    public UnaryOperatorAnalysisResult? Best { get; private set; }
    public ImmutableArray<UnaryOperatorAnalysisResult> Candidates { get; }

    private UnaryOperatorOverloadResolutionResult(
        UnaryOperatorAnalysisResult? best,
        ImmutableArray<UnaryOperatorAnalysisResult> candidates)
    {
        Best = best;
        Candidates = candidates;
    }

    public static UnaryOperatorOverloadResolutionResult Create(
        ImmutableArray<UnaryOperatorAnalysisResult> candidates)
    {
        var best = FindBest(candidates);
        return new UnaryOperatorOverloadResolutionResult(best, candidates);
    }

    private static UnaryOperatorAnalysisResult? FindBest(ImmutableArray<UnaryOperatorAnalysisResult> candidates)
    {
        UnaryOperatorAnalysisResult? best = null;

        foreach (var candidate in candidates)
        {
            if (!candidate.IsValid)
                continue;

            if (best == null)
            {
                best = candidate;
                continue;
            }

            // Apply betterness rules here later
            // For now, just take first valid
        }

        return best;
    }
}