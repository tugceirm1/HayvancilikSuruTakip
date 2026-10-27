
using System;
using System.Security.Cryptography;

namespace HayvancilikSuruTakip.Security
{
    public static class PasswordHelper
    {
        public const int Iterations = 210000;

        public static void HashPassword(
            string password,
            out byte[] hash,
            out byte[] salt)
        {
            salt = new byte[16];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256))
            {
                hash = pbkdf2.GetBytes(32);
            }
        }

        public static bool VerifyPassword(
            string password,
            byte[] expectedHash,
            byte[] salt,
            int iterations)
        {
            if (expectedHash == null ||
                expectedHash.Length != 32 ||
                salt == null ||
                salt.Length != 16 ||
                iterations < 100000)
            {
                return false;
            }

            byte[] actualHash;

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256))
            {
                actualHash = pbkdf2.GetBytes(32);
            }

            int difference = 0;

            for (int i = 0; i < 32; i++)
            {
                difference |= actualHash[i] ^ expectedHash[i];
            }

            return difference == 0;
        }
    }
}
