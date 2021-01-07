using System;

namespace HitMeApp.Indentity.Contract.Commands
{
    public class ChangeUserPassword : IIdentityCommand
    {
        public Guid Guid { get; set; }
        public string CurrentPassword { get; init; }
        public string NewPassword { get; init; }
    }
}
