using System;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace HitMeApp.Shared.Infrastructure.Settings
{
    public static class Extensions
    {
        public static TSettings GetSettings<TSettings>(this IConfiguration configuration, bool optional = false)
            where TSettings : class, new()
        {
            var sectionName = typeof(TSettings).Name.Replace("Settings", string.Empty);
            var section = configuration.GetSection(sectionName);

            if (!section.Exists())
            {
                if (!optional)
                {
                    throw new InvalidOperationException($"A section called {sectionName} is missing in appsettings.json");
                }
                else
                {
                    return null;
                }
            }

            var settings = new TSettings();
            section.Bind(settings);
            return settings;
        }

        public static ContainerBuilder RegisterSettings<TSettings>(this ContainerBuilder builder,
                                                                   IConfiguration configuration,
                                                                   bool optional = false) where TSettings : class, new()
        {
            var settings = configuration.GetSettings<TSettings>(optional);

            if (settings is { })
            {
                builder.RegisterInstance(settings)
                    .As<TSettings>()
                    .SingleInstance()
                    .PreserveExistingDefaults();
            }

            return builder;
        }
    }
}
