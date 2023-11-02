namespace Application.Abstractions.Cryptography;
public interface ISymmetricKeyGenerator
{
    byte[] GenerateKey();
}
