using Application.IService;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service;

public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
{
	private readonly AppDbContext _context;
	private DbSet<T> table;

	public GenericRepository(AppDbContext context)
	{
		_context = context;
		table = _context.Set<T>();
	}

	public async void AddAsync(T entity)
	{
		await table.AddAsync(entity);
	}

	public async Task<IEnumerable<T>> GetAllAsync()
	{
		return await table.ToListAsync();
	}

	public IEnumerable<T> GetAll()
	{
		return table.ToList();
	}

	public T GetById(object id)
	{
		try
		{
			return table.Find(id);
		}
		catch (Exception ex)
		{
			throw;
		}
	}

	public async Task<T> GetByIdAsync(object id)
	{
		try
		{
			return await table.FindAsync(id);
		}
		catch (Exception ex)
		{
			throw;
		}
	}

	private bool disposed = false;
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	private void Dispose(bool disposing)
	{
		if (!this.disposed)
		{
			if (disposing)
			{
				_context.Dispose();
			}

		}
		this.disposed = true;
	}
}
