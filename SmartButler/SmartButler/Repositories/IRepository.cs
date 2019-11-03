using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SmartButler.Models;

namespace SmartButler.Repositories
{
	public interface IRepository
	{
		Task ConfigureAsync();
	}
}
