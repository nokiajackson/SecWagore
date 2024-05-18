using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SecWagore.Heplers;
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
    [HttpPost]
    public async Task<IActionResult> SaveEntryLog([FromBody] EntryLogVM model)
    {
        if (model == null)
        {
            return BadRequest("Invalid entry log data.");
        }

        var userName = User.FindFirst(ClaimTypes.Name)?.Value;

        var result = await _entryLogService.SaveEntryLogAsync(model);
        if (result)
        {
            return Ok("Entry log saved successfully.");
        }
        else
        {
            return StatusCode(500, "An error occurred while saving the entry log.");
        }
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


    [HttpPost("CreateUser")]
    [SwaggerResponse(200, type: typeof(Result<IActionResult>))]
    public Task<IActionResult> CreateUser(Account account)
    {
        _accountService.CreateUser(account);
        return Task.FromResult<IActionResult>(Ok("Account created successfully."));
    }


}
