using Shopping.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shopping.BusinessLayer
{
    public class GiftVoucher : IVoucher
    {
        public VoucherType Type => VoucherType.Gift;
        public VoucherState State { get; private set; }
        public string ValidationMessage { get; private set; }
        public decimal VoucherPrice { get; private set; }
        public string VoucherReference { get; private set; }
        public GiftVoucher(string refernce, decimal price)
        {
            VoucherReference = refernce;
            VoucherPrice = price;
            State = VoucherState.UnProcessed;
        }
        public IVoucherResponse Redeem(IList<IProduct> products, decimal appliedVouchers)
        {            
            if(products.Where(x => x.ProductCatogery != ProductCatogery.GiftVoucher)
                .Sum(x => x.Price) > VoucherPrice + appliedVouchers)
            {
                State = VoucherState.Redeemed;
                return new VoucherResponse(true, VoucherPrice);
            }
            else
            {
                State = VoucherState.Failed;
                ValidationMessage = $"Basket total less than {VoucherType.Gift.ToString()} Voucher {VoucherReference} value £{VoucherPrice.ToString("F")}." +
                    $" Spend another £{(VoucherPrice + appliedVouchers- products.Where(x => x.ProductCatogery != ProductCatogery.GiftVoucher).Sum(x => x.Price) + decimal.Parse("0.01")).ToString("F")} to apply this Voucher"; 
                return new VoucherResponse(false, default(decimal));               
            }   
        }

        public void Print()
        {
            Console.WriteLine($"1 x £{VoucherPrice.ToString("F")} {VoucherType.Gift.ToString()} Voucher {VoucherReference} applied");
        }
        public void SetState(VoucherState voucherState) {
            State = voucherState;
        }
    }
}
