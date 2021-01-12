using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitMeApp.Indentity.Application.Models
{
    public class JwtOptions
    {
        public string SecretKey { get; init; }
        public long ExpirationTimeInMs { get; init; }
        public string Issuer { get; init; }
    }
}
