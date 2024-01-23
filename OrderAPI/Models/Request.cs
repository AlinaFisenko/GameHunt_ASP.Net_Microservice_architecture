using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAPI.Models
{
    public class Request
    {
        [Key]
        public int id_request { get; set; }

        [ForeignKey("order")]
        public int id_order { get; set; }
        [ForeignKey("Id")]
        public string id_from { get; set; }
        [ForeignKey("Id")]
        public string id_to { get; set; }
        public StateRequest state { get; set; }
        public DateTime date { get; set; }

        public Order order { get; set; }

    }

    public enum StateRequest
    {
        Pending,
        Accepted,
        Rejected,
        Completed,
        Problems
    }
}
