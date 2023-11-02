using Application.Abstractions.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories;
public class VoterRepository : IVoterRepository
{
    private readonly Dictionary<int, Voter> _voters = new();

    private static int _lastVoterId = 0;

    public void Add(Voter voter)
    {
        _lastVoterId++;
        voter.Id = _lastVoterId;
        _voters[voter.Id] = voter;
    }

    public Voter? GetById(int id)
    {
        return _voters.GetValueOrDefault(id);
    }

    public IReadOnlyCollection<Voter> GetAll()
    {
        return _voters.Values.ToList();
    }
}
