using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace AuthService.Domain.Utils
{
    public static class PasswordExtensions
    {
        /// <summary>
        /// Encrypt a string
        /// </summary>
        /// <param name="password">String to encrypt</param>
        /// <returns>Encrypted string</returns>
        public static string PasswordEncrypt(this string password, string saltKey = null)
        {
            if (!string.IsNullOrEmpty(password))
            {
                // Tem que ser 8 caracteres
                saltKey ??= GenerateSaltKey(8);

                if (saltKey.Length != 8)
                    throw new Exception("SaltKey requires 8 chars.");

                var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: Encoding.ASCII.GetBytes(saltKey),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 2561 / 8));

                return hash;
            }

            return null;
        }

        /// <summary>
        /// Uses concatenated then SubStringed GUIDs to get a random string of the
        /// desired length. Relies on the randomness of the GUID generation algorithm.
        /// </summary>
        /// <param name="stringLength">Length of string to return</param>
        /// <returns>Random string of <paramref name="stringLength"/> characters</returns>
        private static string GenerateSaltKey(int stringLength)
        {
            StringBuilder sb = new ();
            int numGuidsToConcat = (((stringLength - 1) / 32) + 1);
            for (int i = 1; i <= numGuidsToConcat; i++)
            {
                sb.Append(Guid.NewGuid().ToString("N"));
            }

            return sb.ToString(0, stringLength);
        }
    }
}
