using FluentAssertions;
using TDDLab.Core.InvoiceMgmt;
using Xunit;

namespace Tests.TDDLab.Core.InvoiceMgmt.MoneyTests
{
    public class MoneyValidationRulesTests
    {
        private const ulong ValidAmount = 0U;

        [Fact]
        public void ForValidValues_ShouldSucceedValidation()
        {
            // arrange
            var sut = new Money(ValidAmount, "ECTS");

            // act
            var result = sut.IsValid;

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void ForEmptyCurrency_ShouldFailValidation()
        {
            // arrange
            var sut = new Money(ValidAmount, string.Empty);

            // act
            var result = sut.Validate();

            // assert
            result.Should().Contain(Money.ValidationRules.Currency);
        }

        [Fact]
        public void ForNullCurrency_ShouldFailValidation()
        {
            // arrange
            var sut = new Money(ValidAmount, null);

            // act
            var result = sut.Validate();

            // assert
            result.Should().Contain(Money.ValidationRules.Currency);
        }
    }
}