using System.Collections.Generic;

namespace Shopping.Common
{
    public interface IVoucher
    {
        VoucherType Type { get; }
        VoucherState State { get; }
        string VoucherReference { get; }
        decimal VoucherPrice { get; }
        string ValidationMessage { get; }
        IVoucherResponse Redeem(IList<IProduct> products, decimal appliedVoucherValue);
        void SetState(VoucherState voucherState);
        void Print();
    }
}
