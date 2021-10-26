using Xunit;
using Intive.ConfR.Domain.ValueObjects;
using Intive.ConfR.Domain.Exceptions;

namespace Intive.ConfR.Domain.Tests.ValueObjects
{
    public class EMailAddressTests
    {
        [Fact]
        public void ShouldHaveCorrectNameAndDomain()
        {
            var address = EMailAddress.For("test@testing.com");

            Assert.Equal("test", address.Name);
            Assert.Equal("testing.com", address.Domain);
        }

        [Fact]
        public void ToStringReturnsCorrectFormat()
        {
            const string value = "test@testing.com";

            var address = EMailAddress.For(value);

            Assert.Equal(value, address.ToString());
        }

        [Fact]
        public void ExplicitConversionFromStringSetsDomainAndName()
        {
            var address = (EMailAddress)"test@testing.com";

            Assert.Equal("test", address.Name);
            Assert.Equal("testing.com", address.Domain);
        }

        [Fact]
        public void ShouldThrowAdAccountInvalidExceptionForInvalidEMAilAddress()
        {
            Assert.Throws<EMailAddressInvalidException>(() => (EMailAddress)"testtesting.com");
        }
    }
}
