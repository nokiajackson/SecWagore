using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SecWagore.Models;
using System.Data.Entity;

namespace SecWagore.Service
{
    /// <summary>
    /// 共通性服務
    /// </summary>
    public class EntryLogService : BaseService<ApplicationUser>
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="dbModel"></param>
        /// <param name="configuration"></param>
        /// <param name="userManageService"></param>
        public EntryLogService(SecDbContext dbModel,
            IConfiguration configuration) : base(dbModel)
        {
            _configuration = configuration;
        }

        public async Task<bool> SaveEntryLogAsync(EntryLog entryLog)
        {
            DbModel.EntryLogs.Add(entryLog);
            var result = await DbModel.SaveChangesAsync();
            return result > 0;
        }
    }
}
