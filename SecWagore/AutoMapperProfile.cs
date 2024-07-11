using AutoMapper;
using SecWagore.Models.ViewModel;
using SecWagore.Models;
using SecWagore;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Mapping from EntryLog to EntryLogVM
        CreateMap<EntryLog, EntryLogVM>()
            .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => MapStringToEnum(src.Purpose)));

        // Mapping from EntryLogVM to EntryLog
        CreateMap<EntryLogVM, EntryLog>()
            .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.Purpose.ToString()));
    }
    private Purpose MapStringToEnum(string purpose)
    {
        return purpose switch
        {
            "洽公" => Purpose.洽公,
            "送貨" => Purpose.送貨,
            "活動" => Purpose.活動,
            "維護" => Purpose.維護,
            "志工" => Purpose.志工,
            "面試" => Purpose.面試,
            "其他" => Purpose.其他,
            _ => throw new ArgumentException($"無效的 Purpose 值: {purpose}")
        };
    }
}