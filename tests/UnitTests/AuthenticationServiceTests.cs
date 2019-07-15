using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using RelativeRank.Entities;
using RelativeRank.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
    }
}
