using Core.Entities.Concrete;//
using Core.Utilities.Security.Encryption;//
//using Microsoft.Extensions.Configuration;//
using Microsoft.IdentityModel.Tokens;//
using System.IdentityModel.Tokens.Jwt;//oluşturmak serileştirmek ve doğrullamak için jwt bir yapıdır
using System.Collections.Generic;
using System.Security.Claims;
using Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }//apimizdeki apsetings okumaya yarıyor
        private TokenOptions _tokenOptions;
        //Iconfigiration ile apiseting deki okuduğumuz değerleri atıcağımız nesne
        private DateTime _accessTokenExpiration;//accses token ne zaman geçersizleşecek 
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            //getsection demek apideki token optionsu al 
            //section demek token opsion api setingdeki 
            //onu al ve token opionsa at


        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)//bu kulnıcı için token oluşturacağız 
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);//bu token ne zaman bitecek 
            var securityKey = SecurityKeyHalper.CreatSecurityKey(_tokenOptions.SecurityKey);//güvenlik anahtarına ihtiyacım var 
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);//hangi algoritmayı kulanayım ve anahtar nedir
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);//yetkileri atamışız
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),//operantions clamleri atatığımız yer 
                signingCredentials: signingCredentials
            );
            return jwt;
        }
        //Bu kod parçası, kullanıcının JWT içinde taşınacak taleplerini (claims) ayarlar.JWT'nin doğrulanması ve yetkilendirilmesi için kullanılırlar//kulanıca karşılık gelen claimler :
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();//.netin içinde olan birşey
            //.nete var olan methode yeni methotlar ekliyebiliriz (extentions)
             
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());//rolerini ekliyoruz veri tabanından ismi çekip atıyoruz

            return claims;
        }
    }
}
