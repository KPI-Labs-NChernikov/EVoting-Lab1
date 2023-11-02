using Algorithms.Common;
using Application.Abstractions.Cryptography;
using System.Security.Cryptography;

namespace Infrastructure.Cryptography.RSASignature;
public sealed class RSASignatureKeysGenerator : IAsymmetricKeysGenerator
{
    public Keys GenerateKeys()
    {
        using var rsa = RSA.Create();

        return new Keys(rsa.ExportRSAPublicKey(), rsa.ExportRSAPrivateKey());
    }
}
