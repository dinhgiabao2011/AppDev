using AppDev.Data.IRepository;
using AppDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDev.Data.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly ApplicationDbContext _context;

		public CategoryRepository(ApplicationDbContext context) : base (context)
		{
			_context = context;
		}

		public List<Category> CategoryGetAll()
		{
			return _context.Categories.ToList();
		}

		public new void Create(Category entity)
		{
			_context.Categories.Add(entity);
		}

		public Category Get(int? Id)
		{
				return _context.Set<Category>().Find(Id);
		}

		public new List<Category> GetAll()
		{
			return _context.Set<Category>().ToList();
		}
	}
}
