using System;
using System.Collections.Generic;

namespace SecWagore.Models
{
    public class EntryLog
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 關聯校區
        /// </summary>
        public int? CampusId { get; set; }
        /// <summary>
        /// 電話
        /// </summary>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string? FullName { get; set; }
        /// <summary>
        /// 人數
        /// </summary>
        public int? NumberOfPeople { get; set; }
        /// <summary>
        /// 受訪人
        /// </summary>
        public string Interviewee { get; set; } = null!;
        /// <summary>
        /// 事由(ENUM)
        /// </summary>
        public string Purpose { get; set; } = null!;
        /// <summary>
        /// 其他說明
        /// </summary>
        public string OtherDescription { get; set; } = null!;
        /// <summary>
        /// 備註
        /// </summary>
        public string Note { get; set; } = null!;
        /// <summary>
        /// 換證號碼
        /// </summary>
        public string ReplacementNumber { get; set; } = null!;
        /// <summary>
        /// 入校時間
        /// </summary>
        public DateTime EntryTime { get; set; }
        /// <summary>
        /// 離校時間
        /// </summary>
        public DateTime ExitTime { get; set; }
        /// <summary>
        /// 創建時間
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime? UpdateDate { get; set; }
        /// <summary>
        /// 更新人員
        /// </summary>
        public string? UpdateUser { get; set; }

        public virtual Campus? Campus { get; set; }
    }
}
