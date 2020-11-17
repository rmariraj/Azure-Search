using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace FirstApp
{
    public class CustomerIndexModel
    { 
        [JsonPropertyName("CustomerID")]
        [JsonProperty("CustomerID")]
        [SearchableField(IsKey = true, IsFilterable = true, IsSortable = true,
            AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
        public string CustomerID { get; set; }

        [JsonPropertyName("FirstName")]
        [JsonProperty("FirstName")]
        [SearchableField(IsFilterable = true, IsFacetable = true,
           AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
        public string FirstName { get; set; }

        [JsonPropertyName("MiddleName")]
        [JsonProperty("MiddleName")]
        [SearchableField(IsFilterable = true, IsFacetable = true,
          AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
        public string MiddleName { get; set; }

        [JsonPropertyName("LastName")]
        [JsonProperty("LastName")]
        [SearchableField(IsFilterable = true, IsFacetable = true,
         AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
        public string LastName { get; set; }

        [JsonPropertyName("EmailAddress")]
        [JsonProperty("EmailAddress")]
        [SearchableField(IsFilterable = true, IsFacetable = true,
          AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
        public string EmailAddress { get; set; }


        [JsonPropertyName("ModifiedDate")]
        [JsonProperty("ModifiedDate")]
        [SearchableField(IsFilterable = true, IsFacetable = true,
          AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
        public string ModifiedDate { get; set; }




    }
}