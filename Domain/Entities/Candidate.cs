namespace Domain.Entities;
public sealed class Candidate
{
    public int Id { get; }

    public string FullName { get; }

    public int Votes { get; private set; }

    public Candidate(int id, string fullName)
    {
        Id = id;
        FullName = fullName;
    }

    public void AddVote()
    {
        Votes++;
    }
}
