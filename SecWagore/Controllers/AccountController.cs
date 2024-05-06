using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SecWagore.Heplers;
using SecWagore.Models;
using SecWagore.Service;
using Swashbuckle.AspNetCore.Annotations;
using System.Reflection;
using System.Security.Claims;


[Route("Api/[controller]")]
[ApiController]
public class AccountController : Controller
{
    private readonly AccountService _accountService;
    CampusService _campusService;


    public AccountController(
        AccountService accountService,
        CampusService campusService
        )
    {
        _accountService = accountService;
        _campusService = campusService;

    }

    /// <summary>
    /// Get all campuses.
    /// </summary>
    /// <returns>A list of all campuses.</returns>
    [HttpGet("GetAllCampuses")]
    [ProducesResponseType(typeof(List<Campus>), 200)]
    public IActionResult GetAllCampuses()
    {
        var campuses = _campusService.GetAllCampus();
        return Ok(campuses);
    }


    [HttpPost("CreateUser")]
    [SwaggerResponse(200, type: typeof(Result<IActionResult>))]
    public Task<IActionResult> CreateUser(Account account)
    {
        _accountService.CreateUser(account);
        return Task.FromResult<IActionResult>(Ok("Account created successfully."));
    }


    private bool ValidateImageCaptcha(string captcha)
    {
        // 在這裡執行圖片驗證碼驗證的邏輯
        // 這裡僅作為示例，實際應用中你需要根據你的驗證碼機制進行處理
        // 如果圖片驗證碼正確，返回 true，否則返回 false
        // 注意：圖片驗證碼的驗證可能涉及對用戶輸入的圖片進行 OCR 或其他處理
        // 或者是比較用戶輸入的驗證碼與服務端生成的驗證碼是否一致
        return captcha == "correct_image_captcha";
    }

    [HttpGet("{userName}")]
    [SwaggerResponse(200, type: typeof(Result<IActionResult>))]
    public Task<IActionResult> GetAccountById(string userName)
    {
        var account = _accountService.GetAccountById(userName);
        if (account == null)
        {
            return Task.FromResult<IActionResult>(NotFound("Account not found."));
        }
        return Task.FromResult<IActionResult>(Ok(account));
    }

    [HttpPost("login")]
    [SwaggerResponse(200, type: typeof(Result<IActionResult>))]
    //[SwaggerResponse(200, type: typeof(Result<IActionResult>))]
    public async Task<IActionResult> Login(LoginModelVM model)
    {
        // 驗證用戶名和密碼
        bool isValid = _accountService.ValidateCredentials(model);
        if (isValid)
        {
            // 登錄成功，設置用戶的身份驗證標識
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username),
                // 這裏可以設置其他需要記錄的用戶信息
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var authProperties = new AuthenticationProperties { };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                principal,
                authProperties);

            return Ok("Login successful.");
        }
        else
        {
            return Unauthorized("Invalid credentials.");
        }
    }
    /// <summary>
    /// 登出
    /// </summary>
    /// <returns></returns>
    [HttpGet, HttpPost]
    [SwaggerResponse(200, type: typeof(Result<IActionResult>))]
    public async Task<IActionResult> Logout()
    {
        //await _accountService.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    /// <summary>
    /// 驗證圖
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("Captcha")] // 如果您使用了自定義路由，請確保包括該路由
    [SwaggerResponse(200, type: typeof(FileContentResult))]
    public IActionResult Captcha()
    {
        var code = CaptchaHelper.GetCode(6);
        TempData["captcha"] = code; // 將驗證碼存儲在 TempData 中


        var byteArray = CaptchaHelper.GetByteArray(code);
        return File(byteArray, "image/jpeg");
    }

    void WriteLog(string message)
    {
        var path = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
        if (!Directory.Exists(path + "\\Logs\\"))
        {
            Directory.CreateDirectory(path + "\\Logs\\");
        }
        var logFile = path + "\\Logs\\" + string.Format("Portal_{0:D3}{1:D2}{2:D2}.log", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        using (StreamWriter sw = System.IO.File.AppendText(logFile))
        {
            sw.WriteLine(string.Format("{0:T}:{1} ", DateTime.Now, message));
        }
    }
}
