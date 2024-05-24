using System;

namespace SecWagore.Models.ViewModel
{
    public class EntryLogVM : CreatVM
    {
        public string? PhoneNumber { get; set; }=null;
        public string FullName { get; set; }
        public int NumberOfPeople { get; set; }
        //受訪人
        public string Interviewee { get; set; }
        //事由
        public Purpose Purpose { get; set; }

        public string? PurposeDesc { get; set; } = null;
        public string? OtherDescription { get; set; } = null;

        public string? Note { get; set; } = null;
        //換證號碼
        public string ReplacementNumber { get; set; }
        /// <summary>
        /// 入校時間
        /// </summary>
        public DateTime EntryTime { get; set; }
        /// <summary>
        /// 離校時間
        /// </summary>
        public DateTime? ExitTime { get; set; } = null;
        public int? CampusId { get; set; } = 0;
    }
}