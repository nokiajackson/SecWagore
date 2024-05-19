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
    public class EntryLogService : BaseService<EntryLog>
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
            var vm = _context.EntryLogs.Add( new EntryLog
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
                CampusId = model.CampusId,
                CreateDate = DateTime.Now
            });
            //CampusId

            var result = _context.SaveChanges();
            return result > 0;
        }

        public List<EntryLog> GetEntryLogsAsync()
        {
            return _context.EntryLogs.Select(x => new EntryLogVM
            {
                PhoneNumber = x.PhoneNumber,
                FullName = x.FullName,
                NumberOfPeople = x.NumberOfPeople,
                Interviewee = x.Interviewee,
                Purpose = x.Purpose,
                OtherDescription = x.OtherDescription,
                Note = x.Note,
                ReplacementNumber = x.ReplacementNumber,
                EntryTime = x.EntryTime,
                ExitTime = x.ExitTime,
                CampusId = x.CampusId,
                PurposeDesc = x.PurposeEum.GetDescription() // 填充描述
            }).ToList();
        }
    }
}
