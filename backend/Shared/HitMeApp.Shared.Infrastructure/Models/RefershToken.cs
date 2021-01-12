using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitMeApp.Indentity.Application.Models
{
    public class RefershToken
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public bool Revoked { get; set; }
    }
}
