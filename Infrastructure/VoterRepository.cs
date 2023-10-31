using Domain.Entities;

namespace Infrastructure;
public class VoterRepository
{
    private readonly Dictionary<int, Voter> _votes = new Dictionary<int, Voter>();

    public void AddOrUpdate(Voter voter)
    {
        _votes[voter.Id] = voter;
    }

    public Voter? GetById(int id)
    {
        return _votes.GetValueOrDefault(id);
    }

    public void RemoveAll() 
    { 
        _votes.Clear(); 
    }
}
