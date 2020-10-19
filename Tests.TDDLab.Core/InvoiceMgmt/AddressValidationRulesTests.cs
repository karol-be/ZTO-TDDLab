using FluentAssertions;
using FluentAssertions.Execution;
using TDDLab.Core.InvoiceMgmt;
using Xunit;

namespace Tests.TDDLab.Core.InvoiceMgmt
{
    public class AddressValidationRulesTests
    {
        private readonly string _validAddressLine1 = "Politechnika, Gabriela Narutowicza 11/12";
        private readonly string _validCity = "Gdańsk";
        private readonly string _validState = "Pomorze";
        private readonly string _validZip = "80-233";

        [Fact]
        public void ForValidValues_ShouldSucceedValidation()
        {
            // arrange
            var sut = new Address(_validAddressLine1, _validCity, _validState, _validZip);

            // act
            var result = sut.Validate();

            // assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void ForValidValues_ShouldSetCorrectValues()
        {
            // arrange
            var sut = new Address(_validAddressLine1, _validCity, _validState, _validZip);

            // act
            var result = sut.Validate();

            // assert
            using (new AssertionScope())
            {
                sut.AddressLine1.Should().Be(_validAddressLine1);
                sut.City.Should().Be(_validCity);
                sut.State.Should().Be(_validState);
                sut.Zip.Should().Be(_validZip);
            }
        }

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
        public void ForNullAddressLine1_ShouldFailValidation()
        {
            // arrange
            var sut = new Address(null, _validCity, _validState, _validZip);

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
        public void ForNullCity_ShouldFailValidation()
        {
            // arrange
            var sut = new Address(_validAddressLine1, null, _validState, _validZip);

            // act
            var brokenRules = sut.Validate();

            // assert
            brokenRules.Should().Contain(Address.ValidationRules.City);
        }

        [Fact]
        public void ForEmptyState_ShouldFailValidation()
        {
            // arrange
            var sut = new Address(_validAddressLine1, _validCity, string.Empty, _validZip);

            // act
            var brokenRules = sut.Validate();

            // assert
            brokenRules.Should().Contain(Address.ValidationRules.State);
        }

        [Fact]
        public void ForNullState_ShouldFailValidation()
        {
            // arrange
            var sut = new Address(_validAddressLine1, _validCity, null, _validZip);

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

        [Fact]
        public void ForNullZip_ShouldFailValidation()
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