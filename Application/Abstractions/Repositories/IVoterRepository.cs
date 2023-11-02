using Domain.Entities;

namespace Application.Abstractions.Repositories;
public interface IVoterRepository
{
    void Add(Voter voter);
    Voter? GetById(int id);
}
