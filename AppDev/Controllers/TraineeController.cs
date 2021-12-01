using AppDev.Data;
using AppDev.Models;
using AppDev.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AppDev.Controllers
{
	public class TraineeController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public TraineeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public IActionResult Index()
		{
			var trainees = _context.Trainees.Include(x => x.ApplicationUser).Select(x => new TraineeViewModel
			{
				School = x.School,
				Age = x.ApplicationUser.Age,
				FullName = x.FullName,
				Email = x.ApplicationUser.Email
			}).ToList();
			return View(trainees);
		}

		public IActionResult Create()
		{
			var model = new TraineeViewModel();
			return View(model);
		}

		[HttpPost]
		public IActionResult Create(TraineeViewModel model)
		{
			var user = new ApplicationUser()
			{
				UserName = model.Email,
				Email = model.Email,
				Age = model.Age,
			};

			var findRoleResult = _roleManager.FindByNameAsync("Trainee").GetAwaiter().GetResult();
			if (findRoleResult == null)
			{
				var traineeRole = new IdentityRole()
				{
					Name = "Trainee",
					NormalizedName = "TRAINEE"
				};
				_roleManager.CreateAsync(traineeRole);
			}


			var createRusult = _userManager.CreateAsync(user, model.Password).GetAwaiter().GetResult();
			if (createRusult == IdentityResult.Success)
			{
				_userManager.AddToRoleAsync(user, "Trainee").GetAwaiter().GetResult();
			}

			var trainee = new Trainee()
			{
				ApplicationUserId = user.Id,
				School = model.School,
				FullName = model.FullName
			};
			_context.Trainees.Add(trainee);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}
