using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shops.Models;
using Shops.Services;

namespace Shops.Tests
{
    public class ShopManagerTest
    {
        [Test]
        public void CreateShop_ThrowException()
        {
            var shopManager = new ShopManager();
            string address = "Чкаловский 16";
            Shop shop = shopManager.Create("Okey", address);
            string name = "Okey";
            Assert.Equals(shop.ShopName, name);
            Assert.Equals(shop.Address, address);
        }

        [Test]
        public void PersonBuyProducts()
        {
            const uint moneyBefore = 1000;
            const uint productPrice = 80;
            const uint productToBuyCount = 2;
            const uint productCount = 6;

            var person = new Person("name", moneyBefore);
            var shopManager = new ShopManager();
            Shop shop = shopManager.Create("Okey", "Чкаловский 16");
            Product product = shopManager.RegisterProduct("Milk");
            var products = new Dictionary<Product, ShopProductInfo>
            {
                {product, new ShopProductInfo(productPrice, productCount)}
            };
            var productsToBuy = new Dictionary<Product, ShopProductInfo>
            {
                {product, new ShopProductInfo(productPrice, productToBuyCount)}
            }; 
            
            shop.AddProducts(products);
            shop.Buy(person, productsToBuy);
            
            Assert.AreEqual(moneyBefore - productPrice  * productToBuyCount, person.Money);
            Assert.AreEqual(productCount - productToBuyCount , shop.GetProductInfo(product.Guid).Count);
        }

        [Test]
        public void BestPrice()
        {
            const uint productPrice1 = 80;
            const uint productPrice2 = 100;
            
            const uint productToBuyCount = 2;
            const uint productCount1 = 10;
            const uint productCount2 = 8;

            var shopManager = new ShopManager();
            Shop shop1 = shopManager.Create("Okey", "Чкаловский 16");
            Shop shop2 = shopManager.Create("Azbuka Vlusa", "Каменноостровский 45");

            Product product1 = shopManager.RegisterProduct("Milk");
            Product product2 = shopManager.RegisterProduct("Eggs");
            
            var OkeyProducts = new Dictionary<Product, ShopProductInfo>
            {
                {product1, new ShopProductInfo(productPrice1, productCount1)},
                {product2, new ShopProductInfo(productPrice1, productCount1)},
            };
            shop1.AddProducts(OkeyProducts);
            
            var AzbukaProducts = new Dictionary<Product, ShopProductInfo>
            {
                {product1, new ShopProductInfo(productPrice2, productCount2)},
                {product2, new ShopProductInfo(productPrice2, productCount2)},
            };
            shop2.AddProducts(AzbukaProducts);

            var productsToBuy = new Dictionary<Product, ShopProductInfo>
            {
                {product1, new ShopProductInfo(productToBuyCount)},
                {product2, new ShopProductInfo(productToBuyCount)},
            };
            
            Shop shop = shopManager.FindBestPrice(new ProductsList(productsToBuy));
            if(shop.ShopName != "Okey")
                Assert.Fail();
        }

        [Test] 
        public void ChangePrice()
        {
            const uint productPrice = 90;
            const uint productCount = 3;
            const uint expectedPrice = 100;
            var shopManager = new ShopManager();
            Shop shop = shopManager.Create("Okey", "Чкаловский 16");
            Product product = shopManager.RegisterProduct("Milk");
            shop.AddProduct(product, new ShopProductInfo(productPrice, productCount));
            shop.ChangePrice(product, expectedPrice);
            Assert.AreEqual(shop.GetProductInfo(product.Guid).Price, expectedPrice);
        }
    }
}