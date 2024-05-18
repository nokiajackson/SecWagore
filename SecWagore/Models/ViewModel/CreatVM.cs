namespace SecWagore.Models.ViewModel
{
    public class CreatVM
    {
        public DateTime CreateDate { get; set; } //建立時間 (datetime, null)
        public string? UpdateUser { get; set; } //建立人員序號 (int, null)
        public DateTime UpdateDate { get; set; } //異動時間 (datetime, null)
    }
}