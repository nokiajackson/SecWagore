using System;

namespace SecWagore.Models.ViewModel
{
    public class EntryLogVM
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
        public DateTime? ExitTime { get; set; }
        /// <summary>
        /// 創建時間
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime? UpdateDate { get; set; }
    }
}