namespace HitMeApp.Shared.DDD.Exceptions
{
    public class InvalidAggregateIdException : DomainException
    {
        public override string Code => "invalid_aggregate_id";

        public InvalidAggregateIdException() : base("Aggregate ID cannot be empty")
        {
        }
    }
}
