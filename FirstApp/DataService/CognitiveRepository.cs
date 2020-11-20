using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using FirstApp.Model;
using Microsoft.Azure.Search;

namespace FirstApp
{
    public class CognitiveRepository:ICognitiveRepository
    { 
        private readonly SearchClient _searchClient;
        private readonly SearchServiceClient _searchServiceClient;
        public CognitiveRepository(SearchClient searchClient, SearchServiceClient searchServiceClient)
        {
            _searchClient = searchClient;
            _searchServiceClient = searchServiceClient;
        }

        public async Task<List<CustomerIndexModel>> GetCongnitiveSearchCustomers(Customer customer)
        {
            try
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

                //var searchr = _searchServiceClient.
                //_searchServiceClient.SetRetryPolicy
                var searchResults = await _searchClient.SearchAsync<CustomerIndexModel>(string.Empty, searchOptions);
                var documentList = searchResults.Value.GetResults()
                    .Select(item => item.Document)
                    .ToList();
                return documentList;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
         
    }
}