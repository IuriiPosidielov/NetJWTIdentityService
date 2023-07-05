using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace service.Models
{
    public class JwtSettings
    {
        public required string SecurityKey { get; set; }
        public required string Issuer { get; set; }
        public required string Audence {get; set; }
    }
}