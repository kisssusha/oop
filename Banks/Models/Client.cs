using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Backups.Tools;
using Banks.Servises;
using Banks.Tools;

namespace Banks.Models
{
    public class Client
    {
        private static int _id = 0;
        public Client(string name, string surname, string address, string passport)
        {
            Id = _id++;
            Name = name ?? throw new BackupsException("Invalid name");
            Surname = surname ?? throw new BackupsException("Invalid surname");
            Address = address;
            Passport = passport;
        }

        public int Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Address { get; }
        public string Passport { get; }

        public static void Notification(object sender, double newParameter)
        {
        }

        public void SubscriptionsToNewsletter(Bank bank)
        {
            bank.ChangeParametersOfBank += Notification;
        }
    }
}