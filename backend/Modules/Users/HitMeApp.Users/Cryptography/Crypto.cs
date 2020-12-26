using System;
using System.Security.Cryptography;

namespace HitMeApp.Users.Cryptography
{
    internal static class Crypto
    {
        private const int Version = 1;
        private const int IterationCount = 50000;
        private const int SubkeyLength = 32;
        private const int SaltSize = 16;
        private static readonly HashAlgorithmName s_algorithmName = HashAlgorithmName.SHA256;

        public static string Hash(string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            byte[] salt;
            byte[] bytes;
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(text, SaltSize, IterationCount, s_algorithmName))
            {
                salt = rfc2898DeriveBytes.Salt;
                bytes = rfc2898DeriveBytes.GetBytes(SubkeyLength);
            }

            var inArray = new byte[1 + SaltSize + SubkeyLength];
            inArray[0] = Version;
            Buffer.BlockCopy(salt, 0, inArray, 1, SaltSize);
            Buffer.BlockCopy(bytes, 0, inArray, 1 + SaltSize, SubkeyLength);

            return Convert.ToBase64String(inArray);
        }

        public static bool IsHashValid(string hash, string text)
        {
            if (hash == null)
                throw new ArgumentNullException(nameof(hash));
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            var numArray = Convert.FromBase64String(hash);
            if (numArray.Length < 1)
                return false;

            int version = numArray[0];
            if (version > Version)
                return false;

            var salt = new byte[SaltSize];
            Buffer.BlockCopy(numArray, 1, salt, 0, SaltSize);
            var a = new byte[SubkeyLength];
            Buffer.BlockCopy(numArray, 1 + SaltSize, a, 0, SubkeyLength);
            byte[] bytes;
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(text, salt, IterationCount, s_algorithmName))
            {
                bytes = rfc2898DeriveBytes.GetBytes(SubkeyLength);
            }

            if (!CryptographicOperations.FixedTimeEquals(a, bytes))
                return false;

            return true;
        }
    }
}
