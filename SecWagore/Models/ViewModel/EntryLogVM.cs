using System;

namespace SecWagore.Models.ViewModel
{
    public class EntryLogVM : CreatVM
    {
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public int NumberOfPeople { get; set; }
        public string Interviewee { get; set; }
        public string Purpose { get; set; }
        public string OtherDescription { get; set; }
        public string Note { get; set; }
        public string ReplacementNumber { get; set; }
        /// <summary>
        /// 入校時間
        /// </summary>
        public DateTime EntryTime { get; set; }
        /// <summary>
        /// 離校時間
        /// </summary>
        public DateTime? ExitTime { get; set; } = null;
        public int CampusId { get; set; }
    }
}