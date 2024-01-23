using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAPI.Models
{
    public class Order_Devs
	{
		[Key]
		public int id { get; set; }

		[ForeignKey("order")]
		public int id_order { get; set; }

		[ForeignKey("Id")]
		public string id_user { get; set; }

		//[NotMapped]
		//public Order order { get; set; }
	}
}
