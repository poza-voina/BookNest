using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services;

public interface IPasswordManager
{
    string GetPasswordHash(string password, out byte[] salt);
    bool VerifyPassword(string password, string hashPassword, byte[] salt);
}


public class PasswordManager : IPasswordManager
{
    public const string AllowedPasswordChars =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#$%&+-~";

    private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA512;

    private const int HashingKeySize = 64;
    private const int HashingIterationsCount = 350000;
    
    public string GetPasswordHash(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(16);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            HashingIterationsCount,
            _hashAlgorithmName,
            HashingKeySize
        );

        return Convert.ToHexString(hash);
    }

    public bool VerifyPassword(string password, string hashPassword, byte[] salt)
    {
        return Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            HashingIterationsCount,
            _hashAlgorithmName,
            HashingKeySize).SequenceEqual(Convert.FromHexString(hashPassword));
    }
}