using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//sessions
builder.Services.AddSession(
    options =>
    {
        options.IdleTimeout = TimeSpan.FromSeconds(120);
        options.Cookie.IsEssential = true;
    }
    );
builder.Services.AddHttpContextAccessor();

//cookies
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor > ();


//outputcaching

builder.Services.AddOutputCaching();
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
app.UseSession();
app.UseRouting();
app.UseOutputCaching();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Accounts}/{action=Login}/{id?}");

app.Run();
