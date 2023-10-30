namespace Models.Ballots;
public class EncryptedSignedBallot
{
    public byte[] EncryptedData { get; }

    public EncryptedSignedBallot(byte[] encryptedData)
    {
        EncryptedData = encryptedData;
    }
}
