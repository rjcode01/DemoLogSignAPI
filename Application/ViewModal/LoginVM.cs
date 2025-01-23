using System.ComponentModel.DataAnnotations;

namespace Application.ViewModal;

public class LoginVM
{
	[EmailAddress]
	public string? Email { get; set; }
	
	public string? Password { get; set; }
}
