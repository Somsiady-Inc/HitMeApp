using HitMeApp.Shared.DDD;

namespace HitMeApp.Users.Core.Events
{
    internal class UserProfileUpdated : DomainEvent
    {
        public UserId UserId { get; }
        PersonalInfo PersonalInfo { get; }
        public bool Complete { get; }

        public UserProfileUpdated(UserId userId, PersonalInfo personalInfo, bool complete)
        {
            UserId = userId;
            PersonalInfo = personalInfo;
            Complete = complete;
        }
    }
}
