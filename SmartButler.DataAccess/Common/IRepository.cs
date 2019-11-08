using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SmartButler.DataAccess.Models;

// https://kudchikarsk.com/repository-pattern-csharp/

namespace SmartButler.DataAccess.Common
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task ConfigureAsync(IEnumerable<TEntity> items);
		Task InsertAsync(TEntity item);
		Task UpdateAsync(TEntity item);

		Task DeleteAsync(TEntity item);
		Task DeleteAsync(int id);

		Task<TEntity> GetAsync(int id);
		Task<List<TEntity>> GetAllAsync();
		Task<List<TEntity>> GetAllAvailableAsync();
	}
}
