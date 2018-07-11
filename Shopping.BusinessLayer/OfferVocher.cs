using Shopping.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shopping.BusinessLayer
{
    public class OfferVocher : IVoucher
    {
        public VoucherType Type => VoucherType.Offer;
        public VoucherState State {get; private set; }
        public string VoucherReference { get; private set; }
        public string ValidationMessage { get; private set; }
        public decimal VoucherPrice { get; private set; }
        private decimal Threshold { get; set; }
        private ProductCatogery ProductCatogery { get; set; }
        public OfferVocher(string reference, decimal price, decimal threshold, ProductCatogery productCatogery)
        {
            VoucherReference = reference;
            VoucherPrice = price;
            Threshold = threshold;
            ProductCatogery = productCatogery;
            State = VoucherState.UnProcessed;

        }
        public IVoucherResponse Redeem(IList<IProduct> products, decimal appliedVouchers)
        {
            if (products.Where(x => x.ProductCatogery != ProductCatogery.GiftVoucher)
             .Sum(x => x.Price) > Threshold && products.Any(x => x.ProductCatogery == ProductCatogery))
            {
                State = VoucherState.Redeemed;
                decimal discountAmount = VoucherPrice > products.Where(x => x.ProductCatogery == ProductCatogery).Sum(x => x.Price) ?
                                         products.Where(x => x.ProductCatogery == ProductCatogery).Sum(x => x.Price) :
                                         VoucherPrice;
                return new VoucherResponse(true, discountAmount);
            }
            else
            {
                State = VoucherState.Failed;
                ValidationMessage = products.Any(x => x.ProductCatogery == ProductCatogery)?
                                         $"You have not reached the spend threshold for voucher {VoucherReference} . Spend another " +
                                         $"£{(Threshold - products.Where(x => x.ProductCatogery != ProductCatogery.GiftVoucher).Sum(x => x.Price) + decimal.Parse("0.01")).ToString("F")} " +
                                         $"to receive £{VoucherPrice.ToString("F")} from you basket total"
                                         :
                                         $"There are no products in your basket applicable to {Type.ToString()} Voucher {VoucherReference}.";
                return new VoucherResponse(false, default(decimal));
            }
       
        }
        public void Print()
        {
            Console.WriteLine($"1 x £{VoucherPrice.ToString("F")} off {ProductCatogery.ToString()} in baskets over £{Threshold.ToString("F")} {Type.ToString()} Voucher {VoucherReference} applied");
        }
        public void SetState(VoucherState voucherState)
        {
            State = voucherState;
            ValidationMessage = $"You cannot apply multiple {Type.ToString()} Voucher to the same Basket";

        }
    }
}
