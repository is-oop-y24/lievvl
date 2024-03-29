﻿using System.Collections.Generic;
using NuGet.Frameworks;
using Shops.Services;
using Shops.Tools;
using NUnit.Framework;

namespace Shops.Tests
{
    public class Tests
    {
        private IShopManager _shopManager;
        private Person _richMe;
        private Person _poorSomebody;

        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
            _richMe = new Person(1000000);
            _poorSomebody = new Person(1);
        }

        [Test]
        public void AddShop_AddProduct_BuyProduct_NotEnouchMoney_ThrowException()
        {
            Shop shop = _shopManager.AddShop("avtosalon", "gdeto");
            Product product = _shopManager.AddProduct("Porsche 911 turbo S");
            _shopManager.AddProductToShop(product, 100, 2, shop);
            int moneyBefore = _richMe.Money;
            int amountBefore = shop.CheckProductAmount(product);
            _shopManager.BuyProduct(shop, product, _richMe, 1);
            Assert.AreEqual(moneyBefore - shop.CheckProductPrice(product), _richMe.Money);
            Assert.AreEqual(amountBefore - 1, shop.CheckProductAmount(product));
            Assert.Catch<ShopException>(() =>
            {
                _shopManager.BuyProduct(shop, product, _poorSomebody, 2);
            });
        }

        [Test]
        public void SetPriceOnProduct()
        {
            Shop shop = _shopManager.AddShop("lala", "lala");
            Product product = _shopManager.AddProduct("lala");
            _shopManager.AddProductToShop(product, 1, 1, shop);
            _shopManager.SetNewPrice(shop, product, 2);
            Assert.AreEqual(2, shop.CheckProductPrice(product));
        }

        [Test] 
        public void SearchMinimalPrice_HaventFound()
        {
            Shop shop1 = _shopManager.AddShop("5", "?");
            Shop shop2 = _shopManager.AddShop("6", "??");
            Product product = _shopManager.AddProduct("potato");
            _shopManager.AddProductToShop(product, 3, 10, shop1);
            _shopManager.AddProductToShop(product, 2, 11, shop2);
            int NEEDED = 12;
            Shop shopWithMinPrice = null;
            int minPrice = 10000;
            foreach (Shop shop in _shopManager.FindShopsContainingProduct(product))
            {
                if (shop.CheckProductAmount(product) > NEEDED && minPrice > shop.CheckProductPrice(product))
                {
                    minPrice = shop.CheckProductPrice(product);
                    shopWithMinPrice = shop;
                }
            }
            Assert.AreEqual(null, shopWithMinPrice);

            NEEDED = 3;
            minPrice = 10000;
            foreach (Shop shop in _shopManager.FindShopsContainingProduct(product))
            {
                if (shop.CheckProductAmount(product) > NEEDED && minPrice > shop.CheckProductPrice(product))
                {
                    minPrice = shop.CheckProductPrice(product);
                    shopWithMinPrice = shop;
                }
            }
            Assert.AreEqual(shop2, shopWithMinPrice);

            shopWithMinPrice = null;
            minPrice = 10000;
            Product nonExistentProduct = _shopManager.AddProduct("UFO");
            foreach (Shop shop in _shopManager.FindShopsContainingProduct(nonExistentProduct))
            {
                if (shop.CheckProductAmount(nonExistentProduct) > NEEDED && minPrice > shop.CheckProductPrice(nonExistentProduct))
                {
                    minPrice = shop.CheckProductPrice(nonExistentProduct);
                    shopWithMinPrice = shop;
                }
            }
            Assert.AreEqual(null, shopWithMinPrice);
        }

        [Test]
        public void BuyManyProducts_TryToBuyWhenNotEnoughMoney_CatchException()
        {
            var shop = _shopManager.AddShop("5", "eulultzx?c");
            var product1 = _shopManager.AddProduct("potato");
            var product2 = _shopManager.AddProduct("meat");
            _shopManager.AddProductToShop(product1, 1, 30, shop);
            _shopManager.AddProductToShop(product2, 10, 30, shop);
            var productsAndAmount = new Dictionary<Product, int>();
            productsAndAmount.Add(product1, 10);
            productsAndAmount.Add(product2, 5);
            int moneyBefore = _richMe.Money;
            int amount1Before = shop.CheckProductAmount(product1);
            int amount2Before = shop.CheckProductAmount(product2);
            int payment = 10 + 50;
            _shopManager.BuyProducts(shop, productsAndAmount, _richMe);
            Assert.AreEqual(moneyBefore - payment, _richMe.Money);
            Assert.AreEqual(amount1Before - 10, shop.CheckProductAmount(product1));
            Assert.AreEqual(amount2Before - 5, shop.CheckProductAmount(product2));

            Assert.Catch<ShopException>(() =>
            {
                _shopManager.BuyProducts(shop, productsAndAmount, _poorSomebody);
            });
        }
    }
}