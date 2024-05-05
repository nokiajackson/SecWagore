using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SecWagore.Models;
using SecWagore.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => { 

});

//var cnstr = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={builder.Environment.ContentRootPath}App_Data\資料庫檔案;Integrated Security=True;Trusted_Connection=True;";

// Configuration
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("SecWagoreContext");
//builder.Services.AddMvc();
builder.Services.AddHttpContextAccessor();

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(connectionString));

//builder.Services.AddDbContext<AppDbContext>(options =>
//  options.UseSqlServer(builder.Configuration.GetConnectionString("SecWagoreContext"))
//);
builder.Services.AddDbContext<SecDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<CampusService>();
builder.Services.AddScoped<CommonService>();



//增加驗證方式，使用 cookie 驗證
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    //瀏覽器限制cookie 只能經由HTTP(S) 協定來存取
    options.Cookie.HttpOnly = true;
    //未登入時會自動導到登入頁
    options.LoginPath = new PathString("/Home/Login");
    //當權限不夠拒絕訪問會自動導到此路徑
    options.AccessDeniedPath = new PathString("/Home/NoAuthorization");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
