using Microsoft.AspNetCore.Mvc;
using SecWagore.Heplers;
using SecWagore.Models;
using SecWagore.Service;
using Swashbuckle.AspNetCore.Annotations;
using System.Reflection;


[Route("Api/[controller]")]
[ApiController]
public class AccountController : Controller
{
    private readonly AccountService _accountService;
    


    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
        
    }

    [HttpGet("Test")]
    public IActionResult Test()
    {
        return Ok("AAA");
    }

    [HttpPost("CreateUser")]
    [SwaggerResponse(200, type: typeof(Result<IActionResult>))]
    public Task<IActionResult> CreateUser(Account account)
    {
        _accountService.CreateUser(account);
        return Task.FromResult<IActionResult>(Ok("Account created successfully."));
    }

    [HttpPost("validate")]
    [SwaggerResponse(200, type: typeof(Result<IActionResult>))]
    public Task<IActionResult> ValidateCredentials(LoginModel model)
    {
        // 首先檢查圖片驗證碼是否正確
        //if (!ValidateImageCaptcha(model.Captcha))
        //{
        //    return Task.FromResult<IActionResult>(BadRequest("Invalid image captcha."));
        //}

        // 驗證帳戶
        bool isValid = _accountService.ValidateCredentials(model.Username, model.Password);
        if (isValid)
        {
            return Task.FromResult<IActionResult>(Ok("Credentials validated successfully."));
        }
        else
        {
            return Task.FromResult<IActionResult>(Unauthorized("Invalid credentials."));
        }
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
    public Task<IActionResult> Login(LoginModel model)
    {
        // 驗證用戶名和密碼
        bool isValid = _accountService.ValidateCredentials(model.Username, model.Password);
        if (isValid)
        {
            return Task.FromResult<IActionResult>(Ok("Login successful."));
        }
        else
        {
            return Task.FromResult<IActionResult>(Unauthorized("Invalid credentials."));
        }
    }
    /// <summary>
    /// 登出
    /// </summary>
    /// <returns></returns>
    //[HttpGet, HttpPost]
    //[SwaggerResponse(200, type: typeof(Result<IActionResult>))]
    //public async Task<IActionResult> Logout()
    //{
    //    //await _accountService.SignOutAsync();
    //    return RedirectToAction("Index", "Home");
    //}

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
