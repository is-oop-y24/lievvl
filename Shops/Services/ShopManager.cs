using System.Collections.Generic;
using System.Linq;

namespace Shops.Services
{
    public class ShopManager : IShopManager
    {
        private List<Shop> _listOfShops;
        private int _nextId;

        public ShopManager()
        {
            _nextId = 1;
        }

        public Shop AddShop(string name, string address)
        {
            var shop = new Shop(_nextId, address, name);
            _listOfShops.Add(shop);
            _nextId++;
            return shop;
        }

        public Product AddProduct(Shop shop, string name, int price, int amount = 0)
        {
            var product = new Product(name, price, amount);
            shop.AddProduct(product);
            return product;
        }

        public List<Shop> FindShops(string name)
        {
            _listOfShops.Select(shop => )
        }
    }
}