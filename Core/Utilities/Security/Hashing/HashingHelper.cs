using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        //biz burda şifre vericez ve dışarıya hash ve saltı çıkarıcak 
                         //CratePasswordHash
        public static void CreatPasswordHash(string password,out byte[] passwordHash ,out byte[] passwordSalt  )//ona verdiğimiz passwordu hash liyecek 
        {
           //.net in kriptografiklerini kulanıcaz 
            using (var hmc= new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmc.Key;
                passwordHash = hmc.ComputeHash(Encoding.UTF8.GetBytes(password));
                //passwor vericez onu byte döüştürüp hash ve salt yapıcaz
            }

        }
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmc = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
               var ComputhedHash= hmc.ComputeHash(Encoding.UTF8.GetBytes(password));//parolayı tekrardan oluşturuyoruz çünkü salt ile karşılaştıracağız 

                for (int i = 0; i < ComputhedHash.Length; i++)
                {
                    if (ComputhedHash[i] != passwordHash[i])
                    {
                        return false;
                    }

                }
                return true;
            }
        }
    }
}
