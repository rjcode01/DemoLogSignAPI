using Application.ViewModal;
using Domain.Entities;

namespace Application.IDataService;

public interface IAccountService
{
	Task RegisterAsync(RegisterVM registerVM);
	Task LoginAsync(LoginVM loginVM);
	Task<RegisterVM> GetUserDetailByEmailAsync(string email);
}
