using AppDev.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDev.Data.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
			Categories = new CategoryRepository(_context);
		}

		public ICategoryRepository Categories { get; private set; }

		public void Dispose()
		{
			_context.Dispose();
		}

		public void SaveChanges()
		{
		  _context.SaveChanges();
		}
	}
}
