namespace Domain.Entities;
public class CentralElectionCommission
{
    public byte[] BallotEncryptionKey { get; }

    public CentralElectionCommission(byte[] ballotEncryptionKey)
    {
        BallotEncryptionKey = ballotEncryptionKey;
    }
}
