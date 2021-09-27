using System.Collections.Generic;

namespace Shops.Services
{
    public interface IShopManager
    {
        public Shop AddShop(string name, string address);
        public Product AddProduct(Shop shop, string name, int price, int amount = 0);

        public Product FindProductAtShop(Shop shop, string name);
        public List<Shop> FindShops(string name);
        public Shop GetShop(int id);

        public void SetNewPrice(Product product, int newPrice);
        public void SetNewPrice(Shop shop, string name, int newPrice);

        public void DeliverProduct(Shop shop, string name, int amount);
        public void BuyProduct(Product product);
        public void CloseShop(Shop shop);
    }
}