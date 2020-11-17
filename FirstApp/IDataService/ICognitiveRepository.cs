using System.Collections.Generic;
using System.Threading.Tasks;
using FirstApp.Model;

namespace FirstApp
{
    public interface ICognitiveRepository
    {
        public Task<List<CustomerIndexModel>> GetCongnitiveSearchCustomers(Customer customer);
    }
}