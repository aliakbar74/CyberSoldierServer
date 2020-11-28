using System.Threading.Tasks;

namespace EmailService {
	public interface IEmailSender {
		Task SendEmail(Message message);
	}
}
