using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubscriptionAPI.Models
{
    public class User_Subscription
    {
        [Key]
        public int id { get; set; }
        public string id_user { get; set; }

        [ForeignKey("subscriptionId")]
        public ushort id_subscription { get; set; }
        public DateTime end_date { get; set; }

        //[NotMapped]
       //public Subscription subscription { get; set; }
    }
}
