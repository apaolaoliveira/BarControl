using BarControl.Shared;
using System.Collections;

namespace BarControl.ProductModule
{
    internal class ProductRepository : RepositoryBase 
    {
        public ProductRepository(ArrayList arrayList)
        {
            records = arrayList;
        }
    }
}
