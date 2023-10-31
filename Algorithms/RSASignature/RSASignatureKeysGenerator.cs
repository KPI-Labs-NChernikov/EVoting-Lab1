using Algorithms.Abstractions;
using Algorithms.Common;
using System.Security.Cryptography;

namespace Algorithms.RSASignature;
public sealed class RSASignatureKeysGenerator : IAsymmetricKeysGenerator
{
    public Keys GenerateKeys()
    {
        using var rsa = RSA.Create();

        return new Keys(rsa.ExportRSAPublicKey(), rsa.ExportRSAPrivateKey());
    }
}
