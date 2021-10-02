using System.Collections.Generic;

namespace Shops.Models
{
    public class ProductsList
    {
        public ProductsList(Dictionary<Product, ShopProductInfo> products)
        {
            ProductList = products;
        }

        public Dictionary<Product, ShopProductInfo> ProductList
        {
            get;
        }
    }
}