namespace Domain.Entities;
public sealed class Voting
{
    public IReadOnlyCollection<Candidate> Candidates { get; }

    public byte[] CentralElectionCommissionKey { get; }

    public Voting(IReadOnlyCollection<Candidate> candidates, byte[] centralElectionCommissionKey)
    {
        Candidates = candidates;
        CentralElectionCommissionKey = centralElectionCommissionKey;
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