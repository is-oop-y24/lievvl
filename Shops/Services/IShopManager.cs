using System.Collections.Generic;

namespace Shops.Services
{
    public interface IShopManager
    {
        Shop AddShop(string name, string address);
        Product AddProduct(string name);
        void AddProductToShop(Product product, int price, int amount, Shop shop);

        Product FindProduct(string name);
        Product GetProduct(int id);
        List<Shop> FindShopsContainingProduct(Product product);
        Shop GetShop(int id);

        void SetNewPrice(Shop shop, Product product, int newPrice);
        void DeliverProduct(Shop shop, Product product, int amount);

        void BuyProduct(Shop shop, Product product, Person person, int amount);
        void BuyProducts(Shop shop, Dictionary<Product, int> productsAndAmount, Person person);

        void CloseShop(Shop shop);
        void RemoveProduct(Product product);
        void RemoveProductFromShop(Product product, Shop shop);
    }
}