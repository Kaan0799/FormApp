using Bussiness.Abstract;
using Bussiness.Concrate;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IUserService, UserManager>();
builder.Services.AddSingleton<IUserDal, EfUserFramework>();
builder.Services.AddSingleton<IFormService, FormManager>();
builder.Services.AddSingleton<IFormDal, EfFormRepository>();
builder.Services.AddSingleton<IQuestionService, QuestionManager>();
builder.Services.AddSingleton<IQuestionDal, EfQuestionRepository>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Login/Login";
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
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
