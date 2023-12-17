using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken
    {// kulsnıcı podtmende kulanıcı adı ve şifresi vericek bizde ona token vericez

        public string Token { get; set; }
        public DateTime Expiration { get; set; }

    }
}
