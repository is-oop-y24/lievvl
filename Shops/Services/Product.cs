using Shops.Tools;

namespace Shops.Services
{
    public class Product
    {
        private string _name;
        private int _price;
        private int _amount;

        public Product(string name, int price, int amount)
        {
            if (amount < 0)
                throw new ShopException("Negative amount of Product!");
            _name = name;
            _price = price;
            _amount = amount;
        }

        public string Name
        {
            get { return _name; }
        }

        public int Price
        {
            get { return _price; }
        }

        public int Amount
        {
            get { return _amount; }
        }

        internal void DecreaseAmount(int buyedAmount)
        {
            if (buyedAmount > _amount)
                throw new ShopException("Buyed more than shop have!");
            if (buyedAmount < 0)
                throw new ShopException("Buyed negative amount of Products!");
            _amount -= buyedAmount;
        }

        internal void IncreaseAmount(int deliveredAmount)
        {
            if (deliveredAmount < 0)
                throw new ShopException("Delivered negative amount of products!");
            _amount += deliveredAmount;
        }

        internal void ChangePrice(int newPrice)
        {
            _price = newPrice;
        }
    }
}