namespace Shopping.Common
{
    public interface IProduct
    {
        int ProductId { get; }
        ProductCatogery ProductCatogery { get; }
        string ProductName { get; }
        decimal Price { get; }
    }
}
