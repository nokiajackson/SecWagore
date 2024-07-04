using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecWagore.Models
{
    [Table("Account")]
    public class Account
    {
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 關聯校區
        /// </summary>
        public int CampusId { get; set; }
        /// <summary>
        /// 帳號
        /// </summary>
        [MaxLength(255)] 
        public string Username { get; set; } = null!;
        /// <summary>
        /// 密碼
        /// </summary>
        [MaxLength(255)]
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
        /// <summary>
        /// 身分:一般:1,警衛:2,admin:9
        /// </summary>
        public int Ide { get; set; }

        public virtual Campus? Campus { get; set; }
    }
}
