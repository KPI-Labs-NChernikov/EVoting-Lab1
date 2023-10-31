using Algorithms.Abstractions;
using System.Security.Cryptography;

namespace Algorithms.RSASignature;
public sealed class RSASignatureProvider : ISignatureProvider
{
    public HashAlgorithmName HashAlgorithmName
    {
        get
        {
            return _hashAlgorithmName;
        }
        set
        {
            if (string.IsNullOrEmpty(value.Name))
            {
                throw new ArgumentException("The name of hash alorithm must not be empty", nameof(value));
            }

            _hashAlgorithmName = value;
        }
    }
    private HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;

    public RSASignaturePadding Padding
    {
        get
        {
            return _padding;
        }
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _padding = value;
        }
    }
    private RSASignaturePadding _padding = RSASignaturePadding.Pkcs1;

    public byte[] Sign(byte[] data, byte[] privateKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(privateKey, out _);

        return rsa.SignData(data, _hashAlgorithmName, _padding);
    }

    public bool Verify(byte[] data, byte[] signature, byte[] publicKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportRSAPublicKey(publicKey, out _);

        return rsa.VerifyData(data, signature, _hashAlgorithmName, _padding);
    }
}
