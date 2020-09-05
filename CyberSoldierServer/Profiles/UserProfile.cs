using AutoMapper;
using CyberSoldierServer.Dtos;
using CyberSoldierServer.Models;
using CyberSoldierServer.Models.Auth;

namespace CyberSoldierServer.Profiles {
	public class UserProfile : Profile {
		public UserProfile() {
			CreateMap<UserSignUpDto, AppUser>();
			CreateMap<AppUser, UserSignUpDto>();
		}
	}
}
