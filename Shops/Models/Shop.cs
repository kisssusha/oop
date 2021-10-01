using System;
using System.Collections.Generic;
using System.Linq;

namespace Shops.Models
{
    public class Shop
    {
        private readonly Dictionary<Product, ShopProductInfo> _productsDictionary;
        private Guid _guid;
        public Shop(string shopName, string address)
        {
            _guid = Guid.NewGuid();
            ShopName = shopName;
            Address = address;
            _productsDictionary = new Dictionary<Product, ShopProductInfo>();
        }

        public string ShopName { get; }
        public string Address { get; }

        public void ChangePrice(Product product, uint price)
        {
            _productsDictionary[FindProduct(product.Guid)].Price = price;
        }

        public void AddProducts(Dictionary<Product, ShopProductInfo> products)
        {
            foreach ((Product product, ShopProductInfo productInfo) in products)
                AddProduct(product, productInfo);
        }

        public uint GetPrice(Dictionary<Product, ShopProductInfo> products)
        {
            uint cost = 0;
            foreach ((Product product, ShopProductInfo productInfo) in products)
            {
                Product prod = FindProduct(product.Guid);
                if (_productsDictionary[prod].Count < productInfo.Count)
                    return uint.MaxValue;
                cost += productInfo.Count * _productsDictionary[prod].Price;
                _productsDictionary[prod].Count -= productInfo.Count;
            }

            return cost;
        }

        public void Buy(Person person, Dictionary<Product, ShopProductInfo> products)
        {
            uint cost = 0;
            foreach ((Product product, ShopProductInfo productInfo) in products)
            {
                Product prod = FindProduct(product.Guid);
                if (prod is null)
                    throw new Exception($"Shop hasn't {product.ProductName}");
                if (_productsDictionary[prod].Count < productInfo.Count)
                    throw new Exception($"Shop hasn't {productInfo.Count} {prod.ProductName}");
                cost += productInfo.Price * productInfo.Count;
                _productsDictionary[prod].Count -= productInfo.Count;
            }

            if (person.Money < cost)
                throw new Exception("Person doesn't have enough money");
            person.Money -= cost;
        }

        public Product FindProduct(Guid id)
        {
            return _productsDictionary.Keys.FirstOrDefault(p => p.Guid == id);
        }

        public void AddProduct(Product product, ShopProductInfo productInfo)
        {
            Product prod = FindProduct(product.Guid);
            if (prod is null)
            {
                _productsDictionary.Add(product, productInfo);
                return;
            }

            _productsDictionary[product].Count += productInfo.Count;
        }

        public ShopProductInfo GetProductInfo(Guid id)
        {
            KeyValuePair<Product, ShopProductInfo> productInfo = _productsDictionary.FirstOrDefault(pi => pi.Key.Guid == id);
            return productInfo.Value;
        }
    }
}