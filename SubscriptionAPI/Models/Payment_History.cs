using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubscriptionAPI.Models
{
    public class Payment_History
    {
        [Key]
        public int id { get; set; }
        public string id_user { get; set; }

        [ForeignKey("subscriptionId")]
        public ushort id_subscription { get; set; }
        public double price { get; set; }
        public DateTime date { get; set; }

        //[NotMapped]
       // public Subscription subscription { get; set; }

    }
}
