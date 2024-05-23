using SecWagore;

public class SearchEntryLogVM
{
    public string? FullName { get; set; }
    public Purpose? Purpose { get; set; } = 0;

    /// <summary>
    /// 入校時間
    /// </summary>
    public DateTime? EntryTimeStart { get; set; } = null;
    public DateTime? EntryTimeEnd { get; set; } = null;
    /// <summary>
    /// 離校時間
    /// </summary>
    public DateTime? ExitTimeStart { get; set; } = null;
    public DateTime? ExitTimeEnd { get; set; } = null;
    public int? CampusId { get; set; }
}