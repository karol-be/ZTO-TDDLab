using System;
using FluentAssertions;
using TDDLab.Core.InvoiceMgmt;
using Xunit;

namespace Tests.TDDLab.Core.DomainExtensions
{
    public class ToCurrencyTests
    {
        private readonly Money _sut;
        
        public ToCurrencyTests()
        {
            _sut = new Money(20);
        }
        
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
            _sut.ToCurrency(string.Empty);
            
            // assert
            _sut.Currency.Should().Be(originalCurrency);
            _sut.Amount.Should().Be(originalAmount);
            
            // w zależności do podejścia można by użyć metody equals klasy Money
            // Z jednej strony utrzymywanie tego testu stanowi narzut (konieczność uaktualnienia po każdym dodaniu pola)
            // z drugiej strony nie musimy ufać implementacji Equals co stanowi plus.
            // Equals powinno być pokryte testami, mimo wszystko jest niezerowa szansa że testy Equals zawiodą, w wyniku
            // czego możliwe jest że ten test da nam fałszywy pass dlatego zdecydowałem się na porównanie pól.
        }
    }
}