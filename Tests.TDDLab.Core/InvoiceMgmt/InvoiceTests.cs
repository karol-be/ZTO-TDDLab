using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using TDDLab.Core.InvoiceMgmt;
using Tests.TDDLab.Core.InvoiceMgmt.Factories;
using Xunit;

namespace Tests.TDDLab.Core.InvoiceMgmt
{
    public class InvoiceTests
    {
        private const string ValidInvoiceNumber = "16384";
        private static readonly Recipient ValidRecipient = RecipientsFactory.ValidRecipient;
        private static readonly Address ValidAddress = AddressFactory.ValidAddress;

        private readonly IList<InvoiceLine> _validInvoiceLines = new List<InvoiceLine>
        {
            InvoiceLineFactory.ValidInvoiceLine,
            InvoiceLineFactory.ValidInvoiceLine
        };

        [Fact]
        public void ForValidValues_ShouldSucceedValidation()
        {
            // act
            var sut = new Invoice(ValidInvoiceNumber, ValidRecipient, ValidAddress,
                _validInvoiceLines);

            // assert
            sut.IsValid.Should().BeTrue();
        }

        [Fact]
        public void ForValidValues_ShouldCorrectlySetValues()
        {
            // act
            var sut = new Invoice(ValidInvoiceNumber, ValidRecipient, ValidAddress,
                _validInvoiceLines);

            // assert
            using (new AssertionScope())
            {
                sut.InvoiceNumber.Should().Be(ValidInvoiceNumber);
                sut.Recipient.Should().Be(ValidRecipient);
                sut.BillToAddress.Should().Be(ValidAddress);
                sut.Lines.Should().BeEquivalentTo(_validInvoiceLines);
            }
        }

        // !!! This is based on guess how invoice should work (I assume it may contain a bug) !!!
        [Fact]
        public void ForValidValues_ShouldAttachInvoiceLines()
        {
            // arrange
            var invoiceLines = new[] {InvoiceLineFactory.ValidInvoiceLine, InvoiceLineFactory.ValidInvoiceLine};

            // act
            var sut = new Invoice(ValidInvoiceNumber, ValidRecipient, ValidAddress,
                _validInvoiceLines);

            // assert
            using (new AssertionScope())
            {
                invoiceLines[0].Invoice.Should().Be(sut);
                invoiceLines[1].Invoice.Should().Be(sut);
            }
        }

        [Fact]
        public void ForMissingDiscount_ShouldNotSetDiscount()
        {
            // act
            var sut = new Invoice(ValidInvoiceNumber, ValidRecipient, ValidAddress,
                _validInvoiceLines);

            // assert
            sut.Discount.Should().BeNull();
        }

        [Fact]
        public void ForEmptyInvoiceNumber_ShouldFailValidation()
        {
            // arrange
            var sut = new Invoice(string.Empty, ValidRecipient, ValidAddress,
                _validInvoiceLines);

            // act
            var result = sut.Validate();

            // assert
            result.Should().ContainSingle(Invoice.ValidationRules.BillingAddress.Name);
        }

        [Fact]
        public void ForNullInvoiceNumber_ShouldFailValidation()
        {
            // arrange
            var sut = new Invoice(null, ValidRecipient, ValidAddress,
                _validInvoiceLines);

            // act
            var result = sut.Validate();

            // assert
            result.Should().ContainSingle(Invoice.ValidationRules.BillingAddress.Name);
        }


        [Fact]
        public void ForInvalidRecipient_ShouldFailValidation()
        {
            // arrange
            var sut = new Invoice(ValidInvoiceNumber, RecipientsFactory.InvalidRecipient, ValidAddress,
                _validInvoiceLines);

            // act
            var result = sut.Validate();

            // assert
            result.Should().ContainSingle(Invoice.ValidationRules.Recipient.Name);
        }

        [Fact]
        public void ForNullRecipient_ShouldFailValidation()
        {
            // arrange
            var sut = new Invoice(ValidInvoiceNumber, null, ValidAddress,
                _validInvoiceLines);

            // act
            var result = sut.Validate();

            // assert
            result.Should().ContainSingle(Invoice.ValidationRules.Recipient.Name);
        }

        [Fact]
        public void ForInvalidBillToAddress_ShouldFailValidation()
        {
            // arrange
            var sut = new Invoice(ValidInvoiceNumber, ValidRecipient, AddressFactory.InvalidAddress,
                _validInvoiceLines);

            // act
            var result = sut.Validate();

            // assert
            result.Should().ContainSingle(Invoice.ValidationRules.Recipient.Name);
        }

        [Fact]
        public void ForNullAddressToBill_ShouldFailValidation()
        {
            // arrange
            var sut = new Invoice(ValidInvoiceNumber, ValidRecipient, null, _validInvoiceLines);

            // act
            var result = sut.Validate();

            // assert
            result.Should().ContainSingle(Invoice.ValidationRules.Recipient.Name);
        }


        [Fact]
        public void ForEmptyInvoiceLines_ShouldFailValidation()
        {
            // arrange
            var sut = new Invoice(ValidInvoiceNumber, ValidRecipient, ValidAddress,
                Enumerable.Empty<InvoiceLine>());

            // act
            var result = sut.Validate();

            // assert
            result.Should().ContainSingle(Invoice.ValidationRules.Recipient.Name);
        }

        [Fact]
        public void ForNullInvoiceLines_ShouldFailValidation()
        {
            // arrange
            var sut = new Invoice(ValidInvoiceNumber, ValidRecipient, ValidAddress,
                null);

            // act
            var result = sut.Validate();

            // assert
            result.Should().ContainSingle(Invoice.ValidationRules.Recipient.Name);
        }

        [Fact]
        public void ForInvalidInvoiceLines_ShouldFailValidation()
        {
            // arrange
            var twoInvalidInvoiceLines = new[]
                {InvoiceLineFactory.InvalidInvoiceLine, InvoiceLineFactory.InvalidInvoiceLine};

            var sut = new Invoice(ValidInvoiceNumber, ValidRecipient, ValidAddress,
                twoInvalidInvoiceLines);

            // act
            var result = sut.Validate();

            // assert
            result.Should().ContainSingle(Invoice.ValidationRules.Recipient.Name);
        }


        [Fact]
        public void ForValidAndInvalidInvoiceLines_ShouldFailValidation()
        {
            // arrange
            var validAndInvalidInvoiceLine = new[]
                {InvoiceLineFactory.ValidInvoiceLine, InvoiceLineFactory.InvalidInvoiceLine};

            var sut = new Invoice(ValidInvoiceNumber, ValidRecipient, ValidAddress,
                validAndInvalidInvoiceLine);

            // act
            var result = sut.Validate();

            // assert
            result.Should().ContainSingle(Invoice.ValidationRules.Recipient.Name);
        }
    }
}