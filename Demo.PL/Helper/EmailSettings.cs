using Demo.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.Helper
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com",587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("nanorules714@gmail.com", "mckw pftm twde abac");
			client.Send("nanorules714@gmail.com", email.To, email.Subject, email.Body);

		}
	}
}
