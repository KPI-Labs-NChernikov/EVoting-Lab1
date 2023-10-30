namespace Domain.Entities;
public class CandidateVotingResults
{
    public Candidate Candidate { get; }

    public int Votes { get; private set; }

    public CandidateVotingResults(Candidate candidate)
    {
        Candidate = candidate;
    }

    public void AddVote()
    {
        Votes++;
    }
}
