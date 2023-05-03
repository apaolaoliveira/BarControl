using BarControl.ProductModule;
using BarControl.Shared;
using System.Collections;
using System.Data.Common;

namespace BarControl.AccountModule
{
    internal class Order
    {
        public Product Product { get; set; }
        public decimal Quantity { get; set; }

        public Order() { }

        public Order(Product product, int qtd)
        {
            Product = product;
            Quantity = qtd;
        }

        public decimal CalculatePrice(decimal quantity, decimal price)
        {  
            return quantity * price;
        }
    }
}
