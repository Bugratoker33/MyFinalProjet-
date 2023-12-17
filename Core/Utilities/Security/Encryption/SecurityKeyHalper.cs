using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    //uydurunk sitrigii byte arraye çevirir json 
    //smetrik anahtar halini gelirir 
    public class SecurityKeyHalper
    {
        public static SecurityKey CreatSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

        }
    }
}
