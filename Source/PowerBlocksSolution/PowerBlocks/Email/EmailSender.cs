using System;
using System.Text.RegularExpressions;
using System.Net.Mail;
using log4net;
using System.Diagnostics.CodeAnalysis;
using PowerBlocks.IocContainers;
using PowerBlocks.Configuration;

namespace PowerBlocks.Email
{


	#region Exception Classes
	/// <summary>
	/// Exception raised by the EmailSender class
	/// </summary>
	[Serializable]
	public class EmailSenderException : Exception
	{
		private readonly MailMessage _mailMessage = null;
		
		/// <summary>
		/// The EmailMessage object that is associated with this exception.
		/// </summary>
		public MailMessage MailMessage { get { return _mailMessage; } }

		/// <summary>
		/// Constructs and EmailSenderException with optional arguments
		/// </summary>
		/// <param name="message"></param>
		/// <param name="innerException"></param>
		/// <param name="mailMessage"></param>
		public EmailSenderException(string message, Exception innerException = null, MailMessage mailMessage = null)
			: base(message, innerException)
		{
			_mailMessage = mailMessage;
		}
	}
	#endregion

	
	/// <summary>
	/// Used to send emails.
	/// </summary>
	public class EmailSender
	{
		#region Private Fields	
		// @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$"; this is an alternative that did not seem as complete as below
		internal const string EmailRegularExpression = @"^[A-Za-z0-9._%+-]+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";
		internal const char EmailAddressDelimiter = ';';
		private readonly IEmailProvider _emailProvider;
		internal readonly ILog Log = Logger.GetLogger(typeof(EmailSender));
		#endregion

		#region Constructor (s)
		/// <summary>
		/// Default constructor will use the configured email provider.
		/// </summary>
		public EmailSender() : this(IocContainer.Get<IEmailProvider>())
		{		
		}

		/// <summary>
		/// Constructor allows you to pass in a custom email provider. This can be useful in a Unit Test for example
		/// where you do not want to send emails 'really'.
		/// </summary>
		/// <param name="emailProvider"></param>
		public EmailSender(IEmailProvider emailProvider)
		{
			_emailProvider = emailProvider;
		}
		#endregion

		#region Static Helper Methods

		/// <summary>
		/// Checks for a valid email address. If you need more validation this method can be enhanced as required.
		/// </summary>
		/// <param name="emailAddress"></param>
		/// <returns></returns>
		private static bool IsValidEmail(string emailAddress)
		{
			bool valid = Regex.IsMatch(emailAddress, EmailRegularExpression);
			return valid;
		}
		#endregion

		#region Send Methods

		/// <summary>
		/// Sends an email with basic send options. The "to" parameter can contain
		/// a semi-colon delimited list of emails to send to multiple recipients.
		/// </summary>
		/// <param name="to">
		///	A Single email address or a semi-colon list of email addresses.
		/// </param>
		/// <param name="from"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		
		public void Send(string to, string from, string subject, string body)
		{
			#region Validation
			if (string.IsNullOrWhiteSpace(to))
			{
				throw new EmailSenderException("Must provide a TO email address");
			}
			if (string.IsNullOrWhiteSpace(from))
			{
				throw new EmailSenderException("Must provide a FROM email address");
			}
			#endregion

			// Convert to Mail Objects
			MailAddressCollection toAddresses= ParseMultipleAddress(to);
			MailAddress fromAddress = new MailAddress(from);
			
			Send(toAddresses,fromAddress, subject, body);
		}

		/// <summary>
		/// Sends an email
		/// </summary>
		/// <param name="toMailAddresses"></param>
		/// <param name="fromMailAddress"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		public void Send(MailAddressCollection toMailAddresses, MailAddress fromMailAddress, string subject, string body)
		{
			Send(toMailAddresses, fromMailAddress, subject, body, false);
		}

		/// <summary>
		/// Sends an email with an HTML option by settings the htmlFormat flag
		/// </summary>
		/// <param name="toMailAddresses"></param>
		/// <param name="fromMailAddress"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		/// <param name="htmlFormat"></param>
		public void Send(MailAddressCollection toMailAddresses, MailAddress fromMailAddress, string subject, string body, bool htmlFormat)
		{
			#region Validation
			if (toMailAddresses == null || toMailAddresses.Count < 1)
			{
				throw new EmailSenderException("To Email is Null or Empty, you must specify a valid entry. ");
			}
			if (fromMailAddress == null)
			{
				throw new EmailSenderException("From Email is Empty, you must specify a valid entry. ");
			}
			#endregion
			
			MailMessage message = new MailMessage
			{
				From = fromMailAddress,
				Subject = subject,
				Body = body,
				IsBodyHtml = htmlFormat,
				Priority = MailPriority.Normal
			};

			// Copy all the mail to's
			foreach (MailAddress ma in toMailAddresses)
			{
				message.To.Add(ma);
			}

			// Send it now!!!
			Send(message);

			Log.Debug(string.Format(@"Email Sent - From:{0} To:{1} Subject:{2}", message.From, MailAddressCollectionToDelimitedString(message.To), subject));
		}

		/// <summary>
		/// Send an email using formatted as html
		/// </summary>
		/// <param name="to"></param>
		/// <param name="from"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification="Could not find a reason why this warning is being generated")]
		public void SendHtml(string to, string from, string subject, string body)
		{

				//MailMessage message = new MailMessage()
				//{
				//    From = new MailAddress(from),
				//    Subject = subject,
				//    Body = body,
				//    IsBodyHtml = true,
				//    Priority = MailPriority.Normal
				//};

			using (MailMessage message = new MailMessage
			{ 
				From = new MailAddress(from),
				Subject = subject,
				Body = body,
				IsBodyHtml = true,
				Priority = MailPriority.Normal })
			{




				// Copy all the mail to's
				MailAddressCollection toAddresses = ParseMultipleAddress(to);
				foreach (MailAddress ma in toAddresses)
				{
					message.To.Add(ma);
				}

				// Send Her!
				Send(message);
			}
		}
		
		/// <summary>
		/// This is the lowest level Send() that defers to the email provider to do the actual send. This method should be used if you need
		/// full control over how the email is sent
		/// </summary>
		/// <param name="mailMessage"></param>
		public void Send(MailMessage mailMessage)
		{
			try
			{
				//////////////////////////////////////////////////////////////////////////////////////////
				//NOTE: This is the only location where we actually call down to the actuall EmailProvider
				//////////////////////////////////////////////////////////////////////////////////////////

				_emailProvider.Send(mailMessage);
			}
			catch (Exception ex)
			{
				throw new EmailSenderException(ex.Message, ex, mailMessage);
			}
		}

		#endregion

		#region Custom Send Methods

		/// <summary>
		/// Sends Email to the configured debug address. This will generally be used in development and staging to be notified of 
		/// remote errors as they occur.
		/// </summary>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		public void SendDebugMessage(string subject, string body)
		{
			Send(Settings.EmailAddressDebugNotifications.GetString(),
				 Settings.SmtpServerFromEmailAddress.GetString(),
				 subject,
				 body);
		}
		
		/// <summary>
		/// Sends Email using the Exception formatted in the body
		/// </summary>
		/// <param name="exception"></param>
		public void SendDebugErrorExceptionMessage(Exception exception)
		{		
			if (exception == null)
			{
				throw new ArgumentNullException("exception", "exception argument is null. You must pass in a valid instance.");
			}
			
			string subject = "Error in " + exception.Source + " : " + exception.TargetSite;
			string body = exception.ToString();
			SendDebugMessage(subject, body);
		}	
		#endregion

		#region Helper Methods

		/// <summary>
		/// Takes a semi colon delimited list of email addresses and parses it into
		/// a MailAddress object
		/// </summary>
		/// <param name="delimitedEmailList"></param>
		/// <returns></returns>
		public MailAddressCollection ParseMultipleAddress(string delimitedEmailList)
		{
			MailAddressCollection collection = new MailAddressCollection();

			if (string.IsNullOrWhiteSpace(delimitedEmailList))
			{
				return collection;
			}

			string[] items = delimitedEmailList.Split(EmailAddressDelimiter);
			
			foreach(string e in items)
			{
				string emailAddress = e.Trim();
				
				// you might have an extra empty entry so just continue
				if(string.IsNullOrEmpty(emailAddress))
					continue;

				// Validate it
				if(!IsValidEmail(emailAddress))
				{
					string error = string.Format("Invalid Email Address: {0} This email will not be added to the collections.", emailAddress);
					throw new EmailSenderException(error);
				}

				collection.Add(new MailAddress(emailAddress));
			}

			return collection;
		}

		/// <summary>
		/// Converts a MailAddressCollection to a semicolon delimited string
		/// </summary>
		/// <param name="addresses"></param>
		/// <returns></returns>
		public string MailAddressCollectionToDelimitedString(MailAddressCollection addresses)
		{
			if (addresses == null)
			{
				return string.Empty;
			}

			// ReSharper disable LoopCanBeConvertedToQuery
			string stringValue = string.Empty;
			foreach (var address in addresses)
			{
				stringValue += address.Address + EmailAddressDelimiter.ToString();
			}
			// ReSharper restore LoopCanBeConvertedToQuery
			return stringValue;
		}
		
		#endregion

	}
}