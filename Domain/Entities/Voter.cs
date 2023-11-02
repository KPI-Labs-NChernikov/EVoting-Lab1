using FluentResults;

namespace Domain.Entities;
public sealed class Voter
{
    public int Id { get; set; }

    public string FullName { get; }

    public ushort Age { get; }

    public bool IsCapable { get; }

    public byte[]? PublicKey { get; set; }

    public bool HasVoted { get; private set; }

    public Voter(string fullName, ushort age, bool isCapable)
    {
        FullName = fullName;
        Age = age;
        IsCapable = isCapable;
    }

    public Result IsAbleToVote()
    {
        if (Age < 18)
        {
            return Result.Fail($"Voter {FullName} must be 18 years old. Currently, they are {Age} years old.");
        }

        if (!IsCapable)
        {
            return Result.Fail($"Voter {FullName} is not capable.");
        }

        if (HasVoted)
        {
            return Result.Fail($"Voter {FullName} has already casted their vote.");
        }

        return Result.Ok();
    }

    public void MarkAsVoted()
    {
        HasVoted = true;
    }
    //}

    //public EncryptedSignedBallot GenerateBallot(int candidateId, byte[] centralElectionCommissionKey, ISignatureProvider signatureProvider, IEncryptionProvider encryptionProvider, IObjectToByteArrayTransformer objectToByteArrayTransformer)
    //{
    //    var ballot = new Ballot(Id, candidateId);

    //    var ballotAsByteArray = objectToByteArrayTransformer.Transform(ballot);
    //    var signedBallot = new SignedBallot(ballot, signatureProvider.Sign(ballotAsByteArray, _privateKey), PublicKey);

    //    var signedBallotAsByteArray = objectToByteArrayTransformer.Transform(signedBallot);
    //    var encryptedSignedBallot = new EncryptedSignedBallot(encryptionProvider.Encrypt(signedBallotAsByteArray, centralElectionCommissionKey));

    //    return encryptedSignedBallot;
    //}
}
