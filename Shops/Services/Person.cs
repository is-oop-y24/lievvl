namespace Shops.Services
{
    public class Person
    {
        private int _money;

        public Person(int money)
        {
            _money = money;
        }

        public int Money
        {
            get { return _money; }

            internal set { _money = value;  }
        }

        public void GiveSalary(int salary)
        {
            _money += salary;
        }
    }
}