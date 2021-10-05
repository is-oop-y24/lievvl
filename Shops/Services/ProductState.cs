using Shops.Tools;

namespace Shops.Services
{
    public class ProductState
    {
        private int _price;
        private int _amount;

        public ProductState(int price, int amount)
        {
            Price = price;
            Amount = amount;
        }

        public int Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (value < 0)
                    throw new ShopException("Price cannot be negative!");
                _price = value;
            }
        }

        public int Amount
        {
            get
            {
                return _amount;
            }

            set
            {
                if (value < 0)
                    throw new ShopException("Amount cannot be negative!");
                _amount = value;
            }
        }
    }
}