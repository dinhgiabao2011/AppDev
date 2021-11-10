using AppDev.Data;
using AppDev.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AppDev.Controllers
{
	public class CourseController : Controller
	{
		private readonly ApplicationDbContext _context;

		public CourseController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var obj = _context.Courses.Include(x=>x.Category);
			return View(obj.ToList());
		}

		public IActionResult Create()
		{
			//List<Category> categories = _context.Categories.ToList();
			//SelectList CategoryList = new SelectList(categories, "Id", "Name");
			//ViewBag.categoryList = CategoryList;
			Course course = new Course
			{
				CategoryList = _context.Categories.ToList().Select(x => new SelectListItem
				{
					Text = x.Name,
					Value = x.Id.ToString()
				})
			};
			return View(course);
		}

		[HttpPost]
		public async Task<IActionResult> Create(Course model)
		{
			if (ModelState.IsValid)
			{
				if (model.Id == 0)
				{
					_context.Add(model);
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(Index));
				}
			}
			return View(model);
		}

		public IActionResult Update(int? Id)
		{
			Course course = new Course
			{
				CategoryList = _context.Categories.ToList().Select(x => new SelectListItem
				{
					Text = x.Name,
					Value = x.Id.ToString()
				})
			};
			var obj = _context.Courses.Find(Id);
			if (Id == null)
			{
				return NotFound();
			}
			return View(course);
		}

		[HttpPost]
		public async Task<IActionResult> Update(int? Id, Course model)
		{
			if (model.Id != Id)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				_context.Courses.Update(model);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}

		public IActionResult Delete(int? Id)
		{
			if (Id == null)
			{
				return NotFound();
			}

			var obj = _context.Courses.FirstOrDefault(x => x.Id.Equals(Id));
			return View(obj);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> Confirm(int? Id)
		{
			var obj = _context.Courses.Find(Id);
			_context.Courses.Remove(obj);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}
