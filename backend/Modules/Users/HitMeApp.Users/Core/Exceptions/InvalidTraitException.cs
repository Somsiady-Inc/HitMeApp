using HitMeApp.Shared.DDD;

namespace HitMeApp.Users.Core.Exceptions
{
    internal class InvalidTraitException : DomainException
    {
        public override string Code => "invalid_trait";

        public string TraitValue { get; }
        public int MaxTraitLength { get; }

        public InvalidTraitException(string traitValue, int maxTraitLength)
            : base($"Valid trait should not be empty and have a max length of {maxTraitLength}")
        {
            TraitValue = traitValue;
            MaxTraitLength = maxTraitLength;
        }
    }
}
