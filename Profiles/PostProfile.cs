using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.DTO.Post;
using AutoMapper;

namespace AlumniNetworkAPI.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            // Post<->PostReadDTO
            CreateMap<Post, PostReadDTO>()
                // turn related replies into a list of ints
                .ForMember(pdto => pdto.Replies, opt => opt
                .MapFrom(p => p.Replies.Select(r => r.Id).ToList()))
                .ReverseMap();

            // Post<->PostCreateDTO
            CreateMap<Post, PostCreateDTO>().ReverseMap();
            // Post<->PostEditDTO
            CreateMap<Post, PostEditDTO>().ReverseMap();
        }
    }
}
