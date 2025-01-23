namespace Application.IService;

public interface IGenericRepository<T> : IDisposable
{
	Task<IEnumerable<T>> GetAllAsync();
	IEnumerable<T> GetAll();
	T GetById(object id);
	Task<T> GetByIdAsync(object id);
	void AddAsync(T entity);
}
