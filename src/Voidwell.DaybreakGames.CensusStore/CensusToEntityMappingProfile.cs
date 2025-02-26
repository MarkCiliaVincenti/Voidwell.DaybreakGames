﻿using Voidwell.DaybreakGames.Census.Models;
using Voidwell.DaybreakGames.Data.Models.Planetside;

namespace Voidwell.DaybreakGames.CensusStore
{
    public class CensusToEntityMappingProfile : AutoMapper.Profile
    {
        public CensusToEntityMappingProfile()
        {
            CreateMap<CensusExperienceModel, Experience>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ExperienceId));

            CreateMap<CensusFactionModel, Faction>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.FactionId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name.English));

            CreateMap<CensusItemCategoryModel, ItemCategory>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ItemCategoryId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name.English));

            CreateMap<CensusItemModel, Item>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ItemId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name.English))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description.English));

            CreateMap<CensusMapRegionModel, MapRegion>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.MapRegionId))
                .ForMember(d => d.XPos, opt => opt.MapFrom(s => s.LocationX))
                .ForMember(d => d.YPos, opt => opt.MapFrom(s => s.LocationY))
                .ForMember(d => d.ZPos, opt => opt.MapFrom(s => s.LocationZ));

            CreateMap<CensusMapHexModel, MapHex>()
                .ForMember(d => d.XPos, opt => opt.MapFrom(s => s.X))
                .ForMember(d => d.YPos, opt => opt.MapFrom(s => s.Y));

            CreateMap<CensusFacilityLinkModel, FacilityLink>();

            CreateMap<CensusMetagameEventCategoryModel, MetagameEventCategory>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.MetagameEventId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name.English))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description.English));

            CreateMap<CensusMetagameEventStateModel, MetagameEventState>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.MetagameEventStateId));

            CreateMap<CensusProfileModel, Profile>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProfileId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name.English));

            CreateMap<CensusLoadoutModel, Loadout>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.LoadoutId));

            CreateMap<CensusVehicleModel, Vehicle>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.VehicleId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name.English))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description.English));

            CreateMap<CensusVehicleFactionModel, VehicleFaction>();

            CreateMap<CensusZoneModel, Zone>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ZoneId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name.English))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description.English));

            CreateMap<CensusWorldModel, World>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.WorldId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name.English));

            CreateMap<CensusTitleModel, Title>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.TitleId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name.English));
        }
    }
}
