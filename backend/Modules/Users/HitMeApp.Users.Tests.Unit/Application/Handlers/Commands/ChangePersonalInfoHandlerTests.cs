using System;
using System.Threading.Tasks;
using HitMeApp.Users.Application.Exceptions;
using HitMeApp.Users.Application.Handlers.Commands;
using HitMeApp.Users.Contract.Commands;
using HitMeApp.Users.Contract.Dtos;
using HitMeApp.Users.Core;
using NSubstitute;
using Shouldly;
using Xunit;

namespace HitMeApp.Users.Tests.Unit.Application.Handlers.Commands
{
    public class ChangePersonalInfoHandlerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly ChangePersonalInfoHandler _changePersonalInfoHandler;

        public ChangePersonalInfoHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _changePersonalInfoHandler = new ChangePersonalInfoHandler(_userRepository);
        }

        public Task<UserDto> Act(ChangePersonalInfo command)
            => _changePersonalInfoHandler.Handle(command);

        [Fact]
        public async Task given_non_existent_user_not_found_exception_should_be_thrown()
        {
            var userId = Guid.NewGuid();
            var command = new ChangePersonalInfo { UserId = userId };
            _userRepository.Get(userId).Returns(Task.FromResult<User>(null));

            var exception = await Record.ExceptionAsync(() => Act(command));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<UserNotFoundException>();
        }

        [Fact]
        public async Task given_existent_user_and_valid_personal_info_user_should_be_persisted()
        {
            var userId = Guid.NewGuid();
            var command = new ChangePersonalInfo
            {
                UserId = userId,
                Nickname = "Test",
                Description = "Test Description",
                Sex = Sex.Female,
                BirthDate = new DateTime(1920, 5, 20)
            };
            var user = User.Incomplete(userId);
            _userRepository.Get(userId).Returns(user);

            await Act(command);

            user.PersonalInfo.ShouldNotBe(null);
            user.PersonalInfo.Nickname.ShouldBe(command.Nickname);
            user.PersonalInfo.Description.ShouldBe(command.Description);
            user.PersonalInfo.Sex.ShouldBe(Sex.From(command.Sex.GetValueOrDefault()));
            user.PersonalInfo.BirthDate.ShouldBe(command.BirthDate);
        }
    }
}
