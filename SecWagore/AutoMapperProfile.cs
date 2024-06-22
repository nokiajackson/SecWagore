using AutoMapper;
using SecWagore.Models.ViewModel;
using SecWagore.Models;
using SecWagore;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<EntryLog, EntryLogVM>()
            .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => (Purpose)src.Purpose))
            .ReverseMap()
            .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => (byte)src.Purpose));
    }
}