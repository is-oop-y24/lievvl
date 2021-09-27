using System;
using System.Collections.Generic;
using System.Linq;
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

        public int Id
        {
            get { return _id; }
        }

        public string 

        internal void AddProduct(Product product)
        {
            Product isThereProduct = _listOfProduct.SingleOrDefault(iterProduct => iterProduct.Name == product.Name);
            if (isThereProduct == null)
            {
                _listOfProduct.Add(product);
            }
            else
            {
                isThereProduct.IncreaseAmount(product.Amount);
            }
        }

    }
}