using AppDev.Models;
using Microsoft.EntityFrameworkCore;

namespace AppDev.Data.Seed
{
	public static class Seeding
	{
		public static void Seed(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().HasData(
									new Category
									{
										Id = 1,
										Name = "ASP.NET",
										Description = "Welcome to class ASP.NET"
									});



		}
	}
}
