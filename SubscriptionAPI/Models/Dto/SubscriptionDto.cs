namespace SubscriptionAPI.Models.Dto
{
    public class SubscriptionDto
    {
        public ushort subscriptionId { get; set; }
        public string subscriptionTitle { get; set; }
        public double subscriptionPrice { get; set; }
        public int subscriptionDays { get; set; }
    }
}
