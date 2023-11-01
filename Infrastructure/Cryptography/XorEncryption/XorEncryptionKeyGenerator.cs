using Application.Abstractions.Cryptography;
using System.Security.Cryptography;

namespace Infrastructure.Cryptography.XorEncryption;
public sealed class XorEncryptionKeyGenerator : ISymmetricKeyGenerator
{
    public int KeySize
    {
        get
        {
            return _keySize;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "They key size cannot be less than or equal to 0.");
            }

            _keySize = value;
        }
    }
    private int _keySize = 256;

    public byte[] GenerateKey() => RandomNumberGenerator.GetBytes(_keySize);
}
