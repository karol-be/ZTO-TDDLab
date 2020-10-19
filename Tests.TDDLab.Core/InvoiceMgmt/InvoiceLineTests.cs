using FluentAssertions;
using FluentAssertions.Execution;
using TDDLab.Core.InvoiceMgmt;
using Tests.TDDLab.Core.InvoiceMgmt.Factories;
using Xunit;

namespace Tests.TDDLab.Core.InvoiceMgmt
{
    public class InvoiceLineTests
    {
        private const string ValidProductName = "punkt ECTS";

        private static readonly Money ValidMoney = MoneyFactory.ValidMoney;

        [Fact]
        public void ForValidValues_ShouldSucceedValidation()
        {
            // arrange
            var sut = new InvoiceLine(ValidProductName, ValidMoney);

            // act
            var validationResult = sut.IsValid;

            // assert
            validationResult.Should().BeTrue();
        }

        [Fact]
        public void ForValidValues_ShouldSetCorrectValues()
        {
            // act
            var sut = new InvoiceLine(ValidProductName, ValidMoney);

            // assert
            using (new AssertionScope())
            {
                sut.ProductName.Should().Be(ValidProductName);
                sut.Money.Should().Be(ValidMoney);
            }
        }

        [Fact]
        public void ForEmptyProductName_ShouldFailValidation()
        {
            // arrange
            var sut = new InvoiceLine(string.Empty, ValidMoney);

            // act
            var validationResult = sut.Validate();

            // assert
            validationResult.Should().ContainSingle(InvoiceLine.ValidationRules.ProductName.Name);
        }

        [Fact]
        public void ForNullProductName_ShouldFailValidation()
        {
            // arrange
            var sut = new InvoiceLine(null, ValidMoney);

            // act
            var validationResult = sut.Validate();

            // assert
            validationResult.Should().ContainSingle(InvoiceLine.ValidationRules.ProductName.Name);
        }

        [Fact]
        public void ForInvalidMoney_ShouldFailValidation()
        {
            // arrange
            var sut = new InvoiceLine(ValidProductName, MoneyFactory.InvalidMoney);

            // act
            var validationResult = sut.Validate();

            // assert
            validationResult.Should().ContainSingle(InvoiceLine.ValidationRules.Money.Name);
        }

        [Fact]
        public void ForNullMoney_ShouldFailValidation()
        {
            // arrange
            var sut = new InvoiceLine(ValidProductName, null);

            // act
            var validationResult = sut.Validate();

            // assert
            validationResult.Should().ContainSingle(InvoiceLine.ValidationRules.Money.Name);
        }
    }
}