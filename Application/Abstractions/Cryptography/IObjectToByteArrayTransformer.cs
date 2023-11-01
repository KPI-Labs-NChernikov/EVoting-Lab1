namespace Application.Abstractions.Cryptography;
public interface IObjectToByteArrayTransformer
{
    byte[] Transform(object obj);

    T? ReverseTransform<T>(byte[] data); 
}
