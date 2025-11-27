using System.Security.Cryptography;
using System.Text;

namespace Codebuddy.Infrastructure.Identity;

public class PasswordHasher
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 10000;
    private const char Separator = '.';

    public string Hash(string password)
    {
        using var rng = RandomNumberGenerator.Create();
        var salt = new byte[SaltSize];
        rng.GetBytes(salt);

        var key = GetKey(password, salt);
        return string.Join(Separator, Convert.ToBase64String(salt), Convert.ToBase64String(key));
    }

    public bool Check(string hash, string password)
    {
        var parts = hash.Split(Separator);
        if (parts.Length != 2)
        {
            return false;
        }

        var salt = Convert.FromBase64String(parts[0]);
        var key = Convert.FromBase64String(parts[1]);
        var incomingKey = GetKey(password, salt);

        return CryptographicOperations.FixedTimeEquals(incomingKey, key);
    }

    private static byte[] GetKey(string password, byte[] salt)
    {
        return Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, KeySize);
    }
}
