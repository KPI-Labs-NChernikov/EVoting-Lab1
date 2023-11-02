namespace Domain.Entities;
public sealed class Candidate
{
    public Guid Id { get; set; }

    public string FullName { get; }

    public int Votes { get; private set; }

    public Candidate(string fullName)
    {
        FullName = fullName;
    }

    public void AddVote()
    {
        Votes++;
    }
}
