using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SmartButler.DataAccess.Models;
using SmartButler.DataAccess.Repositories;

namespace SmartButler.DataAccess.Common
{
	public abstract class BaseRepository<TEntity> where TEntity : class, new()
	{
		internal readonly RepositoryComponent Component;
		internal readonly string TableName;

		protected BaseRepository(RepositoryComponent component, string tableName)
		{
			Component = component;
			TableName = tableName;
		}

	}
}
