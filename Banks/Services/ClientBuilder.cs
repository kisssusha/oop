using System;
using Banks.Models;
using Banks.Tools;

namespace Banks.Servises
{
    public class ClientBuilder
    {
        private string _name;
        private string _passport;
        private string _surname;
        private string _address;

        public ClientBuilder ChangeName(string name)
        {
            _name = name ?? throw new BanksException("Invalid name");
            return this;
        }

        public ClientBuilder ChangeSurname(string surname)
        {
            _surname = surname ?? throw new BanksException("Invalid surname");
            return this;
        }

        public ClientBuilder ChangePassport(string passport)
        {
            _passport = passport ?? throw new BanksException("Invalid passport");
            return this;
        }

        public ClientBuilder ChangeAddress(string address)
        {
            _address = address ?? throw new BanksException("Invalid address");
            return this;
        }

        public Client Create()
        {
            return new Client(_name, _surname, _address, _passport);
        }
    }
}