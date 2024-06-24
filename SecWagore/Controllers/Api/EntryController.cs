using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SecWagore.Helpers;
using SecWagore.Models;
using SecWagore.Service;
using Swashbuckle.AspNetCore.Annotations;
using System.Reflection;
using System.Security.Claims;
using SecWagore.Models.ViewModel;


[Route("Api/[controller]")]
[ApiController]
public class EntryController : Controller
{
    private readonly AccountService _accountService;
    EntryLogService _entryLogService;


    public EntryController(
        AccountService accountService,
        EntryLogService etryLogService
        )
    {
        _accountService = accountService;
        _entryLogService = etryLogService;

    }

    
    [HttpPost("Save")]
    [SwaggerResponse(200, type: typeof(Result<IActionResult>))]
    public async Task<Result<EntryLogVM>> SaveEntryLog([FromBody] EntryLogVM model)
    {

        var userName = User.FindFirst(ClaimTypes.Name)?.Value;
        var campusId = User.FindFirst("CampusId");

        if (campusId != null && userName != null)
        {
            int _campusId;
            if (int.TryParse(campusId.Value, out _campusId))
            {
                model.CampusId = _campusId;
            }
            model.UpdateUser = userName;
        }

        //院區為0 要打槍
        var result = await _entryLogService.SaveEntryLogAsync(model);

        return result;
    }

    [HttpPost("Update")]
    [SwaggerResponse(200, type: typeof(Result<IActionResult>))]
    public async Task<Result<EntryLogVM>> UpdateEntryLog([FromBody] EntryLogVM model)
    {
        if (model == null)
        {
            // return BadRequest("Invalid entry log data.");
            return ResultHelper.Failure<EntryLogVM>("找不到指定的資料!", ResultHelper.StatusCode.Save);
        }

        var userName = User.FindFirst(ClaimTypes.Name)?.Value;
        var campusId = User.FindFirst("CampusId");

        if (campusId != null && userName != null)
        {
            int _campusId;
            if (int.TryParse(campusId.Value, out _campusId)&& model.Id==0)
            {
                //新增要掛校區
                model.CampusId = _campusId;
            }
            model.UpdateUser = userName;
        }

        var result = await _entryLogService.UpateEntryLogAsync(model);
        return result;
    }

    [HttpPost("UpdateExitDate")]
    [SwaggerResponse(200, type: typeof(Result<IActionResult>))]
    public async Task<Result<EntryLogVM>> UpdateExitDate([FromBody] EntryLogVM model)
    {
        if (model == null || model.Id == 0)
            return ResultHelper.Failure<EntryLogVM>("找不到指定的資料!", ResultHelper.StatusCode.Save);

        return await _entryLogService.UpateEntryLogAsync(model);
    }


    /// <summary>
    /// Get all campuses.
    /// </summary>
    /// <returns>A list of all campuses.</returns>
    //[HttpGet("GetAllCampuses")]
    //[ProducesResponseType(typeof(List<Campus>), 200)]
    //public IActionResult GetAllCampuses()
    //{
    //    var campuses = _entryLogService.GetAllCampus();
    //    return Ok(campuses);
    //}

    [HttpGet("EntryLogList")]
    [SwaggerResponse(200, type: typeof(Result<IActionResult>))]
    public ActionResult<List<EntryLog>> EntryLogList([FromQuery] SearchEntryLogVM vm)
    {
        var campusIdClaim = User.FindFirst("CampusId");
        if (campusIdClaim != null)
        {
            if (int.TryParse(campusIdClaim.Value, out int campusId))
            {
                vm.CampusId = campusId;
            }
            else
            {
                throw new Exception("Invalid CampusId format.");
            }
        }
        var entryLogs = _entryLogService.GetEntryLogsList(vm);
        return Ok(entryLogs);
    }

    [HttpPost("CreateUser")]
    [SwaggerResponse(200, type: typeof(Result<IActionResult>))]
    public Task<IActionResult> CreateUser(Account account)
    {
        _accountService.CreateUser(account);
        return Task.FromResult<IActionResult>(Ok("Account created successfully."));
    }

}
