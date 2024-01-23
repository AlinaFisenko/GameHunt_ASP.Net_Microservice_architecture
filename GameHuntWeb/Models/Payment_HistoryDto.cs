using System.ComponentModel.DataAnnotations.Schema;

namespace SubscriptionAPI.Models.Dto
{
    public class Payment_HistoryDto
    {
        public string id_user { get; set; }
        public int id_subscription { get; set; }
        public double price { get; set; }
        public DateTime date { get; set; }

    }
}
