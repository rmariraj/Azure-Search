using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstApp.Model;
using Microsoft.EntityFrameworkCore;

namespace FirstApp
{
    public class SqlRepository : ISqlRepository
    {
        SalesLTContext _salesLTContext;
        public SqlRepository(SalesLTContext salesLTContext)
        {
            _salesLTContext = salesLTContext;
        }

        public async Task<List<Customer>> GetCustomers(Customer customer)
        {
            return await _salesLTContext.Customers.Where(x => x.CustomerID == customer.CustomerID).ToListAsync();
        }

    }
}
