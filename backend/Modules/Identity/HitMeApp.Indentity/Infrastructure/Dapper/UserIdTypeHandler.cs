using System;
using System.Data;
using Dapper;
using HitMeApp.Indentity.Core;

namespace HitMeApp.Indentity.Infrastructure.Dapper
{
    internal sealed class UserIdTypeHandler : SqlMapper.TypeHandler<UserId>
    {
        public override UserId Parse(object value)
            => value is Guid id ? new UserId(id) : null;

        public override void SetValue(IDbDataParameter parameter, UserId value)
        {
            parameter.Value = value.Value;
        }
    }
}
