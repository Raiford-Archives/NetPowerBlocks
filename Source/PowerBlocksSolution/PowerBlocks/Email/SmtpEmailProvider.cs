using System;
using System.Net.Mail;
using PowerBlocks.Configuration;


namespace PowerBlocks.Email
{
	/// <summary>
	/// Sends the email to the SMTP server specified in the config file
	/// </summary>
	public class SmtpEmailProvider : IEmailProvider
	{
		internal readonly ILog Log = Logger.GetLogger(typeof(SmtpEmailProvider));

		/// <summary>
		/// Single method to send email to the SMTP server for delivery
		/// </summary>
		/// <param name="mailMessage"></param>
		public void Send(MailMessage mailMessage)
		{

			using (SmtpClient smtp = new SmtpClient
			(
				Settings.SmtpServer.GetString(),
				Settings.SmtpServerPort.GetInt()
			))
			{

				//smtp.Send(mailMessage);			

				// I think we will always want to send asynch. Never did this so need to test what comes back
				smtp.SendAsync(mailMessage, mailMessage);
				smtp.SendCompleted += (sender, e) =>
				{
					//Get the Original MailMessage object
					MailMessage mail = (MailMessage)e.UserState;

					//write out the subject
					string emailInfo = string.Format("From:{0} To:{1} Subject:{2}", mail.From, mail.To, mail.Subject);
					if (e.Cancelled)
					{
						Log.Warn("Email Canceled - " + emailInfo);
					}
					if (e.Error != null)
					{
						Log.Warn("Error Sending Email - " + emailInfo);
					}
					else
					{
						Log.Debug("Email Sent - " + emailInfo);
					}
				};
			}
		}


			
	}
}
