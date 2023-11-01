using Application.Abstractions.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories;
public class VotingRepository : IVotingRepository
{
    private Voting? _currentVoting;
    private static int _lastCandidateId = 0;

    public void Start(Voting voting)
    {
        foreach (var candidate in voting.Candidates)
        {
            _lastCandidateId++;
            candidate.Id = _lastCandidateId;
        }

        _currentVoting = voting;
    }

    public Voting? Get()
    {
        return _currentVoting;
    }
}
