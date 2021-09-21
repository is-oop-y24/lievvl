using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Services
{
    public class Shop
    {
        private List<Product> _listOfProduct;
        private int _id;
        private string _address;
        private string _name;

        public Shop(int id, string address, string name)
        {
            _id = id;
            _address = address;
            _name = name;
        }

        public void AddProducts(Product product)
        {
            foreach (Product product in _listOfProduct)
            {
                if (product.)
            }
        }
    }
}