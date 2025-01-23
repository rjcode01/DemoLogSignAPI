using Application.IService;
using Infrastructure.Data;

namespace Infrastructure.Service;

public class UnitOfWork(AppDbContext context) : IUnitOfWork, IDisposable
{

	private readonly AppDbContext _context = context;

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
	private void Dispose(bool disposing)
	{
		if (disposing)
		{
			_context.Dispose();
		}
	}

	public IGenericRepository<T> GenericRepository<T>() where T : class
	{
		IGenericRepository<T> repository = new GenericRepository<T>(_context);
		return repository;
	}

	public async Task<bool> SaveAsync()
	{
		try
		{
			if (_context.SaveChanges() > 0)
				return true;
			else
				return false;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}
}
