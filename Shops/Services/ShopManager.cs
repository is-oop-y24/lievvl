using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager : IShopManager
    {
        private List<Shop> _listOfShops;
        private List<Product> _listOfProducts;
        private int _nextProductId;
        private int _nextShopId;

        public ShopManager()
        {
            _listOfProducts = new List<Product>();
            _listOfShops = new List<Shop>();
            _nextShopId = 1;
            _nextProductId = 1;
        }

        public Shop AddShop(string name, string address)
        {
            var shop = new Shop(_nextShopId, address, name);
            _listOfShops.Add(shop);
            _nextShopId++;
            return shop;
        }

        public Product AddProduct(string name)
        {
            bool isThereProduct = _listOfProducts.Any(prod => prod.Name == name);
            if (isThereProduct)
                throw new ShopException("Manager already contains this product!");
            var productToAdd = new Product(name, _nextProductId);
            _listOfProducts.Add(productToAdd);
            _nextProductId++;
            return productToAdd;
        }

        public void AddProductToShop(Product product, int price, int amount, Shop shop)
        {
            bool isProductAtSystem = _listOfProducts.Any(prod => prod.Id == product.Id);
            if (!isProductAtSystem)
                throw new ShopException("Product haven't been added to system!");
            shop.AddProduct(product, price, amount);
        }

        public Product FindProduct(string name)
        {
            return _listOfProducts.SingleOrDefault(prod => prod.Name == name);
        }

        public Product GetProduct(int id)
        {
             Product product = _listOfProducts.SingleOrDefault(prod => prod.Id == id);
             if (product == null)
                 throw new ShopException("Wrong product ID!");
             return product;
        }

        public List<Shop> FindShopsContainingProduct(Product product)
        {
            return _listOfShops.Where(shop => shop.CheckIfThereProduct(product)).ToList();
        }

        public Shop GetShop(int id)
        {
            Shop searchedShop = _listOfShops.SingleOrDefault(shop => shop.Id == id);
            if (searchedShop == null)
                throw new ShopException("Wrong shop ID!");
            return searchedShop;
        }

        public void SetNewPrice(Shop shop, Product product, int newPrice)
        {
            bool isThereShop = _listOfShops.Any(sh => sh.Id == shop.Id);
            bool isThereProduct = _listOfProducts.Any(prod => prod.Id == product.Id);
            if (!(isThereShop & isThereProduct))
                throw new ShopException("There no product or shop!");
            shop.SetNewPrice(product, newPrice);
        }

        public void DeliverProduct(Shop shop, Product product, int amount)
        {
            bool isThereShop = _listOfShops.Any(sh => sh.Id == shop.Id);
            bool isThereProduct = _listOfProducts.Any(prod => prod.Id == product.Id);
            if (!(isThereShop & isThereProduct))
                throw new ShopException("There no product or shop!");
            shop.DeliverProducts(product, amount);
        }

        public void BuyProduct(Shop shop, Product product, Person person, int amount)
        {
            bool isThereShop = _listOfShops.Any(sh => sh.Id == shop.Id);
            bool isThereProduct = _listOfProducts.Any(prod => prod.Id == product.Id);
            if (!(isThereShop & isThereProduct))
                throw new ShopException("There no product or shop!");
            if (person == null)
                throw new ShopException("Non-existent human...");

            shop.BuyProduct(product, person, amount);
        }

        public void BuyProducts(Shop shop, Dictionary<Product, int> productsAndAmount, Person person)
        {
            bool isThereShop = _listOfShops.Any(sh => sh.Id == shop.Id);
            if (!isThereShop)
                throw new ShopException("Trying to buy products from non-existent shop!");
            int bill = 0;
            foreach (Product product in productsAndAmount.Keys)
            {
                bool isThereProduct = _listOfProducts.Any(prod => prod.Id == product.Id);
                if (!isThereProduct)
                    throw new ShopException("Trying to buy non-existent product at shop!");
                if (productsAndAmount[product] > shop.CheckProductAmount(product))
                    throw new ShopException("Shop don't have this amount of product!");
                bill += shop.CheckProductPrice(product) * productsAndAmount[product];
            }

            if (person.Money < bill)
                throw new ShopException("Ha-ha, you poor");

            foreach (Product product in productsAndAmount.Keys)
            {
                shop.BuyProduct(product, person, productsAndAmount[product]);
            }
        }

        public void CloseShop(Shop shop)
        {
            bool isThereShop = _listOfShops.Any(sh => sh.Id == shop.Id);
            if (!isThereShop)
                throw new ShopException("Trying to delete non-existent shop!");
            _listOfShops.Remove(shop);
        }

        public void RemoveProduct(Product product)
        {
            bool isThereProduct = _listOfProducts.Any(prod => prod.Id == product.Id);
            if (!isThereProduct)
                throw new ShopException("Trying to delete non-existent product!");
            var listOfShopContainingProduct = _listOfShops.Where(shop => shop.CheckIfThereProduct(product)).ToList();
            foreach (Shop shop in listOfShopContainingProduct)
                shop.DeleteProduct(product);
            _listOfProducts.Remove(product);
        }

        public void RemoveProductFromShop(Product product, Shop shop)
        {
            bool isThereShop = _listOfShops.Any(sh => sh.Id == shop.Id);
            if (!isThereShop)
                throw new ShopException("Trying to remove product from non-existent shop!");
            shop.DeleteProduct(product);
        }
    }
}