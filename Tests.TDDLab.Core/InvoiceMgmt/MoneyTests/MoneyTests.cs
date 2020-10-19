using FluentAssertions;
using FluentAssertions.Execution;
using TDDLab.Core.InvoiceMgmt;
using Xunit;

namespace Tests.TDDLab.Core.InvoiceMgmt.MoneyTests
{
    public class MoneyTests
    {
        private const ulong CorrectAmount = 20U;
        private const string CorrectCurrency = "ECTS";

        #region ZERO

        [Fact]
        public void Always_ZeroShouldHaveZeroAmount()
        {
            // act
            var result = Money.ZERO;

            // assert
            result.Amount.Should().Be(0U);
        }

        #endregion

        #region ToString()

        [Fact]
        public void Always_ShouldCorrectlyConvertToString()
        {
            // arrange
            var sut = new Money(17, "ECTS");

            // act
            var stringified = sut.ToString();

            // assert
            stringified.Should().Be("17ECTS");
        }

        #endregion

        #region ctor

        [Fact]
        public void ForCorrectAmount_ShouldCorrectlySet()
        {
            // act
            var sut = new Money(CorrectAmount);

            // assert
            sut.Amount.Should().Be(CorrectAmount);
        }

        [Fact]
        public void ForCorrectValues_ShouldCorrectlySetValues()
        {
            // act
            var sut = new Money(CorrectAmount, CorrectCurrency);

            // assert
            using (new AssertionScope())
            {
                sut.Amount.Should().Be(CorrectAmount);
                sut.Currency.Should().Be(CorrectCurrency);
            }
        }

        [Fact]
        public void ForNonProvidedCurrency_ShouldSetDefaultCurrency()
        {
            // act
            var sut = new Money(CorrectAmount);

            // assert
            sut.Currency.Should().Be(Money.DefaultCurrency);
        }

        #endregion

        #region operator-

        [Fact]
        public void ForGivenAmountOfMoneySubtracted_ShouldDecreaseAmount()
        {
            // arrange
            var sut = new Money(20U);
            var subtrahend = new Money(10U);

            // act
            var result = sut - subtrahend;

            // assert
            result.Amount.Should().Be(10);
        }

        [Fact]
        public void ForMoneySubtractedInOtherCurrency_ShouldNotChangeTheMinuendCurrency()
        {
            // arrange
            var sut = new Money(20U);
            var subtrahend = new Money(10U, "ECTS");
            var originalCurrency = sut.Currency;

            // act
            var result = sut - subtrahend;

            // assert
            result.Currency.Should().Be(originalCurrency);
        }

        #endregion

        #region operator+

        [Fact]
        public void ForGivenAmountOfMoneyAdded_ShouldSumAmounts()
        {
            // arrange
            var sut = new Money(20U);
            var addend = new Money(10U);

            // act
            var result = sut + addend;

            // assert
            result.Amount.Should().Be(30);
        }

        [Fact]
        public void ForMoneyAddedInOtherCurrency_ShouldNotChangeTheMinuendCurrency()
        {
            // arrange
            var sut = new Money(20U);
            var addend = new Money(10U, "ECTS");
            var originalCurrency = sut.Currency;

            // act
            var result = sut + addend;

            // assert
            result.Currency.Should().Be(originalCurrency);
        }

        #endregion

        #region equality_operator==

        [Fact]
        public void ForComparisonWithSameAmountAndCurrency_ShouldReturnTrue()
        {
            // arrange
            var sut = new Money(0U, "ECTS");
            var other = new Money(0U, "ECTS");


            // act
            var operatorResult = sut == other;
            var equalsResult = sut.Equals(other);

            // assert
            operatorResult.Should().Be(equalsResult).And.BeTrue();
        }

        [Fact]
        public void ForComparisonWithSameAmountOtherCurrency_ShouldReturnFalse()
        {
            // arrange
            var sut = new Money(0U, "ECTS");
            var other = new Money(0U, "PLN");


            // act
            var operatorResult = sut == other;
            var equalsResult = sut.Equals(other);

            // assert
            operatorResult.Should().Be(equalsResult).And.BeFalse();
        }

        [Fact]
        public void ForComparisonWithOtherAmountSameCurrency_ShouldReturnFalse()
        {
            // arrange
            var sut = new Money(0U, "ECTS");
            var other = new Money(20U, "ECTS");


            // act
            var operatorResult = sut == other;
            var equalsResult = sut.Equals(other);

            // assert
            operatorResult.Should().Be(equalsResult).And.BeFalse();
        }

        [Fact]
        public void ForComparisonWithOtherAmountOtherCurrency_ShouldReturnFalse()
        {
            // arrange
            var sut = new Money(0U, "ECTS");
            var other = new Money(20U, "PLN");


            // act
            var operatorResult = sut == other;
            var equalsResult = sut.Equals(other);

            // assert
            operatorResult.Should().Be(equalsResult).And.BeFalse();
        }

        #endregion
    }
}