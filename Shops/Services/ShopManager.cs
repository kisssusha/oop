﻿using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Models;

namespace Shops.Services
{
    public class ShopManager
    {
        private readonly List<Shop> _shops;
        private readonly List<Product> _registeredProducts;

        public ShopManager()
        {
            _registeredProducts = new List<Product>();
            _shops = new List<Shop>();
        }

        public Shop Create(string shopName, string address)
        {
            var shop = new Shop(shopName, address);
            _shops.Add(shop);
            return shop;
        }

        public Product RegisterProduct(string productName)
        {
            var prod = new Product(productName);
            _registeredProducts.Add(prod);
            return prod;
        }

        public Shop FindBestPrice(ProductsList productsList)
        {
            uint cost = uint.MaxValue;
            Shop sh = null;
            foreach (Shop shop in _shops)
            {
                uint currentCost = shop.GetPrice(productsList.ProductList);
                if (currentCost >= cost) continue;
                sh = shop;
                cost = currentCost;
            }

            if (cost == uint.MaxValue)
                throw new Exception("Shops haven't this count of products");
            return sh;
        }

        public Shop FindShop(string shopName, string address)
        {
            return _shops.FirstOrDefault(shop => shop.Address == address && shop.ShopName == shopName);
        }
    }
}