namespace ApplicationCore.Exceptions
{
    public class ProductNotFound : Exception
    {
        public ProductNotFound(int productId) : base($"No product found with id {productId}")
        {
        }
    }
}
