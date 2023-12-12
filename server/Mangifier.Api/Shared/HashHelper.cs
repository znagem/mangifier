using System.Security.Cryptography;

namespace Mangifier.Api.Shared;

public static class HashHelper
{
    /// <summary>Generate byte array use for password hashing.</summary>
    /// <param name="saltByteSize"></param>
    public static byte[] SaltByte(int saltByteSize = 24)
    {
        var data = new byte[saltByteSize];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(data);
        return data;
    }

    /// <summary>Generate string use for password hashing.</summary>
    /// <param name="password">The password literal</param>
    /// <param name="salt">The salt bytes generated</param>
    /// <param name="iterations">Iteration count</param>
    /// <param name="hashByteSize"></param>
    /// <returns></returns>
    public static byte[] ComputeHash(
        string password,
        byte[] salt,
        int iterations = 10101,
        int hashByteSize = 24)
    {
        using var bytes = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA512);
        return bytes.GetBytes(hashByteSize);
    }
}