using TDDLab.Core.InvoiceMgmt;

namespace Tests.TDDLab.Core.InvoiceMgmt.Factories
{
    public static class InvoiceLineFactory
    {
        public static InvoiceLine ValidInvoiceLine => new InvoiceLine("ECTS", MoneyFactory.ValidMoney);
        public static InvoiceLine InvalidInvoiceLine => new InvoiceLine(string.Empty, MoneyFactory.InvalidMoney);
    }
}