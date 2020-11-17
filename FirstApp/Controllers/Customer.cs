using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FirstApp.Model;
using Azure.Search.Documents;

namespace FirstApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Customer : ControllerBase
    {
        ISqlRepository _sqlRepository;
        ICognitiveRepository _cognitiveRepository;

        private readonly SearchClient _searchClient;
        public Customer(ISqlRepository sqlRepository, SearchClient searchClient, ICognitiveRepository cognitiveRepository)
        {
            _sqlRepository = sqlRepository;
            //_searchClient = searchClient;
            _cognitiveRepository = cognitiveRepository;
        }
        [Route("search")]
        [HttpPost]
        public async Task<IActionResult> GetCustomers([FromBody] Model.Customer customer)
        {
            var res = await _sqlRepository.GetCustomers(customer);
            return Ok(res);
        }
        [Route("CongnitiveSearch")]
        [HttpPost]
        public async Task<IActionResult> GetCongnitiveSearchCustomers([FromBody] Model.Customer customer)
        { 
            var documentList = await  _cognitiveRepository.GetCongnitiveSearchCustomers(customer);
            return Ok(documentList);
        }
    }
}
