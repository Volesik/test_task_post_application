using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using PostApp.BL.Interfaces;
using PostApp.BL.Services;
using PostApp.Common.Constants;
using PostApp.DL.EntityFramework;
using PostApp.DL.Interfaces;
using PostApp.DL.Repositories;
using PostApp.Web.Common.Extensions;
using PostApp.Workers.Extensions;
using PostApp.Workers.Filters;
using PostApp.Workers.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Hangfire configuration
var hangfireQueues = new List<string>();
builder.Configuration.GetSection("Hangfire:Queues").Bind(hangfireQueues);

if (hangfireQueues.Count == 0)
{
    hangfireQueues = WorkerQueueConstants.QueuesForRegistration;
}

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(options => options
        .UseNpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddHangfireServer(options =>
{
    options.Queues = hangfireQueues.ToArray();
});

builder.Services.AddDbContext<PostAppContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddWebCommonServices(builder.Configuration);
builder.Services.AddScoped<PostMapper>();
builder.Services.AddScoped<UserMapper>();
builder.Services.AddScoped(typeof(IDatabaseContextRepository<>), typeof(DatabaseContextRepository<>));
builder.Services.AddScoped<IDatabaseContextRepository, DatabaseContextRepository>();
builder.Services.AddTransient<IStartupFilter, MigrationStartupFilter<PostAppContext>>();

builder.Services.AddScoped<IUserInfoService, UserInfoService>();
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHangfireDashboard();

app.UseAuthorization();

app.MapControllers();
app.MapHangfireDashboard();
app.RegisterRecurringJobs();
app.Run();