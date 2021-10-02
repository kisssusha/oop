using System;

namespace Shops.Models
{
    public class Product
    {
        public Product(string productName)
        {
            Guid = Guid.NewGuid();
            ProductName = productName;
        }

        public Guid Guid
        {
            get;
        }

        public string ProductName
        {
            get;
        }
    }
}