using Infrastructure.Cryptography.Common;

namespace Application.Abstractions.Cryptography;
public interface IAsymmetricKeysGenerator
{
    Keys GenerateKeys();
}
