using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// we store the authentication cookie in the browser,
// so we need to configure the cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/Denied"; //403 page
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//use HttpRedirection to redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

//figuring out the which controller to load based on the URL requested by the user
app.UseRouting();

//check if the user is authenticated or not, if not redirect to login page
app.UseAuthentication();

//check if the user is authorized to access the requested resource, if not redirect to access denied page
app.UseAuthorization();

//serve static files like CSS, JS, images, etc.
app.MapStaticAssets();

//map the controller routes, the default route is Home/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
