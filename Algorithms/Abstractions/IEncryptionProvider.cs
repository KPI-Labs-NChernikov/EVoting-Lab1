namespace Algorithms.Abstractions;
public interface IEncryptionProvider
{
    byte[] Encrypt(byte[] data, byte[] publicKey);
    byte[] Decrypt(byte[] data, byte[] privateKey);
}
