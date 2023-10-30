namespace Algorithms.Abstractions;
public interface ISignatureProvider
{
    Keys GenerateKeys();
    byte[] Sign(byte[] data, byte[] privateKey);
    bool Verify(byte[] data, byte[] signature, byte[] publicKey);
}
