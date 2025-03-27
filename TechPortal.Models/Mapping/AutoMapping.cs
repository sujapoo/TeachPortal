using AutoMapper;


namespace TechPortal.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TechPortal.Models.Models.Student, PersonDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.TeacherId, opt => opt.MapFrom(src => src.TeacherId));
        }
    }
}
