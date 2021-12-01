using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDev.Models
{
	public class AssignTraineeToCourse
	{
		public int Id { get; set; }

		public string TraineeId { get; set; }

		[ForeignKey("TraineeId")]
		public Trainee Trainee { get; set; }

		public int CourseId { get; set; }
		public Course Course { get; set; }

		public DateTime CreateAt { get; set; }

		[NotMapped]
		public IEnumerable<SelectListItem> TraineeList { get; set; }

		[NotMapped]
		public IEnumerable<SelectListItem> CourseList { get; set; }

		public AssignTraineeToCourse()
		{
			CreateAt = DateTime.Now;
		}
	}
}
