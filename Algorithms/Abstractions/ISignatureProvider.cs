namespace Algorithms.Abstractions;
public interface ISignatureProvider
{
    byte[] Sign(byte[] data, byte[] privateKey);
    bool Verify(byte[] data, byte[] signature, byte[] publicKey);
}
