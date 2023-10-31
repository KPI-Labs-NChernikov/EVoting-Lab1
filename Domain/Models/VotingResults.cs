namespace Domain.Models;
public sealed class VotingResults : ICloneable
{
    public IReadOnlyCollection<CandidateVotingResults> CandidatesResults { get; }
    public DateTime? VotingCompletedAt { get; private set; }

    public VotingResults(IReadOnlyCollection<CandidateVotingResults> candidateResults)
    {
        CandidatesResults = candidateResults;
    }

    public void CompleteVoting()
    {
        if (VotingCompletedAt is not null)
        {
            return;
        }

        VotingCompletedAt = DateTime.Now;
    }

    public object Clone()
    {
        var candidatesClone = new List<CandidateVotingResults>();
        foreach (var candidateResults in CandidatesResults)
        {
            candidatesClone.Add(new CandidateVotingResults(new Candidate(candidateResults.Candidate.Id, candidateResults.Candidate.FullName)) { Votes = candidateResults.Votes });
        }

        var clone = new VotingResults(candidatesClone)
        {
            VotingCompletedAt = VotingCompletedAt
        };
        return clone;
    }
}
