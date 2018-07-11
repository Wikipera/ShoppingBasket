using Shopping.Common;
using System.Collections.Generic;

namespace Shopping.BusinessLayer
{
    public interface IVoucherManager
    {
        bool HasFailedVouchers { get; }
        void AddVoucher(IVoucher voucher);
        decimal ApplyVouchers(IList<IProduct> products);
        void Print();
        void PrintValidationMessages();
    }
}
