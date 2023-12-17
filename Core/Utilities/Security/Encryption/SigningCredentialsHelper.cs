using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper//imzalamak demek ::::
        //webapide jwt servislerimizin web apinin kulanabileceği jwt oluşturailmesi için burda ne var elimizde birtane anahtara ihtiyacımız var dıya elimizdeki security key formatında vericez ve o da bize imzalam nesnesini döndürüyor olucak 
        //credionatal sistem girebilmemiz için elimzdeki olanlardır:
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            // asp nete diyoruz ki anahtar olarak SecurityKey kulan şifreleme olarak da güvenlik algoritmalardınan sha512 kulan 

        }
    }
}
