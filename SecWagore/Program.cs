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

//var cnstr = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={builder.Environment.ContentRootPath}App_Data\��Ʈw�ɮ�;Integrated Security=True;Trusted_Connection=True;";

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



//�W�[���Ҥ覡�A�ϥ� cookie ����
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    //�s��������cookie �u��g��HTTP(S) ��w�Ӧs��
    options.Cookie.HttpOnly = true;
    //���n�J�ɷ|�۰ʾɨ�n�J��
    options.LoginPath = new PathString("/Home/Login");
    //���v�������ڵ��X�ݷ|�۰ʾɨ즹���|
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
