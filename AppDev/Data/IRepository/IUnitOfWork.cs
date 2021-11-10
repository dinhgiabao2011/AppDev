using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDev.Data.IRepository
{
	public interface IUnitOfWork : IDisposable
	{
		ICategoryRepository Categories { get; }

		void SaveChanges();
	}
}
