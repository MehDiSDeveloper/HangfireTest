using Hangfire;
using Microsoft.EntityFrameworkCore;
using TestHangfire2.Contexts.HangfireDbContext;
using TestHangfire2.Interfaces;
using TestHangfire2.Services;
using Serilog;
using Hangfire.SqlServer;

//TestHangfire2

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//configing serilog
Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File($"c:\\Serilogs\\TestHangfire2-{DateTime.Today}.txt")
                .WriteTo.Console()
                .CreateLogger();

try
{
    // Your application logic here
    Log.Information("This is an Test information log");

    // Other logging statements...
}
catch (Exception ex)
{
    Log.Error(ex, "An error occurred while logging Test");
}
finally
{
    // Ensure to flush the logs on application exit
    Log.CloseAndFlush();
};

//number of retries of background jobs by hangfire
GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 5 });

//hangfire
//var options = new SqlServerStorageOptions
//{
//    //set the interval polling from database by hangfire
//    QueuePollInterval = TimeSpan.FromSeconds(5),
//    PrepareSchemaIfNecessary = false,
//};
//builder.Services.AddHangfire(conf => 
//conf.UseSqlServerStorage("Data Source=MehdiPc; Initial Catalog=TestHangfireJobDb; Integrated Security=True;TrustServerCertificate=True;", options));

builder.Services.AddHangfire(conf => 
conf.UseSqlServerStorage("Data Source=MehdiPc; Initial Catalog=TestHangfireJobDb; Integrated Security=True;TrustServerCertificate=True;"));

//configuring datbase
builder.Services.AddDbContext<ApplicationDbContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationConnectionString"));
});
builder.Services.AddScoped<ISurveyService, SurveyService>();

var app = builder.Build();

//Hangfire Config
var hangfireDashboardOptions = new DashboardOptions { AppPath = "/" };
app.UseHangfireDashboard("/jobs", hangfireDashboardOptions);
var hangfireServerOptions = new BackgroundJobServerOptions
{
    Queues = new[] { "q2","q3" },
    ServerName = "S2",
};
app.UseHangfireServer(hangfireServerOptions);

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
