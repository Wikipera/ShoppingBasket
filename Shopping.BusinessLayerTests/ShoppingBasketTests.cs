using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shopping.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shopping.Common;
using System.Threading.Tasks;

namespace Shopping.BusinessLayer.Tests
{
    [TestClass()]
    public class ShoppingBasketTests
    {
        private ShoppingBasket shoppingBasket = null;
        [TestInitialize()]
        public void Initialize()
        {
            IVoucherManager voucherManager = new VoucherManager();
            shoppingBasket = new ShoppingBasket(voucherManager);
        }

        [TestMethod()]
        public void ShoppingBasket_Test01_Without_Vouchers()
        {
            shoppingBasket.Add(new Product(1,ProductCatogery.Hat,"Hat",decimal.Parse("10.50")));
            shoppingBasket.Add(new Product(2, ProductCatogery.Jumper, "Jumper", decimal.Parse("54.65")));
            shoppingBasket.Checkout();
            Assert.AreEqual(shoppingBasket.TotalValue, decimal.Parse("65.15"));
       
        }
        [TestMethod()]
        public void ShoppingBasket_Test02_With_Single_Gift_Voucher()
        {
            shoppingBasket.Add(new Product(1, ProductCatogery.Hat, "Hat", decimal.Parse("10.50")));
            shoppingBasket.Add(new Product(2, ProductCatogery.Jumper, "Jumper", decimal.Parse("54.65")));
            shoppingBasket.Add(new GiftVoucher(Guid.NewGuid().ToString(), 5));
            shoppingBasket.Checkout();
            Assert.AreEqual(shoppingBasket.TotalValue, decimal.Parse("60.15"));

        }
        [TestMethod()]
        public void ShoppingBasket_Test03_With_Multiple_Gift_Voucher()
        {
            shoppingBasket.Add(new Product(1, ProductCatogery.Hat, "Hat", decimal.Parse("10.50")));
            shoppingBasket.Add(new Product(2, ProductCatogery.Jumper, "Jumper", decimal.Parse("54.65")));
            shoppingBasket.Add(new GiftVoucher(Guid.NewGuid().ToString(), 10));
            shoppingBasket.Add(new GiftVoucher(Guid.NewGuid().ToString(), 40));
            shoppingBasket.Checkout();
            Assert.AreEqual(shoppingBasket.TotalValue, decimal.Parse("15.15"));

        }
        [TestMethod()]
        public void ShoppingBasket_Test04_With_Multiple_Gift_Voucher_Exceeding_BasketValue()
        {
            shoppingBasket.Add(new Product(1, ProductCatogery.Hat, "Hat", decimal.Parse("10.50")));
            shoppingBasket.Add(new Product(2, ProductCatogery.Jumper, "Jumper", decimal.Parse("54.65")));
            shoppingBasket.Add(new GiftVoucher(Guid.NewGuid().ToString(), 10));
            shoppingBasket.Add(new GiftVoucher(Guid.NewGuid().ToString(), 100));
            shoppingBasket.Checkout();
            Assert.AreEqual(shoppingBasket.TotalValue, decimal.Parse("55.15"));

        }
        [TestMethod()]
        public void ShoppingBasket_Test05_With_Multiple_Gift_Voucher_Exceeding_BasketValue()
        {
            shoppingBasket.Add(new Product(1, ProductCatogery.Hat, "Hat", decimal.Parse("10.50")));
            shoppingBasket.Add(new Product(2, ProductCatogery.Jumper, "Jumper", decimal.Parse("54.65")));
            shoppingBasket.Add(new GiftVoucher(Guid.NewGuid().ToString(), 10));
            shoppingBasket.Add(new GiftVoucher(Guid.NewGuid().ToString(), 10));
            shoppingBasket.Add(new GiftVoucher(Guid.NewGuid().ToString(), 10));
            shoppingBasket.Add(new GiftVoucher(Guid.NewGuid().ToString(), 10));
            shoppingBasket.Add(new GiftVoucher(Guid.NewGuid().ToString(), 10));
            shoppingBasket.Add(new GiftVoucher(Guid.NewGuid().ToString(), 10));
            shoppingBasket.Add(new GiftVoucher(Guid.NewGuid().ToString(), 10));
            shoppingBasket.Checkout();
            Assert.AreEqual(shoppingBasket.TotalValue, decimal.Parse("5.15"));

        }
        [TestMethod()]
        public void ShoppingBasket_Test06_Single_Offer_Vouchers()
        {
            shoppingBasket.Add(new Product(1, ProductCatogery.Hat, "Hat", decimal.Parse("10.50")));
            shoppingBasket.Add(new Product(2, ProductCatogery.Jumper, "Jumper", decimal.Parse("54.65")));
            shoppingBasket.Add(new OfferVocher(Guid.NewGuid().ToString(), 5, 50,ProductCatogery.Hat));
            shoppingBasket.Checkout();
            Assert.AreEqual(shoppingBasket.TotalValue, decimal.Parse("60.15"));

        }
        [TestMethod()]
        public void ShoppingBasket_Test07_Single_Offer_Vouchers_InValid_Catogery()
        {
            shoppingBasket.Add(new Product(1, ProductCatogery.Hat, "Hat", decimal.Parse("10.50")));
            shoppingBasket.Add(new Product(2, ProductCatogery.Jumper, "Jumper", decimal.Parse("54.65")));
            shoppingBasket.Add(new OfferVocher(Guid.NewGuid().ToString(), 5, 50, ProductCatogery.Trainers));
            shoppingBasket.Checkout();
            Assert.AreEqual(shoppingBasket.TotalValue, decimal.Parse("65.15"));

        }
        [TestMethod()]
        public void ShoppingBasket_Test08_Mulitple_Offer_Vouchers_With_Valid_Catogery()
        {
            shoppingBasket.Add(new Product(1, ProductCatogery.Hat, "Hat", decimal.Parse("10.50")));
            shoppingBasket.Add(new Product(2, ProductCatogery.Jumper, "Jumper", decimal.Parse("54.65")));
            shoppingBasket.Add(new OfferVocher(Guid.NewGuid().ToString(), 5, 50, ProductCatogery.Hat));
            shoppingBasket.Add(new OfferVocher(Guid.NewGuid().ToString(), 5, 50, ProductCatogery.Hat));
            shoppingBasket.Checkout();
            Assert.AreEqual(shoppingBasket.TotalValue, decimal.Parse("60.15"));

        }
        [TestMethod()]
        public void ShoppingBasket_Test09_Single_Gift_And_Single_Offer_Vouchers()
        {
            shoppingBasket.Add(new Product(1, ProductCatogery.Hat, "Hat", decimal.Parse("10.50")));
            shoppingBasket.Add(new Product(2, ProductCatogery.Jumper, "Jumper", decimal.Parse("54.65")));
            shoppingBasket.Add(new GiftVoucher(Guid.NewGuid().ToString(), 5));
            shoppingBasket.Add(new OfferVocher(Guid.NewGuid().ToString(), 5, 50, ProductCatogery.Hat));
            shoppingBasket.Checkout();
            Assert.AreEqual(shoppingBasket.TotalValue, decimal.Parse("55.15"));

        }
        [TestMethod()]
        public void ShoppingBasket_Test10_Multiple_Gift_And_Single_Offer_Vouchers()
        {
            shoppingBasket.Add(new Product(1, ProductCatogery.Hat, "Hat", decimal.Parse("10.50")));
            shoppingBasket.Add(new Product(2, ProductCatogery.Jumper, "Jumper", decimal.Parse("54.65")));
            shoppingBasket.Add(new GiftVoucher(Guid.NewGuid().ToString(), 5));
            shoppingBasket.Add(new GiftVoucher(Guid.NewGuid().ToString(), 5));
            shoppingBasket.Add(new OfferVocher(Guid.NewGuid().ToString(), 5, 50, ProductCatogery.Hat));
            shoppingBasket.Checkout();
            Assert.AreEqual(shoppingBasket.TotalValue, decimal.Parse("50.15"));

        }
        [TestMethod()]
        public void ShoppingBasket_Test11_Multiple_Gift_And_Single_Offer_Voucher_WithInValid_Catogery()
        {
            shoppingBasket.Add(new Product(1, ProductCatogery.Hat, "Hat", decimal.Parse("10.50")));
            shoppingBasket.Add(new Product(2, ProductCatogery.Jumper, "Jumper", decimal.Parse("54.65")));
            shoppingBasket.Add(new GiftVoucher(Guid.NewGuid().ToString(), 5));
            shoppingBasket.Add(new GiftVoucher(Guid.NewGuid().ToString(), 5));
            shoppingBasket.Add(new OfferVocher(Guid.NewGuid().ToString(), 5, 50, ProductCatogery.Shoes));
            shoppingBasket.Checkout();
            Assert.AreEqual(shoppingBasket.TotalValue, decimal.Parse("55.15"));

        }
        [TestMethod()]
        public void ShoppingBasket_Test12_With_Gift_Voucher_Item_And_Gift_Voucher()
        {
            shoppingBasket.Add(new Product(1, ProductCatogery.Shirts, "Shirt", decimal.Parse("10.50")));
            shoppingBasket.Add(new Product(2, ProductCatogery.GiftVoucher, "Gift", decimal.Parse("20.00")));
            shoppingBasket.Add(new GiftVoucher(Guid.NewGuid().ToString(), 10));
            shoppingBasket.Checkout();
            Assert.AreEqual(shoppingBasket.TotalValue, decimal.Parse("20.50"));

        }
        [TestMethod()]
        public void ShoppingBasket_Test13_With_Gift_Voucher_Item_And_Gift_Voucher_Exceeding_Value()
        {
            shoppingBasket.Add(new Product(1, ProductCatogery.Shirts, "Shirt", decimal.Parse("10.50")));
            shoppingBasket.Add(new Product(2, ProductCatogery.GiftVoucher, "Gift", decimal.Parse("20.00")));
            shoppingBasket.Add(new GiftVoucher(Guid.NewGuid().ToString(), 20));
            shoppingBasket.Checkout();
            Assert.AreEqual(shoppingBasket.TotalValue, decimal.Parse("30.50"));

        }
        [TestMethod()]
        public void ShoppingBasket_Test14_With_Gift_Voucher_Item_And_Offer_Voucher_Exceeding_Value()
        {
            shoppingBasket.Add(new Product(1, ProductCatogery.Shirts, "Shirt", decimal.Parse("40.50")));
            shoppingBasket.Add(new Product(2, ProductCatogery.GiftVoucher, "Gift", decimal.Parse("20.00")));
            shoppingBasket.Add(new OfferVocher(Guid.NewGuid().ToString(), 5, 50, ProductCatogery.Shirts));
            shoppingBasket.Checkout();
            Assert.AreEqual(shoppingBasket.TotalValue, decimal.Parse("60.50"));

        }
        [TestMethod()]
        public void ShoppingBasket_Test15_With_Offer_Voucher_With_Valid_Catogery_Under_Priced_item()
        {
            shoppingBasket.Add(new Product(1, ProductCatogery.Hat, "Hat", decimal.Parse("25.00")));
            shoppingBasket.Add(new Product(2, ProductCatogery.Jumper, "Jumper", decimal.Parse("26.00")));
            shoppingBasket.Add(new Product(3, ProductCatogery.TShirts, "TShirts", decimal.Parse("3.50")));
            shoppingBasket.Add(new OfferVocher(Guid.NewGuid().ToString(), 5, 50, ProductCatogery.TShirts));
            shoppingBasket.Checkout();
            Assert.AreEqual(shoppingBasket.TotalValue, decimal.Parse("51.00"));

        }
        [TestMethod()]
        public void ShoppingBasket_Test16_With_Offer_Voucher_With_Valid_Categery_Over_Priced_item()
        {
            shoppingBasket.Add(new Product(1, ProductCatogery.Hat, "Hat", decimal.Parse("25.00")));
            shoppingBasket.Add(new Product(2, ProductCatogery.Jumper, "Jumper", decimal.Parse("26.00")));
            shoppingBasket.Add(new Product(3, ProductCatogery.TShirts, "TShirts", decimal.Parse("6.00")));
            shoppingBasket.Add(new OfferVocher(Guid.NewGuid().ToString(), 5, 50, ProductCatogery.TShirts));
            shoppingBasket.Checkout();
            Assert.AreEqual(shoppingBasket.TotalValue, decimal.Parse("52.00"));

        }


    }
}