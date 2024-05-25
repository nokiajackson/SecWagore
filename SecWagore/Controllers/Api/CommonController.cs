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
using SecWagore;


[Route("Api/[controller]")]
[ApiController]
public class CommonController : Controller
{
    private readonly AccountService _accountService;
    EntryLogService _entryLogService;


    public CommonController(
        AccountService accountService,
        EntryLogService etryLogService
        )
    {
        _accountService = accountService;
        _entryLogService = etryLogService;

    }
    [HttpGet("EnumList")]
    [SwaggerResponse(200, type: typeof(Result<IActionResult>))]
    public ActionResult<List<EntryLog>> EnumList()
    {
        var response = new EnumDescriptions
        {
            Purposes = EnumeratorHelper.GetEnumDescriptions<Purpose>(),
            // 如果有其他枚举，调用相应的描述方法
        };

        return Ok(response);
    }
}
