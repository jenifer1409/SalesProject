using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using System.Text.Json.Serialization;

namespace SalesTransactionApp.Models
{
    public class SalesTransact
    {
        [JsonProperty("id")]
        [Key]
        public Guid Id { get; set; }

        [JsonProperty("item")]
        public string Item { get; set; }

        [JsonProperty("salesDate")]
        public DateTime SalesDate { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("paymentType")]

        public string PaymentType { get; set; }

        [JsonProperty("CreatedBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("CreatedDate")]

        public DateTime CreatedDate { get; set; }

        [JsonProperty("updatedBy")]
        public string UpdatedBy { get; set; }

        [JsonProperty("updatedDate")]

        public DateTime UpdatedDate { get; set; }

        

 

     }
}
