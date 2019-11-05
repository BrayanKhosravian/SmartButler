using System;
using System.Threading.Tasks;
using SQLite;

namespace SmartButler.DataAccess.Repositories
{
	public interface IBottlesRepository
	{
	}

	public class BottlesRepository : IBottlesRepository
    {
	    public RepositoryComponent RepositoryComponent { get; }

	    public BottlesRepository(RepositoryComponent repositoryComponent)
	    {
		    RepositoryComponent = repositoryComponent;
	    }

	    public Task ConfigureAsync()
        {
	        throw new NotImplementedException();
        }
    }
}
