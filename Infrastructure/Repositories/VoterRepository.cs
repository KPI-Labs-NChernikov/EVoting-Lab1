using Application.Abstractions.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories;
public class VoterRepository : IVoterRepository
{
    private readonly Dictionary<int, Voter> _votes = new();

    private static int _lastVoterId = 0;

    public void AddOrUpdate(Voter voter)
    {
        _lastVoterId++;
        voter.Id = _lastVoterId;
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
