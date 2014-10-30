using System;
using System.Net.Mail;
using log4net;
using PowerBlocks.Utilities;

namespace PowerBlocks.Email
{
	/// <summary>
	/// Dummy EmailProvider that is generally used in a test case. In your test you can simply check the 
	/// "EmailSent" property to ensure that is was sent as expected
	/// </summary>
	public class DummyEmailProvider : IEmailProvider
	{
		internal readonly ILog Log = Logger.GetLogger(typeof(DummyEmailProvider));

		/// <summary>
		/// A copy of the Email that was simulated to be sent that you can check in your test.
		/// </summary>
		public MailMessage EmailSent { get; set; }

		/// <summary>
		/// Dummy implementation of the send. This implementation write an entry to the log as a warning, but does not
		/// send a message.
		/// </summary>
		/// <param name="mailMessage"></param>
		public void Send(MailMessage mailMessage)
		{
			if (mailMessage == null)
			{
				throw new ArgumentNullException("mailMessage", "mailMessage argument is null. You must pass in a valid instance.");
			}
	
			EmailSent = mailMessage;

			// Your a Dummy so just send to the trigger a warning log.
			Log.WarnFormat("Dummy Email Sent from:{0} to:{1} subject:{2} body:{3}",
															mailMessage.From,
															mailMessage.To,
															mailMessage.Subject,
															StringHelper.TrimLengthAppendString(10, mailMessage.Body, "...."));
		}


	
	}
}
