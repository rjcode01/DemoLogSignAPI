namespace Application.IService;

public interface IUnitOfWork
{
	IGenericRepository<T> GenericRepository<T>() where T : class;
	Task<bool> SaveAsync();
}
