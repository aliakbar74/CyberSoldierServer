using System;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;


namespace EmailService {
	public class EmailSender : IEmailSender {
		private readonly EmailConfiguration _emailConfig;

		public EmailSender(EmailConfiguration emailConfig) {
			_emailConfig = emailConfig;
		}

		public async Task SendEmail(Message message) {
			var emailMessage = CreateEmailMessage(message);
			await Send(emailMessage);
		}

		private async Task Send(MimeMessage message) {
			var path = @"smtppickup";
			if (!File.Exists(path)) {
				Directory.CreateDirectory(path);
			}

			await using var writer = new FileStream($@"smtppickup\email-{Guid.NewGuid().ToString("N")}.eml",FileMode.OpenOrCreate);
			await message.WriteToAsync(writer);
		}

		// private async Task Send(MimeMessage emailMessage) {
		// 	using var client = new SmtpClient();
		// 	try {
		// 		client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
		// 		client.AuthenticationMechanisms.Remove("ZOAUTH2");
		// 		client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
		// 		await client.SendAsync(emailMessage);
		// 	} catch (Exception e) {
		// 		Console.WriteLine(e);
		// 		throw;
		// 	} finally {
		// 		await client.DisconnectAsync(true);
		// 		client.Dispose();
		// 	}
		// }

		private MimeMessage CreateEmailMessage(Message message) {
			var mailMessage = new MimeMessage();
			mailMessage.From.Add(new MailboxAddress(_emailConfig.From));
			mailMessage.To.AddRange(message.To);
			mailMessage.Subject = message.Subject;
			mailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) {Text = message.Content};
			return mailMessage;
		}
	}
}
