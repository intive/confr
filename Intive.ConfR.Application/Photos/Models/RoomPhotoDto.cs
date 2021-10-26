using AutoMapper;
using Intive.ConfR.Application.Interfaces.Mapping;
using Intive.ConfR.Domain.Entities;

namespace Intive.ConfR.Application.Photos.Models
{
    public class RoomPhotoDto : IHaveCustomMapping
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<RoomPhoto, RoomPhotoDto>()
                .ForMember(rpDto => rpDto.Path,
                    opt => opt.MapFrom(rp => string.IsNullOrEmpty(rp.SharedAccessSignature)  ? rp.Path : string.Concat(rp.Path, rp.SharedAccessSignature)));
        }
    }
}
