using AutoMapper;
using HitecService.Data.Database.Models;
using HitecService.Models.DTO.MenuConfiguration;
using HitecService.Models.DTO.Privileges;
using HitecService.Models.DTO.Rol;
using HitecService.Models.DTO.User;
using HitecService.Models.Requests.Privileges;
using HitecService.Models.Requests.Rol;
using HitecService.Models.Requests.User;

namespace HitecService.Core.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        MapDto();
    }

    private void MapDto()
    {
        CreateMap<User, UserDto>()
            .ForMember(d => d.RolName, map => map.MapFrom(s => s.Rol.Name));

        CreateMap<Role, RoleDto>();
        CreateMap<MenuConfiguration, MenuConfigurationDto>();
        CreateMap<Privilege, PrivilegesDto>();

        CreateMap<MenuConfiguration, PrivilegeMenuConfigurationDto>()
            .ForMember(d => d.MenuConfigurationId, map => map.MapFrom(s => s.Id))
            .ForMember(d => d.PrivilegeId, map => map.Ignore())
            .ForMember(d => d.CanCreate, map => map.Ignore())
            .ForMember(d => d.CanDelete, map => map.Ignore())
            .ForMember(d => d.CanRead, map => map.Ignore())
            .ForMember(d => d.CanEdit, map => map.Ignore());

        CreateMap<RequestCreateUser, User>()
            .ForMember(d => d.Id, map => map.Ignore())
            .ForMember(d => d.UpdatedAt, map => map.Ignore())
            .ForMember(d => d.CreatedAt, map => map.Ignore());

        CreateMap<RequestRole, Role>()
            .ForMember(d => d.Id, map => map.Ignore())
            .ForMember(d => d.UpdatedAt, map => map.MapFrom(s => DateTime.Now))
            .ForMember(d => d.CreatedAt, map => map.MapFrom(s => DateTime.Now));


        CreateMap<RequestPrivilege, Privilege>()
            .ForMember(d => d.Id, map => map.Ignore())
            .ForMember(d => d.UpdatedAt, map => map.MapFrom(s => DateTime.Now))
            .ForMember(d => d.CreatedAt, map => map.MapFrom(s => DateTime.Now));

    }
}