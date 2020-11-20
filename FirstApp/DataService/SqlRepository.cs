using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
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
            try
            {
                //for (int i = 0; i < 100; i++)
                //{ 
                return await _salesLTContext.Customers.Where(x => x.CustomerID > 0).ToListAsync();
                //}
               // return null;
            }
            catch (SqlException ex)
            {
                if (ex.Number == -2)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

    }
}
