﻿using AppDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDev.Data.IRepository
{
	public interface ICategoryRepository : IRepository<Category>
	{
		List<Category> CategoryGetAll();

		Category Get(int? Id);
	}
}
