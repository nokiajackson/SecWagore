using AutoMapper;
using SecWagore.Models.ViewModel;
using SecWagore.Models;
using SecWagore;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Mapping from EntryLogVM to EntryLog
        CreateMap<EntryLogVM, EntryLog>()
            .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => (int)src.Purpose));

        // Mapping from EntryLog to EntryLogVM
        CreateMap<EntryLog, EntryLogVM>()
            .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => (Purpose)src.Purpose));

    }
}