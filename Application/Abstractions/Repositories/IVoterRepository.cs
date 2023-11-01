using Domain.Entities;

namespace Application.Abstractions.Repositories;
public interface IVoterRepository
{
    void AddOrUpdate(Voter voter);
    Voter? GetById(int id);
    void RemoveAll();
}
