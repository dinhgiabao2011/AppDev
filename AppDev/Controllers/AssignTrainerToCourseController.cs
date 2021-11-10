using AppDev.Data;
using AppDev.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AppDev.Controllers
{
	public class AssignTrainerToCourseController : Controller
	{
		private readonly ApplicationDbContext _context;

		public AssignTrainerToCourseController(ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var obj = _context.AssignTrainers.Include(x => x.Course).Include(x => x.Trainer);

			return View(obj.ToList());
		}

		public IActionResult Create()
		{
			AssignTrainerToCourse assignTrainerToCourse = new AssignTrainerToCourse
			{
				TrainerList = _context.Trainers.ToList().Select(x => new SelectListItem
				{
					Text = x.FullName,
					Value = x.ApplicationUserId.ToString()
				}),
				CourseList = _context.Courses.ToList().Select(x => new SelectListItem
				{
					Text = x.Name,
					Value = x.Id.ToString()
				})
			};
			return View(assignTrainerToCourse);
		}

		[HttpPost]
		public async Task<IActionResult> Create(AssignTrainerToCourse assignTrainerToCourse)
		{
			if (ModelState.IsValid)
			{
				var obj = await _context.AssignTrainers.AddAsync(assignTrainerToCourse);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View();
		}
	}
}
