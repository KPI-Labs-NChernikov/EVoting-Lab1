using FluentResults;

namespace Domain.Entities;
public sealed class Voting
{
    public IReadOnlyCollection<Candidate> Candidates { get; }
    public DateTime? CompletedAt { get; private set; }
    public bool IsCompleted { get; private set; }

    public byte[] CentralElectionCommissionKey { get; }

    public Voting(IReadOnlyCollection<Candidate> candidates, byte[] centralElectionCommissionKey)
    {
        Candidates = candidates;
        CentralElectionCommissionKey = centralElectionCommissionKey;
    }

    public Result AcceptBallot(Ballot ballot, Voter voter)
    {
        if (IsCompleted)
        {
            return Result.Fail("Voting is completed.");
        }

        var voterIsAble = voter.IsAbleToVote();
        if (!voterIsAble.IsSuccess)
        {
            return voterIsAble;
        }

        ballot.Candidate.AddVote();
        voter.MarkAsVoted();

        return Result.Ok();
    }

    public Result CompleteVoting()
    {
        if (IsCompleted)
        {
            return Result.Fail("Voting is completed.");
        }

        IsCompleted = true;
        CompletedAt = DateTime.Now;
        return Result.Ok();
    }
}
//Result
//        .FailIf(VotingCompleted, new Error("Voting is completed."))
//        .Bind(() =>
//            Result.Try(()
//            => objectToByteArrayTransformer.ReverseTransform<SignedBallot>(encryptionProvider.Decrypt(encryptedSignedBallot.EncryptedData, BallotEncryptionKey))
//                ?? throw new InvalidOperationException("Value cannot be transformed to signed ballot."),
//            e => new Error("Message has wrong format or was incorrectly encrypted.").CausedBy(e)))
//        .Bind(sb =>
//        {
//    var signatureIsAuthentic = signatureProvider.Verify(objectToByteArrayTransformer.Transform(sb.Ballot), sb.Signature, sb.PublicKey);
//    if (!signatureIsAuthentic)
//    {
//        return Result.Fail(new Error("The signature is not authentic."));
//    }

//    var voterWasFound = _voters.TryGetValue(sb.Ballot.VoterId, out var voter);
//    if (!voterWasFound)
//    {
//        return Result.Fail(new Error("The voter was not found."));
//    }

//    var signatureBelongsToVoter = voter!.PublicKey == sb.PublicKey;
//    if (!signatureBelongsToVoter)
//    {
//        return Result.Fail(new Error("The ballot was not signed by the voter."));
//    }

//    var voterAbility = voter.IsAbleToVote();
//    if (!voterAbility.IsSuccess)
//    {
//        return voterAbility;
//    }

//    return Result.Ok(sb.Ballot);
//})
//        .Bind(b =>
//        {
//    var voterAlreadyAccepted = _acceptedVotersIds.Contains(b.VoterId);
//    if (voterAlreadyAccepted)
//    {
//        return Result.Fail(new Error("Voter's vote has already been accepted."));
//    }

//    var candidateWasFound = _candidates.TryGetValue(b.CandidateId, out var candidateResults);
//    if (!candidateWasFound)
//    {
//        return Result.Fail(new Error("Candidate was not found."));
//    }

//    candidateResults!.Votes++;
//    _acceptedVotersIds.Add(b.VoterId);

//    return Result.Ok();
//});