using System.ComponentModel.DataAnnotations;

namespace GameHuntWeb.Models.Dto
{
    public class RankingDto
    {
        public int id_ranking { get; set; }
        public string id_dev { get; set; }
        public short count_done { get; set; }
    }
}
