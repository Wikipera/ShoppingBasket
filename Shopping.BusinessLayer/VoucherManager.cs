using Shopping.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.BusinessLayer
{
   public class VoucherManager: IVoucherManager
    {
        private List<IVoucher> vouchers = new List<IVoucher>();
        public bool HasFailedVouchers
        {
            get { return vouchers.Any(x => x.State == VoucherState.Failed); }
        }
        public void AddVoucher(IVoucher voucher)
        {
            vouchers.Add(voucher);
        }
        public decimal ApplyVouchers(IList<IProduct> products) {
            decimal totalDiscount = default(decimal);
            foreach(var voucher in vouchers.Where(x => x.State == VoucherState.UnProcessed))
            {
                if (voucher.Type == VoucherType.Offer && vouchers.Where(x => x.State == VoucherState.Redeemed
                                          && x.Type == VoucherType.Offer).Count() >= 1)
                {
                    voucher.SetState(VoucherState.Failed);
                }

                if (voucher.Type == VoucherType.Gift ||
                    voucher.Type == VoucherType.Offer && vouchers.Where(x => x.State == VoucherState.Redeemed 
                                                           && x.Type == VoucherType.Offer).Count() < 1) {
                    var result = voucher.Redeem(products,totalDiscount);
                    totalDiscount += result.Status ? result.DiscountAmount : default(decimal);
                }
               
            }
            return totalDiscount;
        }
        public void Print()
        {
            if (vouchers.Count() > 0) Console.WriteLine("----------------");
            foreach (var voucher in vouchers) voucher.Print();
        }
        public void PrintValidationMessages()
        {
            Console.WriteLine("----------------");
            foreach (var voucher in vouchers.Where(x => x.State == VoucherState.Failed)) Console.WriteLine(voucher.ValidationMessage);
        }

    }
}
