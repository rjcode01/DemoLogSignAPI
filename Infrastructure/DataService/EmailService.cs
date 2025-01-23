using Application.Feature;
using Application.IDataService;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.DataService;

public class EmailService(IConfiguration configuration) : IEmailService
{
	private readonly IConfiguration _configuration = configuration;
	
	public string SendEmail(string email)
	{
		try
		{
			string fromName = _configuration["EmailSettings:FromName"]!;
			string fromEmail = _configuration["EmailSettings:SenderEmail"]!;
			string fromPassword = _configuration["EmailSettings:SenderPassword"]!;
			string smtpServer = _configuration["EmailSettings:SmtpServer"]!;
			int smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]!);

			MailMessage mailMessage = new MailMessage
			{
				From = new MailAddress(fromEmail, fromName),
				Subject = "Test Email",
				Body = "This is a test email sent from .NET Core.",
				IsBodyHtml = false
			};

			mailMessage.To.Add(email);

			using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
			{
				smtpClient.Credentials = new NetworkCredential(fromEmail, fromPassword);
				smtpClient.EnableSsl = true;

				smtpClient.Send(mailMessage);
				return EmailStatusFeature.Success;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("An error occurred while sending the email: " + ex.Message);
		}
		return EmailStatusFeature.Failed;
	}
}
