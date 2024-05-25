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
                Purpose = (byte)model.Purpose,
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

        public List<EntryLogVM> GetEntryLogsAsync(SearchEntryLogVM vm)
        {
            var query = _context.EntryLogs.AsQueryable();

            if (vm.CampusId.HasValue)
            {
                query = query.Where(el => el.CampusId == vm.CampusId.Value);
            }

            if (!string.IsNullOrEmpty(vm.FullName))
            {
                query = query.Where(el => el.FullName.Contains(vm.FullName));
            }

            if (vm.Purpose.HasValue)
            {
                query = query.Where(el => el.Purpose == (byte)vm.Purpose.Value);

            }

            if (vm.EntryTimeStart.HasValue || vm.EntryTimeEnd.HasValue)
            {
                if (vm.EntryTimeStart.HasValue)
                {
                    query = query.Where(el => el.EntryTime >= vm.EntryTimeStart.Value);
                }

                if (vm.EntryTimeEnd.HasValue)
                {
                    query = query.Where(el => el.EntryTime <= vm.EntryTimeEnd.Value);
                }
            }


            if (vm.ExitTimeStart.HasValue || vm.ExitTimeEnd.HasValue)
            {
                query = query.Where(el =>
                    (!vm.ExitTimeStart.HasValue || el.ExitTime >= vm.ExitTimeStart.Value) &&
                    (!vm.ExitTimeEnd.HasValue || el.ExitTime <= vm.ExitTimeEnd.Value));
            }



            var result = query
                .Select(x => new EntryLogVM
                {
                    PhoneNumber = x.PhoneNumber,
                    FullName = x.FullName,
                    NumberOfPeople = x.NumberOfPeople ?? 0,
                    Interviewee = x.Interviewee,
                    Purpose = (Purpose)x.Purpose,
                    OtherDescription = x.OtherDescription,
                    Note = x.Note,
                    ReplacementNumber = x.ReplacementNumber,
                    EntryTime = x.EntryTime,
                    ExitTime = x.ExitTime
                })
                .ToList();

            return result;

        }
    }
}
