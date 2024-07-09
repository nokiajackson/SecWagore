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

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<EntryLogVM, EntryLog>();
            cfg.CreateMap<EntryLog, EntryLogVM>();
        });
        var mapper = config.CreateMapper();
    }
}