using AutoMapper;
using CyberSoldierServer.Dtos.EjectDtos;
using CyberSoldierServer.Dtos.InsertDtos;
using CyberSoldierServer.Models.PlayerModels;
using SlotDefenceItemInsertDto = CyberSoldierServer.Dtos.InsertDtos.SlotDefenceItemInsertDto;

namespace CyberSoldierServer.Profiles {
	public class MapperProfile : Profile {
		public MapperProfile() {
			//Insert
			CreateMap<CampInsertDto, PlayerCamp>();
			CreateMap<DungeonInsertDto, CampDungeon>();
			CreateMap<ServerCpuInsertDto, CampCpu>();
			CreateMap<SlotInsertDto, DungeonSlot>();
			CreateMap<SlotDefenceItemInsertDto, SlotDefenceItem>();
			CreateMap<WeaponInsertDto, PlayerWeapon>();
			CreateMap<ShieldInsertDto, PlayerShield>();

			//Eject
			CreateMap<Player, PlayerDto>()
				.ForMember(dto=>dto.UserName, opt=>opt.MapFrom(p=>p.User.UserName));

			CreateMap<PlayerCamp, PlayerCampDto>();

			CreateMap<PlayerWeapon, PlayerWeaponDto>()
				.ForMember(dto=>dto.BaseWeaponId, opt=>opt.MapFrom(w=>w.WeaponId))
				.ForMember(dto=>dto.PrefabId, opt=>opt.MapFrom(w=>w.Weapon.PrefabId))
				.ForMember(dto=>dto.Level, opt=>opt.MapFrom(w=>w.Weapon.Level))
				.ForMember(dto=>dto.Type, opt=>opt.MapFrom(w=>w.Weapon.Type));

			CreateMap<PlayerShield, PlayerShieldDto>()
				.ForMember(dto=>dto.BaseShieldId, opt=>opt.MapFrom(s=>s.ShieldId))
				.ForMember(dto=>dto.Level, opt=>opt.MapFrom(s=>s.Shield.Level))
				.ForMember(dto=>dto.Type, opt=>opt.MapFrom(s=>s.Shield.Type));


			CreateMap<CampDungeon, CampDungeonDto>()
				.ForMember(dto => dto.Level, opt => opt.MapFrom(d => d.Dungeon.Level))
				.ForMember(dto => dto.DungeonType, opt => opt.MapFrom(d => d.Dungeon.DungeonType))
				.ForMember(dto => dto.PrefabId, opt => opt.MapFrom(d => d.Dungeon.PrefabId))
				.ForMember(dto => dto.SlotCount, opt => opt.MapFrom(d => d.Dungeon.SlotCount))
				.ForMember(dto => dto.BaseDungeonId, opt => opt.MapFrom(d => d.DungeonId));

			CreateMap<CampCpu, CampCpuDto>()
				.ForMember(dto => dto.Level, opt => opt.MapFrom(c => c.Cpu.Level))
				.ForMember(dto => dto.BaseCpuId, opt => opt.MapFrom(c => c.CpuId))
				.ForMember(dto => dto.CpuType, opt => opt.MapFrom(c => c.Cpu.CpuType))
				;

			CreateMap<DungeonSlot, DungeonSlotDto>()
				.ForMember(dto => dto.Level, opt => opt.MapFrom(s => s.Slot.Level))
				.ForMember(dto => dto.BaseSlotId, opt => opt.MapFrom(s => s.SlotId))
				.ForMember(dto => dto.DefenceType, opt => opt.MapFrom(s => s.Slot.DefenceType));

			CreateMap<SlotDefenceItem, SlotDefenceItemDto>()
				.ForMember(dto=>dto.Level, opt=>opt.MapFrom(d=>d.DefenceItem.Level))
				.ForMember(dto=>dto.BaseDefenceItemId, opt=>opt.MapFrom(d=>d.DefenceItemId))
				.ForMember(dto=>dto.DefenceType, opt=>opt.MapFrom(d=>d.DefenceItem.DefenceType))
				.ForMember(dto=>dto.PrefabId, opt=>opt.MapFrom(d=>d.DefenceItem.PrefabId));

		}
	}
}
