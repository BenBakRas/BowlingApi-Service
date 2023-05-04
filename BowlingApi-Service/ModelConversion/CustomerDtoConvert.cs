using System;
using BowlingData;
using BowlingApi_Service.Dtos;
using ShModel;

namespace BowlingApi_Service.ModelConversion
{
    public class CustomerDtoConvert
    {
        // Convert from Customer objects to CustomerDTO objects
        public static List<CustomerDto>? FromCustomerCollection(List<Customer> inCustomers)
        {
            List<CustomerDto>? aCustomerReadDtoList = null;
            if (inCustomers != null)
            {
                aCustomerReadDtoList = new List<CustomerDto>();
                CustomerDto? tempDto;
                foreach (Customer aCustomer in inCustomers)
                {
                    if (aCustomer != null)
                    {
                        tempDto = FromCustomer(aCustomer);
                        aCustomerReadDtoList.Add(tempDto);
                    }
                }
            }
            return aCustomerReadDtoList;
        }

        // Convert from Customer object to CustomerDTO object
        public static CustomerDto? FromCustomer(Customer inCustomer)
        {
            CustomerDto? aCustomerReadDto = null;
            if (inCustomer != null)
            {
                aCustomerReadDto = new CustomerDto(inCustomer.FirstName, inCustomer.LastName, inCustomer.Email, inCustomer.Phone);
            }
            return aCustomerReadDto;
        }

        // Convert from CustomerDTO object to customer object
        public static Customer? ToCustomer(CustomerDto inDto)
        {
            Customer? aCustomer = null;
            if (inDto != null)
            {
                aCustomer = new Customer(inDto.FirstName, inDto.LastName, inDto.Email, inDto.Phone);
            }
            return aCustomer;
        }
    }



}

