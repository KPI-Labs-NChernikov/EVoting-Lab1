using Algorithms.Abstractions;
using System.Security.Cryptography;

namespace Algorithms;
public class RSASignatureProvider : ISignatureProvider
{
    public HashAlgorithmName HashAlgorithmName { get; set; } = HashAlgorithmName.SHA256;
    public RSASignaturePadding Padding { get; set; } = RSASignaturePadding.Pkcs1;

    public Keys GenerateKeys()
    {
        using var rsa = RSA.Create();

        return new Keys(rsa.ExportRSAPublicKey(), rsa.ExportRSAPrivateKey());
    }

    public byte[] Sign(byte[] data, byte[] privateKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(privateKey, out _);

        return rsa.SignData(data, HashAlgorithmName, Padding);
    }

    public bool Verify(byte[] data, byte[] signature, byte[] publicKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportRSAPublicKey(publicKey, out _);

        return rsa.VerifyData(data, signature, HashAlgorithmName, Padding);
    }
}
