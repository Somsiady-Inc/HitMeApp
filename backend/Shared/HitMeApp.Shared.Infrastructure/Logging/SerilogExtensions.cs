﻿using Serilog;

namespace HitMeApp.Shared.Infrastructure.Logging
{
    public static class SerilogExtensions
    {
        private static readonly string ModuleTag = "Module";

        public static ILogger ForModule(this ILogger logger, string moduleName)
            => logger.ForContext(ModuleTag, moduleName);
    }
}
