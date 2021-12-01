using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDev.Data.IRepository
{
	public interface IRepository<T> where T : class
	{
		List<T> GetAll();

		void Create(T entity);

		void Remove(T entity);
	}
}
