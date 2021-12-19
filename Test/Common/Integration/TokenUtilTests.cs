using System;
using Xunit;
using Xunit.Abstractions;

using Common;

namespace Test.Common.Integration
{
    public sealed class IgnoreOnCiCdFact : FactAttribute
    {
        // https://josephwoodward.co.uk/2019/01/skipping-xunit-tests-based-on-runtime-conditions
        public IgnoreOnCiCdFact()
        {
            if (IsCiCd())
            {
                Skip = "Ignore for CI/CD due to LOCAL_TEST env var not set.";
            }
        }

        private static bool IsCiCd()
        {
            return Environment.GetEnvironmentVariable("LOCAL_TEST") == null;
        }
    }

    /// <summary>
    /// Integration test for base repository
    /// </summary>
    public class TokenUtilTests
    {
        private readonly ITestOutputHelper _output;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenUtilTests"/> class.
        /// </summary>
        /// <param name="output">test output helper.</param>
        public TokenUtilTests(ITestOutputHelper output)
        {
            this._output = output;
        }

        [Fact]
        public void validAuthHeader_null()
        {
            Assert.Equal("TestUser", TokenUtils.validAuthHeader(null));
        }

        [Fact]
        public void validAuthHeader_empty()
        {
            Assert.ThrowsAny<Exception>(() => TokenUtils.validAuthHeader(string.Empty));
        }

        [Fact]
        public void validAuthHeader_invalidFormat()
        {
            Assert.ThrowsAny<Exception>(() => TokenUtils.validAuthHeader("ABC 124"));
        }

        [IgnoreOnCiCdFact]
        public void validAuthHeader_invalidToken()
        {
            Assert.ThrowsAny<Exception>(() => TokenUtils.validAuthHeader("Bearer abc"));
        }
    }
}