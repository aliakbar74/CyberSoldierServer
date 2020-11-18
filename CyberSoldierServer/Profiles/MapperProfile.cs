using AutoMapper;
using CyberSoldierServer.Dtos.PlayerDtos;
using CyberSoldierServer.Dtos.PlayerSetWorldDtos;
using CyberSoldierServer.Models.PlayerModels;

namespace CyberSoldierServer.Profiles {
	public class MapperProfile : Profile {
		public MapperProfile() {
			CreateMap<CampInsertDto, PlayerCamp>();
			CreateMap<DungeonInsertDto, CampDungeon>();
			CreateMap<ServerCpuDto, ServerCpu>();
			CreateMap<SlotInsertDto, DungeonSlot>();
			CreateMap<SlotDefenceItemInsertDto, SlotDefenceItem>();
			CreateMap<WeaponDto, PlayerWeapon>();
			CreateMap<ShieldDto, PlayerShield>();

			CreateMap<PlayerCamp, CampInsertDto>();
			CreateMap<CampDungeon, DungeonInsertDto>();
			CreateMap<ServerCpu, ServerCpuDto>();
			CreateMap<DungeonSlot, SlotInsertDto>();
			CreateMap<SlotDefenceItem, SlotDefenceItemInsertDto>();
			CreateMap<PlayerWeapon, WeaponDto>();
			CreateMap<PlayerShield, ShieldDto>();
			CreateMap<Player, PlayerDto>();
		}
	}
}
