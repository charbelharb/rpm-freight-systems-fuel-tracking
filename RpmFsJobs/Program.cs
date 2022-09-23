using Hangfire;
using Hangfire.SqlServer;
using RpmFsRepositories;
using RpmFsServices.EiaHttpClient;
using RpmFsServices.Jobs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddTransient<IEiaApiClient, EiaApiClient>();
builder.Services.AddTransient<IFuelPricesRepository, FuelPricesRepository>();
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true,
        PrepareSchemaIfNecessary = true,
        SchemaName = "Hangfire"
    }));
builder.Services.AddHangfireServer();
var app = builder.Build();

app.UseHangfireDashboard("/hangfire");
RecurringJob.AddOrUpdate<WeeklyFuelTracker>(
    "weekly-fuel-tracking",
    job => job.ExecuteAsync(),
    builder.Configuration["FuelTrackingCronExpression"]);
app.MapControllers();
app.Run();