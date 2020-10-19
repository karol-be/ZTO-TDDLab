using TDDLab.Core.InvoiceMgmt;

namespace Tests.TDDLab.Core.InvoiceMgmt.Factories
{
    public static class RecipientsFactory
    {
        private const string ValidName = "Dziekan";
        public static Recipient ValidRecipient => new Recipient(ValidName, AddressFactory.ValidAddress);
        public static Recipient InvalidRecipient => new Recipient(string.Empty, AddressFactory.InvalidAddress);
    }
}