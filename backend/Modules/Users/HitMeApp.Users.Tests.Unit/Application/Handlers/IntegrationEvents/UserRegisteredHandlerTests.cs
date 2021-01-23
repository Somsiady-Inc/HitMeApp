using System;
using System.Threading.Tasks;
using HitMeApp.Identity.IntegrationEvents;
using HitMeApp.Users.Application.Exceptions;
using HitMeApp.Users.Application.Handlers.IntegrationEvents;
using HitMeApp.Users.Core;
using NSubstitute;
using Shouldly;
using Xunit;

namespace HitMeApp.Users.Tests.Unit.Application.Handlers.IntegrationEvents
{
    public class UserRegisteredHandlerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly UserRegisteredHandler _userRegisteredHandler;

        public UserRegisteredHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _userRegisteredHandler = new UserRegisteredHandler(_userRepository);
        }

        public Task Act(UserRegistered @event)
            => _userRegisteredHandler.Handle(@event);

        [Fact]
        public async Task given_already_existent_user_exception_should_be_thrown()
        {
            var userId = Guid.NewGuid();
            _userRepository.Exists(userId).Returns(true);
            var @event = new UserRegistered(userId);

            var exception = await Record.ExceptionAsync(() => Act(@event));

            exception.ShouldNotBeNull();
            var userAlreadyExistsException = exception.ShouldBeOfType<UserAlreadyExistsException>();
            userAlreadyExistsException.Id.ShouldBe(userId);
        }

        [Fact]
        public async Task given_non_existent_user_new_user_should_be_persisted()
        {
            var userId = Guid.NewGuid();
            _userRepository.Exists(userId).Returns(false);
            var @event = new UserRegistered(userId);

            await Act(@event);

            await _userRepository.Received(1).Add(Arg.Any<User>());
        }
    }
}
