using CyberSoldierServer.Helpers;

namespace CyberSoldierServer.Services {
	public class ConvertErrorToCodeService : IConvertErrorToCodeService{
		public int ConvertErrorToCode(string error) {
			return error switch {
				"PasswordTooShort" => 1,
				"PasswordRequiresLower" => 2,
				"PasswordRequiresUpper" => 3,
				"InvalidEmail" => 4,
				_ => 0
			};
		}

	}
}
