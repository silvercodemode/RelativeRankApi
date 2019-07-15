using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using RelativeRank.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RelativeRank.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private KeyDerivationPrf _pbkdf2prf = KeyDerivationPrf.HMACSHA1;
        private int _pbkdf2IterationCount = 1000;
        private int _pbkdf2SubkeyLength = 256 / 8;
        private int _saltSize = 128 / 8;

        // this just copies the HashPasswordV2 method from Identity
        // https://github.com/aspnet/Identity/blob/rel/2.0.0/src/Microsoft.Extensions.Identity.Core/PasswordHasher.cs
        public string Hash(string stringToHash)
        {
            byte[] salt = new byte[_saltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var subkey = KeyDerivation.Pbkdf2(
                password: stringToHash,
                salt: salt,
                prf: _pbkdf2prf,
                iterationCount: _pbkdf2IterationCount,
                numBytesRequested: _pbkdf2SubkeyLength);

            var outputBytes = new byte[1 + _saltSize + _pbkdf2SubkeyLength];
            outputBytes[0] = 0x00;
            Buffer.BlockCopy(salt, 0, outputBytes, 1, _saltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + _saltSize, _pbkdf2SubkeyLength);

            return Convert.ToBase64String(outputBytes);
        }

        // this just copies the VerifyHashedPasswordV2 method from Identity
        // https://github.com/aspnet/Identity/blob/rel/2.0.0/src/Microsoft.Extensions.Identity.Core/PasswordHasher.cs
        public bool ValidateHash(string hash, string nonHashedString)
        {
            var hashedStringBytes = Convert.FromBase64String(hash);

            if (hashedStringBytes.Length != 1 + _saltSize + _pbkdf2SubkeyLength)
            {
                return false;
            }

            var salt = new byte[_saltSize];
            Buffer.BlockCopy(hashedStringBytes, 1, salt, 0, salt.Length);

            var expectedSubkey = new byte[_pbkdf2SubkeyLength];
            Buffer.BlockCopy(hashedStringBytes, 1 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            var actualSubkey = KeyDerivation.Pbkdf2(
                nonHashedString, 
                salt,
                KeyDerivationPrf.HMACSHA1,
                _pbkdf2IterationCount,
                _pbkdf2SubkeyLength);

            return ByteArraysEqual(expectedSubkey, actualSubkey);
        }

        // direct copy from:
        // https://github.com/aspnet/Identity/blob/rel/2.0.0/src/Microsoft.Extensions.Identity.Core/PasswordHasher.cs
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }
            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }

        public string GenerateJwt(string claimName, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, claimName)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
