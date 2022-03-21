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
                // include sender name
                .ForMember(pdto => pdto.SenderName, opt => opt
                .MapFrom(p => p.Sender.Name))
                // include sender picture url
                .ForMember(pdto => pdto.SenderPictureURL, opt => opt
                .MapFrom(p => p.Sender.PictureURL))
                .ReverseMap();

            // Post<->PostCreateDTO
            CreateMap<Post, PostCreateDTO>().ReverseMap();
            // Post<->PostEditDTO
            CreateMap<Post, PostEditDTO>().ReverseMap();
        }
    }
}
