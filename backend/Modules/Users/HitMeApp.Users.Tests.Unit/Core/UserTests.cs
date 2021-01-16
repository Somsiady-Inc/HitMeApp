using System;
using System.Linq;
using HitMeApp.Shared.DDD.Exceptions;
using HitMeApp.Users.Core;
using HitMeApp.Users.Core.Events;
using HitMeApp.Users.Core.Specification;
using NSubstitute;
using Shouldly;
using Xunit;

namespace HitMeApp.Users.Tests.Unit.Core
{
    public class UserTests
    {
        private readonly User _user;

        public UserTests()
        {
            _user = User.Incomplete(Guid.NewGuid());
        }

        [Fact]
        public void given_invalid_id_incomplete_user_should_not_be_created()
        {
            var exception = Record.Exception(() => User.Incomplete(Guid.Empty));
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidEntityIdException>();
        }

        [Fact]
        public void given_valid_id_incomplete_used_should_be_created()
        {
            var userId = new UserId(Guid.NewGuid());
            var user = User.Incomplete(userId);

            user.ShouldNotBeNull();
            user.Id.ShouldBe(userId);
        }

        [Fact]
        public void add_preference_should_take_effect_if_preference_was_not_previously_added()
        {
            var newTrait = Trait.New("Test");
            _user.AddPreference(newTrait);

            _user.DomainEvents.Count().ShouldBe(1);
            var domainEvent = _user.DomainEvents.First();
            var userAddedPreference = domainEvent.ShouldBeOfType<UserAddedPreference>();
            userAddedPreference.UserId.ShouldBe(_user.Id);
            userAddedPreference.Preference.ShouldBe(newTrait);
            _user.Preferences.First().ShouldBe(newTrait);
        }

        [Fact]
        public void add_preference_should_not_take_effect_if_preference_was_previously_added()
        {
            var preferenceToAdd = Trait.New("Test");
            _user.AddPreference(preferenceToAdd);
            _user.ClearEvents();

            var loadedPreferenceToAdd = Trait.Load(preferenceToAdd.Id, preferenceToAdd.Value);
            _user.AddPreference(loadedPreferenceToAdd);

            _user.DomainEvents.Count().ShouldBe(0);
        }

        [Fact]
        public void remove_preference_should_take_effect_if_preferece_was_previously_added()
        {
            var preferenceToRemove = Trait.New("Test");
            _user.AddPreference(preferenceToRemove);
            _user.ClearEvents();

            var loadedPreferenceToRemove = Trait.Load(preferenceToRemove.Id, preferenceToRemove.Value);
            _user.RemovePreference(loadedPreferenceToRemove);

            _user.DomainEvents.Count().ShouldBe(1);
            var domainEvent = _user.DomainEvents.First();
            var userRemovedPreference = domainEvent.ShouldBeOfType<UserRemovedPreference>();
            userRemovedPreference.UserId.ShouldBe(_user.Id);
            userRemovedPreference.Preference.Id.ShouldBe(loadedPreferenceToRemove.Id);
            userRemovedPreference.Preference.Value.ShouldBe(loadedPreferenceToRemove.Value);
        }

        [Fact]
        public void remove_preference_should_not_take_effect_if_preference_was_not_previously_added()
        {
            _user.RemovePreference(Trait.New("Test"));

            _user.DomainEvents.Count().ShouldBe(0);
        }

        [Fact]
        public void remove_preference_should_only_remove_requested_preference()
        {
            var preferenceToRemove = Trait.New("Test2");
            _user.AddPreference(Trait.New("Test"));
            _user.AddPreference(preferenceToRemove);
            _user.AddPreference(Trait.New("Test3"));
            _user.ClearEvents();

            var loadedPreferenceToRemove = Trait.Load(preferenceToRemove.Id, preferenceToRemove.Value);
            _user.RemovePreference(loadedPreferenceToRemove);

            _user.Preferences.Count().ShouldBe(2);
            _user.DomainEvents.Count().ShouldBe(1);
            var domainEvent = _user.DomainEvents.First();
            var userRemovedPreference = domainEvent.ShouldBeOfType<UserRemovedPreference>();
            userRemovedPreference.UserId.ShouldBe(_user.Id);
            userRemovedPreference.Preference.ShouldBe(preferenceToRemove);
        }

        [Fact]
        public void add_trait_should_take_effect_if_trait_was_not_previously_added()
        {
            var newTrait = Trait.New("Test");
            _user.AddTrait(newTrait);

            _user.DomainEvents.Count().ShouldBe(1);
            var domainEvent = _user.DomainEvents.First();
            var userAddedTrait = domainEvent.ShouldBeOfType<UserAddedTrait>();
            userAddedTrait.UserId.ShouldBe(_user.Id);
            userAddedTrait.Trait.ShouldBe(newTrait);
            _user.Traits.First().ShouldBe(newTrait);
        }

        [Fact]
        public void add_trait_should_not_take_effect_if_trait_was_previously_added()
        {
            var traitToAdd = Trait.New("Test");
            _user.AddTrait(traitToAdd);
            _user.ClearEvents();

            var loadedTraitToAdd = Trait.Load(traitToAdd.Id, traitToAdd.Value);
            _user.AddTrait(loadedTraitToAdd);

            _user.DomainEvents.Count().ShouldBe(0);
        }

        [Fact]
        public void remove_trait_should_take_effect_if_trait_was_previously_added()
        {
            var traitToRemove = Trait.New("Test");
            _user.AddTrait(traitToRemove);
            _user.ClearEvents();

            var loadedTraitToRemove = Trait.Load(traitToRemove.Id, traitToRemove.Value);
            _user.RemoveTrait(loadedTraitToRemove);

            _user.DomainEvents.Count().ShouldBe(1);
            var domainEvent = _user.DomainEvents.First();
            var userRemovedTrait = domainEvent.ShouldBeOfType<UserRemovedTrait>();
            userRemovedTrait.UserId.ShouldBe(_user.Id);
            userRemovedTrait.Trait.Id.ShouldBe(loadedTraitToRemove.Id);
            userRemovedTrait.Trait.Value.ShouldBe(loadedTraitToRemove.Value);
        }

        [Fact]
        public void remove_trait_should_not_take_effect_if_trait_was_not_previously_added()
        {
            _user.RemoveTrait(Trait.New("Test"));

            _user.DomainEvents.Count().ShouldBe(0);
        }

        [Fact]
        public void remove_trait_should_only_remove_requested_trait()
        {
            var traitToRemove = Trait.New("Test2");
            _user.AddTrait(Trait.New("Test"));
            _user.AddTrait(traitToRemove);
            _user.AddTrait(Trait.New("Test3"));
            _user.ClearEvents();

            var loadedTraitToRemove = Trait.Load(traitToRemove.Id, traitToRemove.Value);
            _user.RemoveTrait(loadedTraitToRemove);

            _user.Traits.Count().ShouldBe(2);
            _user.DomainEvents.Count().ShouldBe(1);
            var domainEvent = _user.DomainEvents.First();
            var userRemovedTrait = domainEvent.ShouldBeOfType<UserRemovedTrait>();
            userRemovedTrait.UserId.ShouldBe(_user.Id);
            userRemovedTrait.Trait.ShouldBe(traitToRemove);
        }

        [Fact]
        public void change_personal_info_with_incomplete_data_should_mark_user_as_incomplete()
        {
            var minimalAgeSpecificationMock = Substitute.For<IMinimalAgeSpecification>();
            var personalInfo = new PersonalInfo(null, null, null, null, minimalAgeSpecificationMock);
            minimalAgeSpecificationMock.IsSatisfiedBy(personalInfo).Returns(false);

            _user.ChangePersonalInfo(personalInfo);

            _user.Complete.ShouldBe(false);
        }

        [Fact]
        public void change_personal_info_with_complete_data_should_mark_user_as_complete()
        {
            var minimalAgeSpecificationMock = Substitute.For<IMinimalAgeSpecification>();
            var personalInfo = new PersonalInfo(
                "Test",
                "Test Description",
                new DateTime(1920, 5, 18),
                Sex.Male,
                minimalAgeSpecificationMock
            );
            minimalAgeSpecificationMock.IsSatisfiedBy(personalInfo).Returns(true);

            _user.ChangePersonalInfo(personalInfo);

            _user.Complete.ShouldBe(true);
        }

        [Fact]
        public void given_valid_location_change_location_should_succeed()
        {
            var location = Location.New(41.902222222222, 12.456388888889);

            _user.ChangeLocation(location);

            _user.DomainEvents.Count().ShouldBe(1);
            var domainEvent = _user.DomainEvents.First();
            var userLocationChanged = domainEvent.ShouldBeOfType<UserLocationChanged>();
            userLocationChanged.ShouldNotBeNull();
            userLocationChanged.UserId.ShouldBe(_user.Id);
            userLocationChanged.Location.ShouldBe(location);
        }
    }
}
