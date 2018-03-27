using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace _6DataGridRowDeteils
{
    public enum OrderStatus
    {
        InProgress, Delivered
    }

    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsMember { get; set; }
        public OrderStatus Status { get; set; }

        private ObservableCollection<Customer> _customersList = null;
        public ObservableCollection<Customer> CustomersList
        {
            get
            {
                if (_customersList != null)
                {
                    return _customersList;
                }

                _customersList = new ObservableCollection<Customer>();

                _customersList.Add(new Customer()
                {
                    FirstName = "Jhon",
                    LastName = "Doe",
                    Email = "jhon.doe@mail.com",
                    IsMember = true,
                    Status = OrderStatus.InProgress
                });

                _customersList.Add(new Customer()
                {
                    FirstName = "Kent",
                    LastName = "Elgas",
                    Email = "k.elgas@mail.com",
                    IsMember = true,
                    Status = OrderStatus.Delivered
                });
                
                _customersList.Add(new Customer()
                {
                    FirstName = "Rea",
                    LastName = "Ostrom",
                    Email = "ostrom@mail.com",
                    IsMember = false,
                    Status = OrderStatus.Delivered
                });

                _customersList.Add(new Customer()
                {
                    FirstName = "Lupe",
                    LastName = "Campen",
                    Email = "l.campen@mail.com",
                    IsMember = false,
                    Status = OrderStatus.InProgress
                });

                return _customersList;
            }
        }
    }
}
