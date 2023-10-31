using Algorithms.Abstractions;
using FluentResults;
using Models.Ballots;

namespace Models;
public class Voter
{
    public int Id { get; }

    public string FullName { get; }

    public ushort Age { get; }

    public bool IsCapable { get; }

    public byte[] PublicKey { get; }

    private readonly byte[] _privateKey;

    public Voter(int id, string fullName, ushort age, bool isCapable, IAsymmetricKeysGenerator keysGenerator)
    {
        Id = id;
        FullName = fullName;
        Age = age;
        IsCapable = isCapable;

        (PublicKey, _privateKey) = keysGenerator.GenerateKeys();
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

        return Result.Ok();
    }

    public EncryptedSignedBallot GenerateBallot(int candidateId, byte[] centralElectionCommissionKey, ISignatureProvider signatureProvider, IEncryptionProvider encryptionProvider, IObjectToByteArrayTransformer objectToByteArrayTransformer)
    {
        var ballot = new Ballot(Id, candidateId);

        var ballotAsByteArray = objectToByteArrayTransformer.Transform(ballot);
        var signedBallot = new SignedBallot(ballot, signatureProvider.Sign(ballotAsByteArray, _privateKey), PublicKey);

        var signedBallotAsByteArray = objectToByteArrayTransformer.Transform(signedBallot);
        var encryptedSignedBallot = new EncryptedSignedBallot(encryptionProvider.Encrypt(signedBallotAsByteArray, centralElectionCommissionKey));

        return encryptedSignedBallot;
    }
}
