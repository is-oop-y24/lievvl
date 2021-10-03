using Shops.Tools;

namespace Shops.Services
{
    public class Product
    {
        private string _name;
        private int _id;

        public Product(string name, int id)
        {
            if (name == null)
                throw new ShopException("Received null instead of name");
            _name = name;
            _id = id;
        }

        public string Name
        {
            get { return _name; }
        }

        public int Id
        {
            get { return _id; }
        }
    }
}