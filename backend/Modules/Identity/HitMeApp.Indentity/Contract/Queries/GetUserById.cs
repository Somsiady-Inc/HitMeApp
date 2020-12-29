using System;
using HitMeApp.Indentity.Contract.Dtos;

namespace HitMeApp.Indentity.Contract.Queries
{
    public class GetUserById : IIdentityQuery<UserDto>
    {
        public Guid Id { get; init; }

        public GetUserById(Guid id)
        {
            Id = id;
        }
    }
}
