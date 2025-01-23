using System.ComponentModel.DataAnnotations;

namespace Application.ViewModal;

public class RegisterVM
{
    public string? Name { get; set; }
	
	[EmailAddress]
	public string? Email { get; set; }
	
	public string? Password { get; set; }
	
	[Phone]
	public string? Phone { get; set; }
}
