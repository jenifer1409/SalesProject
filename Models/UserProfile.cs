using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SalesTransactionApp.Models
{
    public class UserProfile
    {
        [JsonProperty("id")]
        [Key]
        public Guid Id { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("logId")]
        public string LogId { get; set; }

        [JsonProperty("password")]

        public string Password { get; set; }

        [JsonProperty("role")]

        public string Role { get; set; }

        [JsonProperty("manager")]

        public string Manager { get; set; }

    }
}
