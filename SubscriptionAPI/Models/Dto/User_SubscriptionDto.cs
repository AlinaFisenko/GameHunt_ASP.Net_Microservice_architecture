using System.ComponentModel.DataAnnotations.Schema;

namespace SubscriptionAPI.Models.Dto
{
    public class User_SubscriptionDto
    {
        public string id_user { get; set; }
        public int id_subscription { get; set; }
        public DateTime end_date { get; set; }
    }
}
