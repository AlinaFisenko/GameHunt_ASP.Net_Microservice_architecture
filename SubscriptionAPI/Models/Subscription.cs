using System.ComponentModel.DataAnnotations;

namespace SubscriptionAPI.Models
{
    public class Subscription
    {
        [Key]
        public ushort subscriptionId { get; set; }
        [Required]
        public string subscriptionTitle { get; set; }
        [Required]
        public double subscriptionPrice { get; set; }
        [Required]
        public int subscriptionDays { get; set; }
    }
}
