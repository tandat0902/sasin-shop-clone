using Microsoft.Identity.Client;

namespace api_sasin.Models
{
    public class Accounts
    {
        public string AccountId { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime DateCreated { get; set;}
        public string Salt { get; set; }
        public bool Active { get; set; }
    }
}
