using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.DTO.Group;
using AutoMapper;

namespace AlumniNetworkAPI.Profiles
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            // Group<->GroupReadDTO
            CreateMap<Group, GroupReadDTO>()
                // turn related users into a list of ints
                .ForMember(gdto => gdto.Users, opt => opt
                .MapFrom(g => g.Users.Select(u => u.Id).ToList()))
                // turn related posts into a list of ints
                .ForMember(gdto => gdto.Posts, opt => opt
                .MapFrom(g => g.Posts.Select(p => p.Id).ToList()))
                .ReverseMap();

            // Group<->GroupCreateDTO
            CreateMap<Group, GroupCreateDTO>().ReverseMap();
            // Group<->GroupEditDTO
            CreateMap<Group, GroupEditDTO>().ReverseMap();
        }
    }
}
