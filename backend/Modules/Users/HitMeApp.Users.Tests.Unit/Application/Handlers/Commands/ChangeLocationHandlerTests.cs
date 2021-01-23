using System;
using System.Threading.Tasks;
using HitMeApp.Users.Application.Exceptions;
using HitMeApp.Users.Application.Handlers.Commands;
using HitMeApp.Users.Contract.Commands;
using HitMeApp.Users.Core;
using HitMeApp.Users.Core.Exceptions;
using HitMeApp.Users.Core.Specification;
using NSubstitute;
using Shouldly;
using Xunit;

namespace HitMeApp.Users.Tests.Unit.Application.Handlers.Commands
{
    public class ChangeLocationHandlerTests
    {
        private readonly ChangeLocationHandler _changeLocationHandler;
        private readonly IUserRepository _userRepository;

        public ChangeLocationHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _changeLocationHandler = new ChangeLocationHandler(_userRepository);
        }

        public Task Act(ChangeLocation command)
            => _changeLocationHandler.Handle(command);

        [Fact]
        public async Task given_non_existent_user_not_found_exception_should_be_thrown()
        {
            var userId = Guid.NewGuid();
            var command = new ChangeLocation { UserId = userId };
            _userRepository.Get(userId).Returns(Task.FromResult<User>(null));

            var exception = await Record.ExceptionAsync(() => Act(command));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<UserNotFoundException>();
        }

        [Fact]
        public async Task given_valid_location_and_incomplete_user_change_location_should_fail()
        {
            var userId = Guid.NewGuid();
            var command = new ChangeLocation
            {
                UserId = userId,
                Latitude = 41.902222222222,
                Longitude = 12.456388888889
            };
            var user = User.Incomplete(userId);
            _userRepository.Get(userId).Returns(user);

            var exception = await Record.ExceptionAsync(() => Act(command));

            exception.ShouldNotBeNull();
            var cannotUpdateIncompleteUserException = exception.ShouldBeOfType<CannotUpdateIncompleteUserException>();
            cannotUpdateIncompleteUserException.ParameterName.ShouldBe(nameof(user.Location));
        }

        [Fact]
        public async Task given_existent_user_and_valid_data_user_should_be_persisted()
        {
            var userId = Guid.NewGuid();
            var command = new ChangeLocation
            {
                UserId = userId,
                Latitude = 41.902222222222,
                Longitude = 12.456388888889
            };
            var minimalAgeSpecification = Substitute.For<IMinimalAgeSpecification>();
            minimalAgeSpecification.IsSatisfiedBy(Arg.Any<PersonalInfo>()).Returns(true);
            var personalInfo = PersonalInfo.Load(
                "Test",
                "Test description",
                new DateTime(1920, 5, 20),
                Sex.Male,
                minimalAgeSpecification
            );
            var user = User.Load(userId, null, null, personalInfo);
            _userRepository.Get(userId).Returns(user);

            await Act(command);

            user.Location.Latitude.ShouldBe(command.Latitude);
            user.Location.Longitude.ShouldBe(command.Longitude);
            await _userRepository.Received(1).Update(user);
        }
    }
}
