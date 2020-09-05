using AutoMapper;
using CyberSoldierServer.Models.PlayerModels;
using CyberSoldierServer.Models.PlayerModels.Dtos.PlayerDtos;
using CyberSoldierServer.Models.PlayerModels.Dtos.PlayerSetWorldDtos;

namespace CyberSoldierServer.Profiles {
	public class MapperProfile : Profile {
		public MapperProfile() {
			CreateMap<WorldInsertDto, PlayerWorld>();
			CreateMap<PlayerWorldPathInsertDto, PlayerWorldPath>();
			CreateMap<PathDungeonInsertDto, PathDungeon>();
			CreateMap<ServerCpuInsertDto, ServerCpu>();

			CreateMap<PlayerWorld, WorldDto>();
			CreateMap<PlayerWorldPath, WorldPathDto>();
			CreateMap<PathDungeon, PathDungeonDto>();
			CreateMap<ServerCpu, ServerCpuDto>();
		}
	}
}
