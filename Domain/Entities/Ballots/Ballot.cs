namespace Domain.Entities.Ballots;
public class Ballot
{
    public int VoterId { get; }

    public int CandidateId { get; }

    public Ballot(int voterId, int candidateId)
    {
        VoterId = voterId;
        CandidateId = candidateId;
    }
}
