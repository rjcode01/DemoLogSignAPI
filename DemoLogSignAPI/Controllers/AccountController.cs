using Application.IDataService;
using Application.ViewModal;
using Microsoft.AspNetCore.Mvc;

namespace DemoLogSignAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IAccountService accountService) : ControllerBase
{
	private readonly IAccountService _accountService = accountService;
	
	[HttpPost("register")]
	public async Task<ActionResult> Registration([FromBody] RegisterVM registerVM)
	{
		try
		{
			var userDetail = await _accountService.GetUserDetailByEmailAsync(registerVM.Email!);
			if (userDetail != null)
			{
				return Ok($"User with email {registerVM.Email} already exists");
			}

			await _accountService.RegisterAsync(registerVM);
			return Ok("Successfully Registered");
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPost("login")] 
	public async Task<ActionResult> Login([FromBody] LoginVM loginVm)
	{
		try
		{
			await _accountService.LoginAsync(loginVm);
			return Ok("Successful Login");
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}
