using Shopping.Common;

namespace Shopping.BusinessLayer
{
    public class Product : IProduct
    {
        public int ProductId { get; private set; }
        public ProductCatogery ProductCatogery { get; private set; }
        public string ProductName { get; private set; }
        public decimal Price { get; private set; }
        public Product(int productId, ProductCatogery productCatogery, string name, decimal price)
        {
            ProductId = productId;
            ProductCatogery = productCatogery;
            ProductName = name;
            Price = price;
        }
    }
}
