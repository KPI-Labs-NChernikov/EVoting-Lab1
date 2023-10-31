using Application.Dtos;

namespace Application.Services;
public class CentralElectionCommissionService
{
    public Action StartVoting(IReadOnlyCollection<CreateCandidateDto> candidates)
    {

    }

    public Action AcceptBallot(byte[] encryptedSignedBallot)
    {

    }

    public Action CompleteVoting()
    {

    }

    public Action<IReadOnlyCollection<CandidateDto>> GetVotingResults()
    {

    }

    public Action<IReadOnlyCollection<CandidateDto>> GetVotingResultsOrdered()
    {

    }
}
