using AutoMapper;
using CyberSoldierServer.Models.PlayerModels;
using CyberSoldierServer.Models.PlayerModels.Dtos.PlayerDtos;
using CyberSoldierServer.Models.PlayerModels.Dtos.PlayerSetWorldDtos;

namespace CyberSoldierServer.Profiles {
	public class MapperProfile : Profile {
		public MapperProfile() {
			CreateMap<BaseInsertDto, PlayerBase>();
			// CreateMap<PlayerWorldPathInsertDto, PlayerWorldPath>();
			CreateMap<DungeonInsertDto, BaseDungeon>();
			CreateMap<ServerCpuInsertDto, ServerCpu>();
			CreateMap<SlotInsertDto, DungeonSlot>();
			CreateMap<SlotDefenceItemInsertDto, SlotDefenceItem>();

			CreateMap<PlayerWorld, WorldDto>();
			CreateMap<PlayerWorldPath, WorldPathDto>();
			CreateMap<PathDungeon, PathDungeonDto>();
			CreateMap<ServerCpu, ServerCpuDto>();
		}
	}
}
