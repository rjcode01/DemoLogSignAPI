using Application.Feature;
using Application.IDataService;
using Application.IService;
using Application.ViewModal;
using Domain.Entities;
using System.Text.Json;

namespace Infrastructure.DataService;

public class AccountService(IEmailService emailService, IUnitOfWork unitOfWork) : IAccountService
{
	private readonly IEmailService _emailService = emailService;
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	public async Task LoginAsync(LoginVM loginVM)
	{
		var userDetail = await GetUserDetailByEmailAsync(loginVM.Email!);
		if (userDetail == null)
		{
			throw new UnauthorizedAccessException("Invalid email or password.");
		}

		var decryptedPassword = EncryptionFeature.Decrypt(userDetail!.Password!);

		if (decryptedPassword != loginVM.Password)
		{
			throw new UnauthorizedAccessException("Invalid email or password.");
		}

	}

	public async Task RegisterAsync(RegisterVM registerVM)
	{
		var encryptedUser = new RegisterVM
		{
			Name = EncryptionFeature.Encrypt(registerVM.Name!),
			Email = EncryptionFeature.Encrypt(registerVM.Email!),
			Password = EncryptionFeature.Encrypt(registerVM.Password!),
			Phone = EncryptionFeature.Encrypt(registerVM.Phone!)
		};

		string encryptedJson = JsonSerializer.Serialize(encryptedUser);

		User user = new()
		{
			DetailsInJson = encryptedJson
		};

		_unitOfWork.GenericRepository<User>().AddAsync(user);
		await _unitOfWork.SaveAsync();

		_emailService.SendEmail(registerVM.Email!);
	}

	public async Task<RegisterVM> GetUserDetailByEmailAsync(string email)
	{
		var users = await _unitOfWork.GenericRepository<User>().GetAllAsync();

		var userDetails = users
			.Select(ud => new
			{
				UserDetails = ud,
				registerVm = JsonSerializer.Deserialize<RegisterVM>(ud.DetailsInJson!)
			})
			.FirstOrDefault(ud =>
				EncryptionFeature.Decrypt(ud.registerVm!.Email!) == email
			);

		if (userDetails == null)
			return null;

		return userDetails!.registerVm!;
	}
}
