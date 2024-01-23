using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OrderAPI.Models
{
    public class Order
    {
        [Key]
        public int id_order { get; set; }
        public string id_user { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public Genre genre { get; set; }
        [Required]
        public Platform platform { get; set; }
        [Required]
        public string description { get; set; }
        public DateTime date_created { get; set; }
        public double budget { get; set; }
        [Required]
        public  State state { get; set; }
        [Required]
        public short count_devs { get; set; }
        public TimeSpan gameplay_time { get; set; }
        public DateTime deadline { get; set; }
        public WorkCondition work_condition { get; set; }
        [Required]
        public double salary { get; set; }
        public Jobs job_title { get; set; }
    }

    public enum Jobs
    {
        Designer,
        Artist,
        Developer,
        Tester,
        Producer,
        Writer,
        Sound_Engineer,
        Animator,
        Musician,
        Publisher
    }

    public enum WorkCondition
    {
        Ready_To_Take_Disabled,
        Work_From_Home,
        Work_In_Team,
        Work_In_Office
    }

    public enum Platform
    {
        Windows,
        Android,
        IOS,
        MacOS,
        Linux,
        PS4,
        PS5,
        XBOXOne,
        XBOXSeriesX,
        Switch
    }

    public enum State
    {
        New,
        InProgress,
        Completed,
        Cancelled
    }

    public enum Genre
    {
        Action,
        Adventure,
        Fighting,
        Puzzle,
        Racing,
        RolePlaying,
        Shooter,
        Simulation,
        Sports,
        Strategy
    }
}
