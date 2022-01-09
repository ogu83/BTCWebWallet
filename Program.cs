// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using BTCWebWallet.Data;
using System.IO;
using BTCWebWallet.RPCClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Add services to the container.
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlServer(connectionString));

// var connectionString = builder.Configuration.GetConnectionString("PostgresDefaultConnection");
// builder.Services.AddDbContext<ApplicationDbContext>(
//    options => options.UseNpgsql(connectionString, nOptions => nOptions.SetPostgresVersion(9,5)));    

// builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//     .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var configuration = builder.Configuration;


var bitcoinExePath = configuration.GetSection("BitcoinSettings").GetSection("executablePath").Value;
var bitcoinCfgPath = configuration.GetSection("BitcoinSettings").GetSection("configPath").Value;
bitcoinCfgPath = $"{builder.Environment.ContentRootPath}{bitcoinCfgPath}";

var bitcoinWltPath = configuration.GetSection("BitcoinSettings").GetSection("walletPath").Value;
bitcoinWltPath = $"{builder.Environment.ContentRootPath}{bitcoinWltPath}";

var bitcoinDataPath = configuration.GetSection("BitcoinSettings").GetSection("dataPath").Value;
bitcoinDataPath = $"{builder.Environment.ContentRootPath}{bitcoinDataPath}";

Directory.CreateDirectory(bitcoinWltPath);
Directory.CreateDirectory(bitcoinDataPath);

builder.Services.AddSingleton<IBitcoinNode>(x => ActivatorUtilities.CreateInstance<BitcoinNode>(x, bitcoinExePath, bitcoinCfgPath, bitcoinWltPath, bitcoinDataPath));


var rpcbind = configuration.GetSection("RPC").GetSection("rpcbind").Value;
var rpcport = configuration.GetSection("RPC").GetSection("rpcport").Value;
var rpcuser = configuration.GetSection("RPC").GetSection("rpcuser").Value;
var rpcpassword = configuration.GetSection("RPC").GetSection("rpcpassword").Value;
builder.Services.AddSingleton<IRPCClient>(x => ActivatorUtilities.CreateInstance<RPCClient>(x, rpcbind, rpcport, rpcuser, rpcpassword));

var app = builder.Build();

var appLifetime = app.Lifetime;
appLifetime.ApplicationStopping.Register(() =>
{
    app.Logger.LogInformation("App Ended");
    Console.WriteLine("Application Ended");
    var bitcoinNode = app.Services.GetService<IBitcoinNode>();
    if (bitcoinNode != null) 
        bitcoinNode.Terminate();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();