namespace Shops.Models
{
    public class Person
    {
        public Person(string personName, uint money)
        {
            PersonName = personName;
            Money = money;
        }

        public string PersonName
        {
            get;
        }

        public uint Money
        {
            get;
            set;
        }
    }
}