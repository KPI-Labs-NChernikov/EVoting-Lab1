using Algorithms.Common;

namespace Application.Abstractions.Cryptography;
public interface IAsymmetricKeysGenerator
{
    Keys GenerateKeys();
}
