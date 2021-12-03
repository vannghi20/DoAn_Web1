using ProjectWeb1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWeb1.Interface
{
    public interface ICustomerLogic
    {
        Task<bool> Register(Customer customer);
        Task<List<Customer>> GetAllCustomer();
        Task<List<Customer>> GetCustomerById(string id);
        Task<string> UpdateCustomer(Customer food);
        Task<bool> DeleteCustomer(int Id);
    }
}
