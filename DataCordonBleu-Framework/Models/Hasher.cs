using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace DataCordonBleu_Framework.Models {
    //Reference: Jon Holmes's Code
    public static class Hasher {

        ////https://monkelite.com/how-to-hash-password-in-asp-net-core/
        //public static string HashPass(string pass, string salt) {
        //    byte[] hash = KeyDerivation.Pbkdf2(pass, Encoding.UTF8.GetBytes(salt), KeyDerivationPrf.HMACSHA256, 1000, 256 / 8);
        //    return Convert.ToBase64String(hash);
        //}

        /// <summary>
        /// Randomly genterates salt
        /// </summary>
        /// <returns>String containing the salt</returns>
        public static string GetSalt() {
            byte[] randonBytes = new byte[128 / 8];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(randonBytes);
            return Convert.ToBase64String(randonBytes);
        }

        /// <summary>
        /// Generates a key of random characters
        /// </summary>
        /// <returns>String of random characters</returns>
        public static string GetRandKey() {
            string newKey;
            do {
                newKey = GetSalt().Substring(0, 8);
            } while (Contains(newKey, @"+/:\%10oOiILl"));
            return newKey.ToUpper();
        }

        //Source: Jon Holmes
        private static bool Contains(string hayStack, string needles) {
            bool foundIt = false;
            foreach (char ch in needles) {
                if (hayStack.Contains(ch)) {
                    foundIt = true;
                }
            }
            return foundIt;

        }
    }
}
