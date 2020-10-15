using System;
using System.Linq;
using FluentAssertions;
using TDDLab.Core.InvoiceMgmt;
using Xunit;

namespace Tests.TDDLab.Core
{
    public class AddressTests
    {
        private readonly string _validAddressLine1 = "Politechnika, Gabriela Narutowicza 11/12";
        private readonly string _validCity = "Gdańsk";
        private readonly string _validState = "Pomorze";
        private readonly string _validZip = "80-233";
        
        [Fact]
        public void ForEmptyAddressLine1_ShouldFailValidation()
        {
            // arrange
            var sut = new Address(string.Empty, _validCity, _validState, _validZip);
            
            // act
            var brokenRules = sut.Validate();
            
            // assert
            brokenRules.Should().Contain(Address.ValidationRules.AddressLine1);

        }
        
        [Fact]
        public void ForEmptyCity_ShouldFailValidation()
        {
            // arrange
            var sut = new Address(_validAddressLine1, string.Empty, _validState, _validZip);
            
            // act
            var brokenRules = sut.Validate();
            
            // assert
            brokenRules.Should().Contain(Address.ValidationRules.City);
        }
        
        [Fact]
        public void ForEmptyState_ShouldFailValidation()
        {
            // arrange
            var sut = new Address(_validAddressLine1, string.Empty, string.Empty, _validZip);
            
            // act
            var brokenRules = sut.Validate();
            
            // assert
            brokenRules.Should().Contain(Address.ValidationRules.State);
        }
        
        [Fact]
        public void ForEmptyZip_ShouldFailValidation()
        {
            // arrange
            var sut = new Address(_validAddressLine1, _validCity, _validState, string.Empty);
            
            // act
            var brokenRules = sut.Validate();
            
            // assert
            brokenRules.Should().Contain(Address.ValidationRules.Zip);
        }
    }
}