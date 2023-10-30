namespace Domain.Entities.Ballots;
public class EncryptedSignedBallot
{
    public byte[] EncryptedData { get; }

    public EncryptedSignedBallot(byte[] encryptedData)
    {
        EncryptedData = encryptedData;
    }
}
