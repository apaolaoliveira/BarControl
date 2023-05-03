using BarControl.Shared;
using System.Collections;

namespace BarControl.ProductModule
{
    internal class Product : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Product(int productId, string name, string description, decimal price)
        {
            id = productId;
            Name = name;
            Description = description;
            Price = price;
        }

        public override void UpdateData(EntityBase record)
        {
            Product product = (Product)record;

            Name = product.Name;
            Description = product.Description;       
            Price = product.Price;
        }

        public override ArrayList Errors()
        {
            ArrayList ErrorsList = new ArrayList();

            if (string.IsNullOrEmpty(Name.Trim()))
                ErrorsList.Add("\nName is a required field!");

            if (string.IsNullOrEmpty(Description.Trim()))
                ErrorsList.Add("\nCpf is a required field!");

            if(Price == 0)
                ErrorsList.Add("\nPrice is a required field!");

            return ErrorsList;
        }

    }
}
