// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using BTCWebWallet.Data;
// using System.IO;
using BTCWebWallet.RPCClient;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

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

//LOGGING
// builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole();

//RPC CLIENT
var rpcbind = configuration.GetSection("RPC").GetSection("rpcbind").Value;
var rpcport = configuration.GetSection("RPC").GetSection("rpcport").Value;
var rpcuser = configuration.GetSection("RPC").GetSection("rpcuser").Value;
var rpcpassword = configuration.GetSection("RPC").GetSection("rpcpassword").Value;
var rpcallowip = configuration.GetSection("RPC").GetSection("rpcallowip").Value;
builder.Services.AddSingleton<IRPCClient>(
    x => ActivatorUtilities.CreateInstance<RPCClient>(x, rpcbind, rpcport, rpcuser, rpcpassword));

//BITCOIN NODE
var bitcoinExePath = configuration.GetSection("BitcoinSettings").GetSection("executablePath").Value;
var bitcoinCfgPath = configuration.GetSection("BitcoinSettings").GetSection("configPath").Value;
bitcoinCfgPath = $"{builder.Environment.ContentRootPath}{bitcoinCfgPath}";

var bitcoinWltPath = configuration.GetSection("BitcoinSettings").GetSection("walletPath").Value;
bitcoinWltPath = $"{builder.Environment.ContentRootPath}{bitcoinWltPath}";

var bitcoinDataPath = configuration.GetSection("BitcoinSettings").GetSection("dataPath").Value;
bitcoinDataPath = $"{builder.Environment.ContentRootPath}{bitcoinDataPath}";

Directory.CreateDirectory(bitcoinWltPath);
Directory.CreateDirectory(bitcoinDataPath);

builder.Services.AddSingleton<IBitcoinNode>(
    x => ActivatorUtilities.CreateInstance<BitcoinNode>(x, 
        bitcoinExePath, 
        bitcoinCfgPath, 
        bitcoinWltPath, 
        bitcoinDataPath,
        rpcbind,
        rpcport,
        rpcuser,
        rpcpassword,
        rpcallowip));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName.ToLower().Contains("Development".ToLower()))
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