using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text.RegularExpressions;

namespace HitMeApp.Shared.Infrastructure.Exceptions
{
    internal sealed class DefaultExceptionMapperRegistry : IExceptionMapperRegistry
    {
        private readonly ConcurrentDictionary<string, IExceptionToResponseMapper> _exceptionMapperRegistry =
            new ConcurrentDictionary<string, IExceptionToResponseMapper>();

        private IExceptionToResponseMapper _fallbackMapper;

        public void Register<TExceptionMapper>(string namespaceRegex = null)
            where TExceptionMapper : class, IExceptionToResponseMapper, new()
        {
            namespaceRegex ??= typeof(TExceptionMapper).Namespace;

            if (namespaceRegex is null)
            {
                throw new ArgumentNullException(nameof(namespaceRegex), "Cannot register an exception mapper due to invalid namespace");
            }

            _exceptionMapperRegistry.TryAdd(namespaceRegex, new TExceptionMapper());
        }

        public void RegisterFallbackMapper<TFallbackMapper>() where TFallbackMapper : class, IExceptionToResponseMapper, new()
        {
            if (_fallbackMapper is { })
            {
                throw new InvalidOperationException("Fallback exception mapper has already been registered");
            }
            _fallbackMapper = new TFallbackMapper();
        }

        public IExceptionToResponseMapper Resolve(Exception ex)
        {
            var matchingEntry = _exceptionMapperRegistry.SingleOrDefault(mapperEntry => Regex.IsMatch(ex.Source, mapperEntry.Key, RegexOptions.Compiled));
            return matchingEntry.IsDefault() ? _fallbackMapper : matchingEntry.Value;
        }
    }
}
