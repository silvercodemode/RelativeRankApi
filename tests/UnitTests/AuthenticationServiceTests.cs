using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using RelativeRank.Entities;
using RelativeRank.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace RelativeRankTests.UnitTests
{
    public class AuthenticationServiceTests
    {
        [Fact]
        public void HashedStringShouldBeCorrectlyVerifiedByValidateHashMethod()
        {
            var authenticationService = new AuthenticationService();

            var originalString = "testString";
            var hash = authenticationService.Hash(originalString);

            Assert.True(authenticationService.ValidateHash(hash, originalString));
        }

        [Fact]
        public void GenerateJwtShouldGenerateValidToken()
        {
            var secret = "a string that is longer than 32 characters yay";
            var token = new AuthenticationService().GenerateJwt("claim", secret);

            var key = Encoding.ASCII.GetBytes(secret);
            var securityKey = new SymmetricSecurityKey(key);

            SecurityToken securityToken;
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                RequireExpirationTime = true,
                ValidateLifetime = true,
                IssuerSigningKey = securityKey,
                RequireSignedTokens = true,
                ValidateAudience = false,
                ValidateIssuer = false
            }, out securityToken);
        }
    }
}
