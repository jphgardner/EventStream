using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace EventStream.Domain
{
    public class Password
    {
        private string _password;
        private string _salt;

        public Password(string strPassword, string nSalt)
        {
            _password = strPassword;
            _salt = nSalt;
        }

        public static string CreateRandomSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public static bool Validate(string password, string salt, string hash)
        {
            return Resolve(password, salt) == hash;
        }

        public static string Resolve(string password, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(valueBytes);
        }

        public string Resolve()
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                password: _password,
                Encoding.UTF8.GetBytes(_salt),
                KeyDerivationPrf.HMACSHA512,
                10000,
                256 / 8);

            return Convert.ToBase64String(valueBytes);
        }
    }
}