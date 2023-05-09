
namespace BarControl.ProductModule
{
    internal class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(List<Product> productList)
        {
            records = productList;
        }
    }
}
