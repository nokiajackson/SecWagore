using Microsoft.AspNetCore.Mvc;
using SecWagore.Models;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("create")]
    public IActionResult CreateAccount(Account account)
    {
        _accountService.AddAccount(account);
        return Ok("Account created successfully.");
    }

    [HttpPost("validate")]
    public IActionResult ValidateCredentials(LoginModel model)
    {
        // 首先檢查圖片驗證碼是否正確
        if (!ValidateImageCaptcha(model.Captcha))
        {
            return BadRequest("Invalid image captcha.");
        }

        // 驗證帳戶
        bool isValid = _accountService.ValidateCredentials(model.Username, model.Password);
        if (isValid)
        {
            return Ok("Credentials validated successfully.");
        }
        else
        {
            return Unauthorized("Invalid credentials.");
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

    [HttpGet("{id}")]
    public IActionResult GetAccountById(int id)
    {
        var account = _accountService.GetAccountById(id);
        if (account == null)
        {
            return NotFound("Account not found.");
        }
        return Ok(account);
    }
}
