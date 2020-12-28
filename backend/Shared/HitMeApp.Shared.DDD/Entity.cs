﻿namespace HitMeApp.Shared.DDD
{
    public abstract class Entity<TEntityId> where TEntityId : EntityId
    {
        public TEntityId Id { get; protected init; }
    }
}
