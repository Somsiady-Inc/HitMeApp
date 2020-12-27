using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace HitMeApp.Shared.Infrastructure.Exceptions
{
    internal sealed class DefaultExceptionMapperRegistry : IExceptionMapperRegistry
    {
        private readonly Dictionary<string, IExceptionToResponseMapper> _exceptionMapperRegistry =
            new Dictionary<string, IExceptionToResponseMapper>();

        private IExceptionToResponseMapper _fallbackMapper;

        public void Register<TExceptionMapper>(string namespaceRegex = null)
            where TExceptionMapper : class, IExceptionToResponseMapper, new()
        {
            namespaceRegex ??= typeof(TExceptionMapper).Namespace;

            if (namespaceRegex is null)
            {
                throw new ArgumentNullException(nameof(namespaceRegex), "Cannot register an exception mapper due to invalid namespace");
            }

            if (_exceptionMapperRegistry.ContainsKey(namespaceRegex))
            {
                throw new InvalidOperationException($"One exception mapper has been already registered to the namespace {namespaceRegex}");
            }

            _exceptionMapperRegistry.Add(namespaceRegex, new TExceptionMapper());
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
            var exceptionNamespace = ex.GetType().Namespace;
            var matchingEntry = _exceptionMapperRegistry.SingleOrDefault(mapperEntry => Regex.IsMatch(exceptionNamespace, mapperEntry.Key));
            return matchingEntry.Equals(default(KeyValuePair<string, IExceptionToResponseMapper>)) ? _fallbackMapper : matchingEntry.Value;
        }
    }
}
