using FluentValidation;

namespace CyberSoldierServer.Dtos {
	public class UserSignUpDto {
		public string UserName { get; set; }
		public string Password { get; set; }

		public string Email { get; set; }
	}

	public class UserSignValidator : AbstractValidator<UserSignUpDto> {
		public UserSignValidator() {
			RuleFor(x => x.Email).EmailAddress();
		}
	}
}
