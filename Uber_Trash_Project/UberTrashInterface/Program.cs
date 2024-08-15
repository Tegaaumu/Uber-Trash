using Microsoft.Extensions.Logging.Console;
using System.Net.Http.Headers;
using System.Text;
using UberTrashInterface.Entities;
using UberTrashInterface.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Logging.AddConsole(options =>
{
    options.FormatterName = ConsoleFormatterNames.Systemd; // or other suitable formatter
    //options.TimestampFormat =  "[HH:mm:ss] ";
});
builder.Services.AddHttpClient<IUberTrashServices, UberTrashServices>(client =>
{
    client.BaseAddress = new Uri("http://tegaproject-001-site1.atempurl.com/");

    // Set authorization header for all clients
    string username = "11190513";
    string password = "60-dayfreetrial";
    string combined = username + ":" + password;
    string base64Encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes(combined));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Encoded);
});
builder.Services.AddHttpClient<IStellarConnection, StellarConnection>(client =>
{
    client.BaseAddress = new Uri("http://tegaproject-001-site1.atempurl.com/");

    // Set authorization header for all clients
    string username = "11190513";
    string password = "60-dayfreetrial";
    string combined = username + ":" + password;
    string base64Encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes(combined));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Encoded);
}); 
builder.Services.AddScoped<UserSellers>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSession();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
