using System.Net.Mail;

namespace PowerBlocks.Email
{
	
	/// <summary>
	/// Defines and interface that sends an email to a destination source.
	/// </summary>
	public interface IEmailProvider
	{
		/// <summary>
		/// Sends the message to the configured provider
		/// </summary>
		/// <param name="mailMessage"></param>
		void Send(MailMessage mailMessage);

		
		
	}
}
