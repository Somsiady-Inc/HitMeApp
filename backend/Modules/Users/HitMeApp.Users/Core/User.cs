using System;
using System.Collections.Generic;
using System.Linq;
using HitMeApp.Shared.DDD;
using HitMeApp.Users.Core.Events;

namespace HitMeApp.Users.Core
{
    internal sealed class UserId : EntityId
    {
        public UserId(Guid id) : base(id)
        {
        }

        public static implicit operator UserId(Guid guid)
            => new UserId(guid);
    }

    internal class User : AggregateRoot<UserId>
    {
        private readonly ISet<Trait> _traits;
        private readonly ISet<Trait> _preferences;

        public Location Location { get; private set; }
        public IEnumerable<Trait> Traits => _traits;
        public IEnumerable<Trait> Preferences => _preferences;
        public PersonalInfo PersonalInfo { get; private set; }
        public bool Complete => PersonalInfo is { } && PersonalInfo.Valid;

        public static User Incomplete(Guid id)
            => new User(id);

        public static User Load(Guid id, IEnumerable<Trait> traits, IEnumerable<Trait> preferences, PersonalInfo personalInfo)
            => new User(id, traits, preferences, personalInfo);

        protected User(UserId id, IEnumerable<Trait> traits = null, IEnumerable<Trait> preferences = null, PersonalInfo personalInfo = null)
            : base(id)
        {
            _traits = traits?.ToHashSet() ?? new HashSet<Trait>();
            _preferences = preferences?.ToHashSet() ?? new HashSet<Trait>();
            PersonalInfo = personalInfo;
        }

        public void AddPreference(Trait preference)
        {
            // TODO: Maybe we should add a preference limit?
            if (!IsPreferenceAlreadyAdded(preference))
            {
                _preferences.Add(preference);
                RaiseDomainEvent(new UserAddedPreference(Id, preference));
            }
        }

        public void RemovePreference(Trait preference)
        {
            // TODO: Should we have required preferences?
            if (IsPreferenceAlreadyAdded(preference))
            {
                var preferenceToRemove = _preferences.First(p => p.Id == preference.Id);
                _preferences.Remove(preferenceToRemove);
                RaiseDomainEvent(new UserRemovedPreference(Id, preferenceToRemove));
            }
        }

        public void AddTrait(Trait trait)
        {
            if (!IsTraitAlreadyAdded(trait))
            {
                _traits.Add(trait);
                RaiseDomainEvent(new UserAddedTrait(Id, trait));
            }
        }

        public void RemoveTrait(Trait trait)
        {
            if (IsTraitAlreadyAdded(trait))
            {
                var traitToRemove = _traits.First(p => p.Id == trait.Id);
                _traits.Remove(traitToRemove);
                RaiseDomainEvent(new UserRemovedTrait(Id, traitToRemove));
            }
        }

        public void ChangePersonalInfo(PersonalInfo newPersonalInfo)
        {
            PersonalInfo = newPersonalInfo;
            RaiseDomainEvent(new UserProfileUpdated(Id, PersonalInfo, Complete));
        }

        public void ChangeLocation(Location location)
        {
            Location = location;
            RaiseDomainEvent(new UserLocationChanged(Id, Location));
        }

        private bool IsPreferenceAlreadyAdded(Trait preference)
            => _preferences.Any(p => p.Id == preference.Id);

        private bool IsTraitAlreadyAdded(Trait trait)
            => _traits.Any(p => p.Id == trait.Id);
    }
}
