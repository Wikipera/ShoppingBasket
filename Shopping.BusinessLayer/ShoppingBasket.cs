using Shopping.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.BusinessLayer
{
    public class ShoppingBasket 
    {
      
        public decimal TotalValue { get; private set; }
        private decimal discountValue; 
        private readonly IVoucherManager voucherManager;
        private List<IProduct> products = new List<IProduct>();
        public ShoppingBasket(IVoucherManager voucherManager)
        {
            this.voucherManager = voucherManager;
            discountValue = default(decimal);
        }
        public void Add(IProduct product)
        {
             products.Add(product);
        }
        public void Remove(IProduct product) {
            if (products.Contains(product)) {
                products.Remove(product);
            }
        }
        public void Add(IVoucher voucher)
        {
            voucherManager.AddVoucher(voucher);
        }
        public void Checkout() {

            PrintShoppingBag();
            voucherManager.Print();
            discountValue = voucherManager.ApplyVouchers(products);
            TotalValue = products.Sum(x => x.Price) - discountValue;
            PrintTotal();
            if (voucherManager.HasFailedVouchers) voucherManager.PrintValidationMessages();
        }
        private void PrintShoppingBag()
        {
            foreach (var product in products) Console.WriteLine($"1 x {product.ProductName} @ £{product.Price.ToString("F")}");
            
        }
        private void PrintTotal()
        {
            Console.WriteLine("----------------");
            Console.WriteLine("Total: £" + TotalValue.ToString("F"));
        }

    }
    
}
