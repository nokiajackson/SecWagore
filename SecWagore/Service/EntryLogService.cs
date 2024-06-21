using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SecWagore.Models;
using System.Data.Entity;
using SecWagore.Models.ViewModel;
using System.Security.Claims;
using SecWagore.Helpers;
using System.Data.Entity.Infrastructure;
using static SecWagore.Helpers.ResultHelper;

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

        public async Task<Result<EntryLogVM>> SaveEntryLogAsync(EntryLogVM model)
        {
            try
            {
                var vm = _context.EntryLogs.Add(new EntryLog
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
                    ExitTime = model.ExitTime ?? null,
                    CampusId = model.CampusId,
                    CreateDate = model.CreateDate ?? DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                //CampusId

                var result = _context.SaveChanges();
                return ResultHelper.Success<EntryLogVM>(model, ResultHelper.StatusCode.Get);
            }
            catch (Exception ex)
            {
                return ResultHelper.Failure<EntryLogVM>("異動資料失敗！！" + ex.Message, ResultHelper.StatusCode.Get);
            }
            
        }
        public async Task<Result<EntryLogVM>> UpateEntryLogAsync(EntryLogVM model)
        {
            if (model.Id == 0)
            {
                return await SaveEntryLogAsync(model);
            }else if (model.Id > 0)
            {
                if (!_context.EntryLogs.AsQueryable()
                   .Any(r => r.Id == model.Id))
                {
                    return ResultHelper.Failure<EntryLogVM>("找不到指定的資料!", ResultHelper.StatusCode.Get);
                }
            }
           

            using (var trans = _context.Database.BeginTransaction())
            {
                try
                {
                    var entryLogs = await _context.EntryLogs
                                    .Where(r => r.Id == model.Id).FirstOrDefaultAsync();

                    entryLogs.UpdateDate = DateTime.Now;
                    entryLogs.ExitTime = model.ExitTime;

                    var result = _context.SaveChanges();
                    return ResultHelper.Success<EntryLogVM>(model, ResultHelper.StatusCode.Get);
                }
                catch (Exception ex)
                {
                    return ResultHelper.Failure<EntryLogVM>("異動資料失敗！！" + ex.Message, ResultHelper.StatusCode.Get);
                }
            }
        }

        public List<EntryLogVM> GetEntryLogsList(SearchEntryLogVM vm)
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

            if (vm.Purpose.HasValue && vm.Purpose.Value != 0)
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

            var sql = query.ToQueryString();


            var result = query
                .Select(x => new EntryLogVM
                {
                    Id = x.Id,
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
