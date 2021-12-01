using AppDev.Data;
using AppDev.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AppDev.Controllers
{
	public class AssignTraineeToCourseController : Controller
	{
		private readonly ApplicationDbContext _context;

		public AssignTraineeToCourseController(ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var obj = _context.AssignTrainees.Include(x => x.Course).Include(x => x.Trainee);

			return View(obj.ToList());
		}

		public IActionResult Create()
		{
			AssignTraineeToCourse assignTraineeToCourse = new AssignTraineeToCourse
			{
				TraineeList = _context.Trainees.ToList().Select(x => new SelectListItem
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
			return View(assignTraineeToCourse);
		}

		[HttpPost]
		public async Task<IActionResult> Create(AssignTraineeToCourse assignTraineeToCourse)
		{
			if (ModelState.IsValid)
			{
				var obj = await _context.AssignTrainees.AddAsync(assignTraineeToCourse);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View();
		}
	}
}
