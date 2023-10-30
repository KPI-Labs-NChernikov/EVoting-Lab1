namespace Domain.Entities;
public class VotingResults
{
    public IReadOnlyCollection<CandidateVotingResults> CandidateResults { get; }

    public IReadOnlyCollection<CandidateVotingResults> Ordered => CandidateResults.OrderByDescending(c => c.Votes).ToList();

    public VotingResults(IReadOnlyCollection<CandidateVotingResults> candidateResults)
    {
        CandidateResults = candidateResults;
    }
}
