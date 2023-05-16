using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingData.ModelLayer
{
    public class Customer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        //Empty Conscructor
        public Customer() { }

        // Constructor with parameters
        public Customer(string? firstName, string? lastName, string? email, string? phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }
        //Reuses constructor with id parameter
        public Customer(int id, string? firstName, string? lastName, string? email, string? phone) : this(firstName, lastName, email, phone)
        {
            Id = id;
        }
        //Checks if customer is empty
        public bool IsCustomerEmpty
        {
            get
            {
                bool customerIsEmpty = false;
                if (String.IsNullOrWhiteSpace(FirstName) || String.IsNullOrWhiteSpace(LastName))
                {
                    customerIsEmpty = true;
                }
                return customerIsEmpty;
            }
        }

    }
}