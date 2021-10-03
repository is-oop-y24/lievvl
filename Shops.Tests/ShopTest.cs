using Shops.Services;
using Shops.Tools;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;

namespace Shop.Tests
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
            var shop = _shopManager.AddShop("avtosalon", "gdeto");
            var product = _shopManager.AddProduct("Porsche 911 turbo S");
            _shopManager.AddProductToShop(product, 100, 2, shop);
            int moneyBefore = _richMe.Money;
            _shopManager.BuyProduct(shop, product, _richMe, 1);
            Assert.AreEqual(moneyBefore - shop.CheckProductPrice(product), _richMe.Money);
            Assert.Catch<ShopException>(() =>
            {
                _shopManager.BuyProduct(shop, product, _poorSomebody, 2);
            });
        }

        [Test]
        public void SetPriceOnProduct()
        {
            var shop = _shopManager.AddShop("lala", "lala");
            var product = _shopManager.AddProduct("lala");
            _shopManager.AddProductToShop(product, 1, 1, shop);
            _shopManager.SetNewPrice(shop, product, 2);
            Assert.AreEqual(2, shop.CheckProductPrice(product));
        }

        [Test] public void SearchMinimalPrice_HaventFound()
        {
            var shop1 = _shopManager.AddShop("5", "?");
            var shop2 = _shopManager.AddShop("6", "??");
            var product = _shopManager.AddProduct("potato");
            _shopManager.AddProductToShop(product, 3, 10, shop1);
            _shopManager.AddProductToShop(product, 2, 11, shop2);
            int NEEDED = 12;
            Shops.Services.Shop shopWithMinPrice = null;
            int minPrice = 10000;
            foreach (var shop in _shopManager.FindShopsContainingProduct(product))
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
            foreach (var shop in _shopManager.FindShopsContainingProduct(product))
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
            var nonExistentProduct = _shopManager.AddProduct("UFO");
            foreach (var shop in _shopManager.FindShopsContainingProduct(nonExistentProduct))
            {
                if (shop.CheckProductAmount(nonExistentProduct) > NEEDED && minPrice > shop.CheckProductPrice(nonExistentProduct))
                {
                    minPrice = shop.CheckProductPrice(nonExistentProduct);
                    shopWithMinPrice = shop;
                }
            }
            Assert.AreEqual(null, shopWithMinPrice);
        }


    }
}