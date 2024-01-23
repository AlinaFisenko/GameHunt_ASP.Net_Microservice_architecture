namespace GameHuntWeb.Models
{
    public class MyRequestDto
    {
            public int id_request { get; set; }
            public int id_order { get; set; }
            public string id_from { get; set; }
            public string id_to { get; set; }
            public StateRequest state { get; set; }
            public DateTime date { get; set; }

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
