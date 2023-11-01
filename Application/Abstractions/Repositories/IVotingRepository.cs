using Domain.Entities;

namespace Application.Abstractions.Repositories;
public interface IVotingRepository
{
    void Start(Voting voting);

    Voting? Get();
}
