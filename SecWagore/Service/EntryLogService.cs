using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SecWagore.Models;
using System.Data.Entity;
using SecWagore.Models.ViewModel;
using System.Security.Claims;

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

        public async Task<bool> SaveEntryLogAsync(EntryLogVM model)
        {
            var vm = DbModel.EntryLogs.Add( new EntryLog
            {
                PhoneNumber = model.PhoneNumber,
                FullName = model.FullName,
                NumberOfPeople = model.NumberOfPeople,
                Interviewee = model.Interviewee,
                Purpose = model.Purpose,
                OtherDescription = model.OtherDescription,
                Note = model.Note,
                ReplacementNumber = model.ReplacementNumber,
                EntryTime = model.EntryTime,
                CreateDate = DateTime.Now
            });
            var result = await DbModel.SaveChangesAsync();
            return result > 0;
        }
    }
}
