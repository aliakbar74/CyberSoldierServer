using AutoMapper;
using CyberSoldierServer.Dtos.PlayerDtos;
using CyberSoldierServer.Dtos.PlayerSetWorldDtos;
using CyberSoldierServer.Models.PlayerModels;

namespace CyberSoldierServer.Profiles {
	public class MapperProfile : Profile {
		public MapperProfile() {
			CreateMap<BaseInsertDto, PlayerCamp>();
			// CreateMap<PlayerWorldPathInsertDto, PlayerWorldPath>();
			CreateMap<DungeonInsertDto, CampDungeon>();
			CreateMap<ServerCpuInsertDto, ServerCpu>();
			CreateMap<SlotInsertDto, DungeonSlot>();
			CreateMap<SlotDefenceItemInsertDto, SlotDefenceItem>();
			CreateMap<WeaponDto, PlayerWeapon>();
			CreateMap<ShieldDto, PlayerShield>();

			CreateMap<PlayerWorld, WorldDto>();
			CreateMap<PlayerWorldPath, WorldPathDto>();
			CreateMap<PathDungeon, PathDungeonDto>();
			CreateMap<ServerCpu, ServerCpuDto>();
			CreateMap<PlayerWeapon, WeaponDto>();
			CreateMap<PlayerShield, ShieldDto>();
		}
	}
}
