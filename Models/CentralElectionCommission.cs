using Algorithms.Abstractions;
using FluentResults;
using Models.Ballots;

namespace Models;
public class CentralElectionCommission
{
    private readonly Dictionary<int, CandidateVotingResults> _candidates = new();

    private readonly Dictionary<int, Voter> _voters = new();

    private readonly HashSet<int> _acceptedVotersIds = new();

    public VotingResults VotingResults => new(_candidates.Values);

    public byte[] BallotEncryptionKey { get; }

    public CentralElectionCommission(IEnumerable<Candidate> candidates, IEnumerable<Voter> voters, ISymmetricKeyGenerator keyGenerator)
    {
        foreach (var candidate in candidates)
        {
            _candidates.Add(candidate.Id, new CandidateVotingResults(candidate));
        }

        foreach (var voter in voters)
        {
            _voters.Add(voter.Id, voter);
        }

        BallotEncryptionKey = keyGenerator.GenerateKey();
    }

    public Result AcceptBallot(EncryptedSignedBallot ballot, ISignatureProvider signatureProvider, IEncryptionProvider encryptionProvider, IObjectToByteArrayTransformer objectToByteArrayTransformer)
    {

    }
}
