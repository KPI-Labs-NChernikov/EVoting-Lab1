namespace Application.Abstractions.Cryptography;
public interface IEncryptionProvider
{
    byte[] Encrypt(byte[] data, byte[] key);
    byte[] Decrypt(byte[] data, byte[] key);
}
