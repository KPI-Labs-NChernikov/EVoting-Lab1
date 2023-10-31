using Algorithms.Abstractions;
using FluentResults;
using Domain.Models.Ballots;

namespace Domain.Models;
public sealed class CentralElectionCommission
{
    private readonly Dictionary<int, CandidateVotingResults> _candidates = new();

    private readonly Dictionary<int, Voter> _voters = new();

    private readonly HashSet<int> _acceptedVotersIds = new();

    public bool VotingCompleted { get; set; }
    private readonly VotingResults _votingResults;

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
        _votingResults = new(_candidates.Values);
    }

    public Result AcceptBallot(EncryptedSignedBallot encryptedSignedBallot, ISignatureProvider signatureProvider, IEncryptionProvider encryptionProvider, IObjectToByteArrayTransformer objectToByteArrayTransformer)
    {
        return Result
        .FailIf(VotingCompleted, new Error("Voting is completed."))
        .Bind(() =>
            Result.Try(()
            => objectToByteArrayTransformer.ReverseTransform<SignedBallot>(encryptionProvider.Decrypt(encryptedSignedBallot.EncryptedData, BallotEncryptionKey))
                ?? throw new InvalidOperationException("Value cannot be transformed to signed ballot."),
            e => new Error("Message has wrong format or was incorrectly encrypted.").CausedBy(e)))
        .Bind(sb =>
        {
            var isSignatureAuthentic = signatureProvider.Verify(objectToByteArrayTransformer.Transform(sb.Ballot), sb.Signature, sb.PublicKey);
            if (!isSignatureAuthentic)
            {
                return Result.Fail(new Error("The signature is not authentic."));
            }

            var voterWasFound = _voters.TryGetValue(sb.Ballot.VoterId, out var voter);
            if (!voterWasFound)
            {
                return Result.Fail(new Error("The voter was not found."));
            }

            var signatureBelongsToVoter = voter!.PublicKey == sb.PublicKey;
            if (!signatureBelongsToVoter)
            {
                return Result.Fail(new Error("The ballot was not signed by the voter."));
            }

            return Result.Ok(sb.Ballot);
        })
        .Bind(b =>
        {
            var voterAlreadyAccepted = _acceptedVotersIds.Contains(b.VoterId);
            if (voterAlreadyAccepted)
            {
                return Result.Fail(new Error("Voter's vote has already been accepted."));
            }

            var candidateWasFound = _candidates.TryGetValue(b.CandidateId, out var candidateResults);
            if (!candidateWasFound)
            {
                return Result.Fail(new Error("Candidate was not found."));
            }

            candidateResults!.Votes++;
            _acceptedVotersIds.Add(b.VoterId);

            return Result.Ok();
        });
    }

    public VotingResults CompleteVoting()
    {
        if (!VotingCompleted)
        {
            VotingCompleted = true;
            _votingResults.CompleteVoting();
        }

        return (VotingResults)_votingResults.Clone();
    }
}
