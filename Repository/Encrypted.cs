using System;
using System.Security.Cryptography;

namespace WebAPI.Repository
{
 
public class Encrypted
    {
        private const int SaltSize = 16; // You can adjust the salt size based on your security requirements
        private const int HashSize = 20; // Bcrypt produces a 20-byte hash

        public static string HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            // Hash the password with the salt using bcrypt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Combine the salt and hash for storage
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            // Convert the byte array to a base64-encoded string for storage
            string hashedPassword = Convert.ToBase64String(hashBytes);
            return hashedPassword;
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            // Convert the stored hash from base64 to a byte array
            byte[] storedHashBytes = Convert.FromBase64String(storedHash);

            // Extract the salt from the stored hash
            byte[] salt = new byte[SaltSize];
            Array.Copy(storedHashBytes, 0, salt, 0, SaltSize);

            // Hash the entered password with the stored salt
            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Compare the computed hash with the stored hash
            for (int i = 0; i < HashSize; i++)
            {
                if (storedHashBytes[i + SaltSize] != hash[i])
                {
                    return false; // Passwords don't match
                }
            }

            return true; // Passwords match
        }
    }
}
