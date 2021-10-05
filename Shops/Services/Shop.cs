using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Services
{
    public class Shop
    {
        private Dictionary<Product, ProductState> _dictionaryOfProducts;
        private int _id;
        private string _address;
        private string _name;

        public Shop(int id, string address, string name)
        {
            _dictionaryOfProducts = new Dictionary<Product, ProductState>();
            _id = id;
            _address = address;
            _name = name;
        }

        public int Id
        {
            get { return _id; }
        }

        public string Address
        {
            get { return _address; }
        }

        public string Name
        {
            get { return _name; }
        }

        public bool CheckIfThereProduct(Product product)
        {
            return _dictionaryOfProducts.Keys.Any(prod => prod.Id == product.Id);
        }

        public int CheckProductAmount(Product product)
        {
            bool isThereProduct = CheckIfThereProduct(product);
            if (!isThereProduct)
                throw new ShopException("There no such product!");
            return _dictionaryOfProducts[product].Amount;
        }

        public int CheckProductPrice(Product product)
        {
            bool isThereProduct = CheckIfThereProduct(product);
            if (!isThereProduct)
                throw new ShopException("There no such product!");
            return _dictionaryOfProducts[product].Price;
        }

        internal void AddProduct(Product product, int price, int amount = 0)
        {
            bool isThereProduct = CheckIfThereProduct(product);
            if (isThereProduct)
                throw new ShopException("Shop already contains this product!");

            _dictionaryOfProducts.Add(product, new ProductState(price, amount));
        }

        internal void DeliverProducts(Product product, int amount)
        {
            bool isThereProduct = CheckIfThereProduct(product);
            if (!isThereProduct)
                throw new ShopException("Shop haven't got that product!");
            if (amount < 0)
                throw new ShopException("Negative amount!");
            int newAmount = _dictionaryOfProducts[product].Amount + amount;
            _dictionaryOfProducts[product].Amount = newAmount;
        }

        internal void SetNewPrice(Product product, int newPrice)
        {
            bool isThereProduct = CheckIfThereProduct(product);
            if (!isThereProduct)
                throw new ShopException("Shop haven't got that product!");
            _dictionaryOfProducts[product].Price = newPrice;
        }

        internal void BuyProduct(Product product, Person person, int amount)
        {
            bool isThereProduct = CheckIfThereProduct(product);
            if (!isThereProduct)
                throw new ShopException("Shop haven't got that product!");

            if (person.Money < amount * _dictionaryOfProducts[product].Price)
                throw new ShopException("Ha-ha, you poor");

            if (amount > _dictionaryOfProducts[product].Amount)
                throw new ShopException("We haven't got that amount of product!");

            int bill = amount * _dictionaryOfProducts[product].Price;
            int newAmount = _dictionaryOfProducts[product].Amount - amount;
            _dictionaryOfProducts[product].Amount = newAmount;

            person.Money = person.Money - bill;
        }

        internal void DeleteProduct(Product product)
        {
            bool isThereProduct = CheckIfThereProduct(product);
            if (!isThereProduct)
                throw new ShopException("Shop trying to delete non-existent product!");
            _dictionaryOfProducts.Remove(product);
        }
    }
}