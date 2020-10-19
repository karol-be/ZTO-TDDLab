using TDDLab.Core.InvoiceMgmt;

namespace Tests.TDDLab.Core.InvoiceMgmt.Factories
{
    public static class MoneyFactory
    {
        public static Money ValidMoney => new Money(0);
        public static Money InvalidMoney => new Money(0, string.Empty);
    }
}