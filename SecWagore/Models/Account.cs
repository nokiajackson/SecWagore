using System;
using System.Collections.Generic;

namespace SecWagore.Models
{
    public partial class Account
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
        /// 帳號
        /// </summary>
        public string Username { get; set; } = null!;
        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; } = null!;
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
