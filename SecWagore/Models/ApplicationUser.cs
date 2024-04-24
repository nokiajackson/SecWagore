namespace SecWagore.Models
{
    public class ApplicationUser
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
    }
}