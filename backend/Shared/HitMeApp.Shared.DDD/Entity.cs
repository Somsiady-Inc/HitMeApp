namespace HitMeApp.Shared.DDD
{
    public abstract class Entity<TEntityId> where TEntityId : EntityId
    {
        public TEntityId Id { get; protected init; }

        protected Entity()
        {
            // Frameworks usage
        }

        protected Entity(TEntityId id)
        {
            Id = id;
        }
    }
}
