using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PostApp.BL.Interfaces;
using PostApp.BL.Services;
using PostApp.DL.EntityFramework;
using PostApp.DL.Interfaces;
using PostApp.DL.Repositories;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
        logging.SetMinimumLevel(LogLevel.Warning);
    })
    .ConfigureServices((_, services) =>
    {
        services.AddDbContext<PostAppContext>(options =>
            options.UseNpgsql("Host=127.0.0.1;Port=2345;Database=test;Username=test;Password=test"));
        services.AddScoped(typeof(IDatabaseContextRepository<>), typeof(DatabaseContextRepository<>));
        services.AddScoped<IDatabaseContextRepository, DatabaseContextRepository>();
        
        services.AddScoped<IUserInfoService, UserInfoService>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var services = scope.ServiceProvider;

var userService = services.GetRequiredService<IUserInfoService>();

Console.Write("Enter partial city name: ");
var userPartialCityNameInput = (Console.ReadLine() ?? "").Trim();;

try
{
    var users = await userService.GetAsync(userPartialCityNameInput, CancellationToken.None);

    if (users.Length == 0)
    {
        Console.WriteLine("No users found.");
        return;
    }

    foreach (var user in users)
    {
        Console.WriteLine($"Name: {user.Name}");
        Console.WriteLine($"City: {user.City}");
        Console.WriteLine($"Posts count: {user.Posts?.Count ?? 0}");
        Console.WriteLine(new string('-', 22));
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
    
    