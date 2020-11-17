using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstApp.Model;

namespace FirstApp
{
    public interface ISqlRepository
    {
        public Task<List<Customer>> GetCustomers(Customer customer);
    }
}
