using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDev.Models
{
	public class Trainee
	{
		[Key]
		public string ApplicationUserId { get; set; }

		public string School { get; set; }

		public string FullName { get; set; }

		[ForeignKey("ApplicationUserId")]
		public ApplicationUser ApplicationUser { get; set; }
	}

}
