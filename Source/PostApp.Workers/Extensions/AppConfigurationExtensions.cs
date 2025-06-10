﻿using Hangfire;
using PostApp.Workers.Interfaces;
using PostApp.Workers.Workers;

namespace PostApp.Workers.Extensions;

public static class AppConfigurationExtensions
{
    public static WebApplication? RegisterRecurringJobs(this WebApplication? app)
    {
        if (app == null)
        {
            return null;
        }
        
        RegisterOrDeregisterScheduledJob<PostReadWorker>(app.Configuration);
        RegisterOrDeregisterScheduledJob<UserReadWorker>(app.Configuration);

        return app;
    }
    
    private static void RegisterOrDeregisterScheduledJob<T>(IConfiguration configuration)
        where T : IWorker
    {
        var className = typeof(T).Name;
        if (configuration.GetValue<bool>($"Workers:{className}:Enabled"))
        {
            RecurringJob.AddOrUpdate<T>(
                className, 
                x => x.ExecuteAsync(configuration.GetValue<int>($"Workers:{className}:BulkSize")), 
                configuration.GetValue<string>($"Workers:{className}:Schedule"));
        }
        else
        {
            RecurringJob.RemoveIfExists(className);
        }
    }
}