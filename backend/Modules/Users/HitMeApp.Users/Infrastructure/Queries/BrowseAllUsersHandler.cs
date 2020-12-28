using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HitMeApp.Shared.Infrastructure.Cqrs.Queries;
using HitMeApp.Users.Contract.Queries;
using HitMeApp.Users.Dtos;

namespace HitMeApp.Users.Infrastructure.Queries
{
    internal sealed class BrowseAllUsersHandler : IQueryHandler<BrowseAllUsers, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public BrowseAllUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> Handle(BrowseAllUsers query)
        {
            var users = await _userRepository.Browse();
            return users.Select(user => new UserDto { Email = user.Email });
        }
    }
}
