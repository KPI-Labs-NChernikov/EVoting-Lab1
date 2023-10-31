namespace Domain.Entities;
public sealed class Ballot
{
    public Candidate Candidate { get; }

    public Ballot(Candidate candidate)
    {
        Candidate = candidate;
    }
}
