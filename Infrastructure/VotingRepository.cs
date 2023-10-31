using Domain.Entities;

namespace Infrastructure;
public class VotingRepository
{
    private Voting? _currentVoting;

    public void Start(Voting voting)
    {
        _currentVoting = voting;
    }

    public Voting? Get()
    {
        return _currentVoting;
    }
}
