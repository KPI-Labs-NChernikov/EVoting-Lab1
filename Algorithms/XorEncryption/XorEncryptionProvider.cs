using Algorithms.Abstractions;

namespace Algorithms.XorEncryption;
public class XorEncryptionProvider : IEncryptionProvider
{
    public byte[] Encrypt(byte[] data, byte[] key)
    {
        var encrypted = new byte[data.Length];

        for (var i = 0; i < encrypted.Length; i++)
        {
            encrypted[i] = (byte)(data[i] ^ key[i % key.Length]);
        }

        return encrypted;
    }

    public byte[] Decrypt(byte[] data, byte[] key) => Encrypt(data, key);
}
