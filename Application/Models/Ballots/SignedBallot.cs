namespace Application.Models.Ballots;
public sealed class SignedBallot
{
    public Ballot Ballot { get; }

    public byte[] Signature { get; }

    public byte[] PublicKey { get; }

    public SignedBallot(Ballot ballot, byte[] signature, byte[] publicKey)
    {
        Ballot = ballot;
        Signature = signature;
        PublicKey = publicKey;
    }
}
