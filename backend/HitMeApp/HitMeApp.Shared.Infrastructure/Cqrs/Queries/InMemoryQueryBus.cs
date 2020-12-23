using Autofac;
using System;
using System.Threading.Tasks;

namespace HitMeApp.Shared.Infrastructure.Cqrs.Queries
{
    internal sealed class InMemoryQueryBus : IQueryBus
    {
        private readonly IComponentContext _context;

        public InMemoryQueryBus(IComponentContext context)
        {
            _context = context;
        }

        public async Task<TResult> Query<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query), "Command cannot be null");
            }

            var handler = _context.Resolve<IQueryHandler<TQuery, TResult>>();
            return await handler.Handle(query);
        }
    }
}
