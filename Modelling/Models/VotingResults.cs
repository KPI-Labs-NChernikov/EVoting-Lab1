namespace Modelling.Models;
public sealed class VotingResults
{
    public IReadOnlyCollection<CandidateVotingResults> CandidatesResults { get; }

    public VotingResults(IReadOnlyCollection<CandidateVotingResults> candidateResults)
    {
        CandidatesResults = candidateResults;
    }
}
