using FluentAssertions;
using TDDLab.Core.InvoiceMgmt;
using Tests.TDDLab.Core.InvoiceMgmt.Factories;
using Xunit;

namespace Tests.TDDLab.Core.InvoiceMgmt
{
    public class RecipientTests
    {
        private const string ValidName = "Dziekan";

        [Fact]
        public void ForValidValues_ShouldSucceedValidation()
        {
            // arrange
            var sut = new Recipient(ValidName, AddressFactory.ValidAddress);

            // act
            var result = sut.IsValid;

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void ForEmptyName_ValidationShouldFailValidation()
        {
            // arrange
            var sut = new Recipient(string.Empty, AddressFactory.ValidAddress);

            // act
            var result = sut.Validate();

            // assert
            result.Should().Contain(Recipient.ValidationRules.Name);
        }

        // !!! This does not pass due to fact that the check is not null secured (a bug probably?) !!!
        [Fact]
        public void ForMissingAddress_ValidationShouldFailValidation()
        {
            // arrange
            var sut = new Recipient(ValidName, null);

            // act
            var result = sut.Validate();

            // assert
            result.Should().Contain(Recipient.ValidationRules.Address);
        }

        [Fact]
        public void ForInvalidAddress_ValidationShouldFailValidation()
        {
            // arrange
            var sut = new Recipient(ValidName, AddressFactory.InvalidAddress);

            // act
            var result = sut.Validate();

            // assert
            result.Should().Contain(Recipient.ValidationRules.Address);
        }
    }
}