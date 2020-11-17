using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Search.Documents;
using FirstApp.Model;

namespace FirstApp
{
    public class CognitiveRepository:ICognitiveRepository
    { 
        private readonly SearchClient _searchClient;
        public CognitiveRepository(SearchClient searchClient)
        {
            _searchClient = searchClient;
        }

        public async Task<List<CustomerIndexModel>> GetCongnitiveSearchCustomers(Customer customer)
        {
            var filterList = new List<string>();

            filterList.Add($"FirstName eq '{customer.FirstName}'");

            var searchOptions = new SearchOptions
            {
                Filter = string.Join(" and ", filterList),
                IncludeTotalCount = false,
                Size = 10,
                Skip = 0
            };

            var searchResults = await _searchClient.SearchAsync<CustomerIndexModel>(string.Empty, searchOptions);
            var documentList = searchResults.Value.GetResults()
                .Select(item => item.Document)
                .ToList();
            return documentList;
        }
         
    }
}