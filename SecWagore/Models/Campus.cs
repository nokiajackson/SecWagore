using System;
using System.Collections.Generic;

namespace SecWagore.Models
{
    public partial class Campus
    {
        public Campus()
        {
            Accounts = new HashSet<Account>();
            EntryLogs = new HashSet<EntryLog>();
        }

        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 校區名
        /// </summary>
        public string? CampusName { get; set; }
        /// <summary>
        /// 縣市
        /// </summary>
        public string City { get; set; } = null!;
        /// <summary>
        /// 區域
        /// </summary>
        public string District { get; set; } = null!;
        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; } = null!;
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

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<EntryLog> EntryLogs { get; set; }
    }
}
