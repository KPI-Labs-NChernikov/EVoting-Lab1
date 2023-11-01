using Application.Dtos;
using FluentResults;

namespace Application.Services;
public class CentralElectionCommissionService
{
    public Result StartVoting(IReadOnlyCollection<CreateCandidateDto> candidates)
    {

    }

    public Result AcceptBallot(byte[] encryptedSignedBallot)
    {

    }

    public Result CompleteVoting()
    {

    }

    public Result<IReadOnlyCollection<CandidateDto>> GetVotingResults()
    {

    }

    public Result<IReadOnlyCollection<CandidateDto>> GetVotingResultsOrdered()
    {

    }
}
