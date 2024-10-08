using AutoMapper;
using WsTripsTogether.Dto.User;
using WsTripsTogether.Model;
using WsTripsTogether.Utils;

namespace WsTripsTogether.Mapper;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<UserDto, User>()
            // Password hashata
            .ForMember(x => x.Password, y => y.MapFrom(z => z.Password.ConvertToSha512()))
            .ReverseMap();
    }
}