namespace Modelling.Models.Ballots;
public sealed class EncryptedSignedBallot
{
    public byte[] EncryptedData { get; }

    public EncryptedSignedBallot(byte[] encryptedData)
    {
        EncryptedData = encryptedData;
    }
}
