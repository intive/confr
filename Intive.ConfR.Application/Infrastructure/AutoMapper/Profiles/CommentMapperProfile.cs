using AutoMapper;
using Intive.ConfR.Application.Interfaces.Mapping;
using Intive.ConfR.Domain;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Domain.ValueObjects;

namespace Intive.ConfR.Application.Infrastructure.AutoMapper.Profiles
{
    public class CommentMapperProfile : IHaveCustomMapping
    {
        public void CreateMappings(Profile configuration)
        {
            // Note: Explicit string cast is a workaround for the possible bug that resulted in a null DTO email value.
            configuration.CreateMap<Comment, CommentDTO>()
                .ForMember(dst => dst.RoomEmail, opt => opt.MapFrom(src => (string)src.RoomEmail.Value))
                .ForMember(dst => dst.CommentId, opt => opt.MapFrom(src => src.CommentId));

            configuration.CreateMap<CommentDTO, Comment>()
                .ForMember(dst => dst.RoomEmail, opt => opt.MapFrom(src => (EMailAddress)src.RoomEmail))
                .ForMember(dst => dst.CommentId, opt => opt.MapFrom(src => src.CommentId));
        }
    }
}
