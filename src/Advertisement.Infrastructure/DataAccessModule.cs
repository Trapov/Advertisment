using System;
using Advertisement.Application;
using Advertisement.Domain;
using Advertisement.Infrastructure.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Advertisement.Infrastructure
{
    public static class DataAccessModule
    {
        public sealed class ModuleConfiguration
        {
            public IServiceCollection Services { get; init; }
        }
        
        public static IServiceCollection AddDataAccessModule(
            this IServiceCollection services,
            Action<ModuleConfiguration> action
        )
        {
            var moduleConfiguration = new ModuleConfiguration
            {
                Services = services
            };
            action(moduleConfiguration);
            return services;
        }

        public static void InMemory(this ModuleConfiguration moduleConfiguration)
        {
            moduleConfiguration.Services.AddSingleton(new InMemoryRepository());
            moduleConfiguration.Services.AddSingleton<IRepository<User, int>>(sp => sp.GetService<InMemoryRepository>());
            moduleConfiguration.Services.AddSingleton<IRepository<Ad, int>>(sp => sp.GetService<InMemoryRepository>());
        }
    }
}