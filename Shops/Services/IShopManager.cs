using System.Collections.Generic;

namespace Shops.Services
{
    public interface IShopManager
    {
        public Shop AddShop(string name, string address);
        public Product AddProduct(string name);
        public void AddProductToShop(Product product, int price, int amount, Shop shop);

        public Product FindProduct(string name);
        public Product GetProduct(int id);
        public List<Shop> FindShopsContainingProduct(Product product);
        public Shop GetShop(int id);

        public void SetNewPrice(Shop shop, Product product, int newPrice);

        public void DeliverProduct(Shop shop, Product product, int amount);
        public void BuyProduct(Shop shop, Product product, Person person, int amount);

        // public void BuyProducts(Shop shop, Dictionary<Product, int> productsAndAmount, Person person);
        public void CloseShop(Shop shop);
        public void RemoveProduct(Product product);
        public void RemoveProductFromShop(Product product, Shop shop);
    }
}