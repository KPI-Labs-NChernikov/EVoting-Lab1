using Algorithms.Abstractions;
using FluentResults;
using Domain.Models.Ballots;

namespace Domain.Models;
public sealed class CentralElectionCommission
{
    private readonly Dictionary<int, CandidateVotingResults> _candidates = new();

    private readonly Dictionary<int, Voter> _voters = new();

    private readonly HashSet<int> _acceptedVotersIds = new();

    public VotingResults VotingResults { get; }

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
        VotingResults = new(_candidates.Values);
    }

    public Result AcceptBallot(EncryptedSignedBallot encryptedSignedBallot, ISignatureProvider signatureProvider, IEncryptionProvider encryptionProvider, IObjectToByteArrayTransformer objectToByteArrayTransformer)
    {
        return Result.Ok(encryptedSignedBallot)
        .Bind(esb => DecryptSignedBallot(esb, encryptionProvider, objectToByteArrayTransformer))
        .Bind(sb => VerifySignature(sb, signatureProvider, objectToByteArrayTransformer))
        .Bind(sb => VerifyVoter(sb))
        .Bind(sb => VerifyCandidate(sb).Map(sb => sb.Ballot))
        .Bind(b => AddVote(b));
    }

    private Result<SignedBallot> DecryptSignedBallot(EncryptedSignedBallot encryptedSignedBallot, IEncryptionProvider encryptionProvider, IObjectToByteArrayTransformer objectToByteArrayTransformer)
    {
        return Result.Try(()
            => objectToByteArrayTransformer.ReverseTransform<SignedBallot>(encryptionProvider.Decrypt(encryptedSignedBallot.EncryptedData, BallotEncryptionKey))
                ?? throw new InvalidOperationException("Value cannot be transformed to signed ballot."),
            e => new Error("Message has wrong format or was incorrectly encrypted.").CausedBy(e));
    }

    private static Result<SignedBallot> VerifySignature(SignedBallot signedBallot, ISignatureProvider signatureProvider, IObjectToByteArrayTransformer objectToByteArrayTransformer)
    {
        var signatureIsAuthentic = signatureProvider.Verify(objectToByteArrayTransformer.Transform(signedBallot.Ballot), signedBallot.Signature, signedBallot.PublicKey);

        if (!signatureIsAuthentic)
        {
            return Result.Fail(new Error("The signature is not authentic."));
        }

        return Result.Ok(signedBallot);
    }

    private Result<SignedBallot> VerifyVoter(SignedBallot signedBallot)
    {
        var voterWasFound = _voters.TryGetValue(signedBallot.Ballot.VoterId, out var voter);
        if (!voterWasFound)
        {
            return Result.Fail(new Error("The voter was not found."));
        }

        var signatureBelongsToVoter = voter!.PublicKey == signedBallot.PublicKey;
        if (!signatureBelongsToVoter)
        {
            return Result.Fail(new Error("The ballot was not signed by the voter."));
        }

        var voterAbility = voter.IsAbleToVote();
        if (!voterAbility.IsSuccess)
        {
            return voterAbility;
        }

        return Result.Ok(signedBallot);
    }

    private Result<SignedBallot> VerifyCandidate(SignedBallot signedBallot)
    {
        var ballot = signedBallot.Ballot;

        var voterAlreadyAccepted = _acceptedVotersIds.Contains(ballot.VoterId);
        if (voterAlreadyAccepted)
        {
            return Result.Fail(new Error("Voter's vote has already been accepted."));
        }

        var candidateWasFound = _candidates.TryGetValue(ballot.CandidateId, out var candidateResults);
        if (!candidateWasFound)
        {
            return Result.Fail(new Error("Candidate was not found."));
        }

        return Result.Ok(signedBallot);
    }

    private Result AddVote(Ballot ballot)
    {
        _candidates[ballot.CandidateId]!.Votes++;
        _acceptedVotersIds.Add(ballot.VoterId);

        return Result.Ok();
    }
}
