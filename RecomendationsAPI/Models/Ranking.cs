using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecomendationsAPI.Models
{
    public class Ranking
    {
        [Key]
        public int id_ranking { get; set; }
        public string id_dev { get; set; }
        public short count_done{ get; set; }

    }
}
