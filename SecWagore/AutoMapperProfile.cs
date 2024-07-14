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
            .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => (Purpose)src.Purpose));

        // Mapping from EntryLogVM to EntryLog
        CreateMap<EntryLogVM, EntryLog>()
            .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => (int)src.Purpose));
    }
    private Purpose MapStringToEnum(string purpose)
    {
        if (Enum.TryParse<Purpose>(purpose, out var result))
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
        else
        {
            throw new ArgumentException($"無效的 Purpose 值: {purpose}");
        }
    }
}