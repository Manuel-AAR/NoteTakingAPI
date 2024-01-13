using System.Security.Cryptography;
using System.Text;

namespace backend.Services;

public class HashingService
{
    public static string Hash256(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) throw new Exception("no provided input");
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = SHA256.HashData(inputBytes);

        StringBuilder hashedPwd = new();
        foreach (byte b in hashBytes)
        {
            hashedPwd.Append(b.ToString("x2")); // Convert byte to hexadecimal string
        }
        return hashedPwd.ToString();
    }
}