namespace RecomendationsAPI.Models.Dto
{
    public class CommentDto
    {
        public int id_comment { get; set; }
        public int id_ranking { get; set; }
        public string id_client { get; set; }
        public string id_dev { get; set; }
        public string description { get; set; }
        public short rate { get; set; }
    }
}
