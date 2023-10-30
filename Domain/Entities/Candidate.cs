namespace Domain.Entities;
public class Candidate
{
    public int Id { get; }

    public string FullName { get; }

    public Candidate(int id, string fullName)
    {
        Id = id;
        FullName = fullName;
    }
}
