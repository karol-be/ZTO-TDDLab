using FluentAssertions;
using FluentAssertions.Execution;
using TDDLab.Core.InvoiceMgmt;
using Xunit;

namespace Tests.TDDLab.Core.InvoiceMgmt.DomainExtensions
{
    public class ToCurrencyTests
    {
        private readonly Money _sut = new Money(20);

        [Fact]
        public void Always_ShouldReturnMoneyWithSameAmount()
        {
            // act
            var currencyMoney = _sut.ToCurrency(string.Empty);

            // assert
            currencyMoney.Amount.Should().Be(_sut.Amount);
        }

        [Fact]
        public void Always_ShouldReturnMoneyWithNewCurrency()
        {
            // arrange
            var newCurrency = "Student Coin";

            // act
            var currencyMoney = _sut.ToCurrency(newCurrency);

            // assert
            currencyMoney.Currency.Should().Be(newCurrency);
        }

        [Fact]
        public void Always_ShouldNotAlterGivenMoney()
        {
            // arrange
            var originalCurrency = _sut.Currency;
            var originalAmount = _sut.Amount;

            // act
            var _ = _sut.ToCurrency(string.Empty);

            // assert
            using (new AssertionScope())
            {
                _sut.Currency.Should().Be(originalCurrency);
                _sut.Amount.Should().Be(originalAmount);
            }
        }
    }
}