using AppDev.Data;
using AppDev.Data.IRepository;
using AppDev.Data.Repository;
using AppDev.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AppDev.Controllers
{
	public class CategoryController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ApplicationDbContext _context;

		public CategoryController(ApplicationDbContext context, IUnitOfWork unitOfWork)
		{
			_context = context;
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			var obj = _unitOfWork.Categories.CategoryGetAll();
			return View(obj);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Category model)
		{
			if (ModelState.IsValid)
			{
				if (model.Id == 0)
				{
					_unitOfWork.Categories.Create(model);
					_context.SaveChanges();
					return RedirectToAction(nameof(Index));
				}
			}
			return View(model);
		}

		public IActionResult Update(int? Id)
		{
			var obj = _context.Categories.Find(Id);
			if (Id == null)
			{
				return NotFound();
			}
			return View(obj);
		}

		[HttpPost]
		public async Task<IActionResult> Update(int? Id, Category model)
		{
			if (model.Id != Id)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				_context.Categories.Update(model);
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

			var obj = _context.Categories.FirstOrDefault(x => x.Id.Equals(Id));
			return View(obj);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> Confirm(int? Id)
		{
			var obj = _context.Categories.Find(Id);
			_context.Categories.Remove(obj);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}
