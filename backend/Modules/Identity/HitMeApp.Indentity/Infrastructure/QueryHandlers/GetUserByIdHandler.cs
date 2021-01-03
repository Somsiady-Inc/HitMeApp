using System.Threading.Tasks;
using HitMeApp.Indentity.Contract.Dtos;
using HitMeApp.Indentity.Contract.Queries;
using HitMeApp.Indentity.Core;
using HitMeApp.Shared.Infrastructure.Cqrs.Queries;

namespace HitMeApp.Indentity.Infrastructure.QueryHandlers
{
    internal sealed class GetUserByIdHandler : IQueryHandler<GetUserById, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserById query)
        {
            var user = await _userRepository.Get(new UserId(query.Id));
            return user?.AsDto();
        }
    }
}
