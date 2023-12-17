using Business.Constants;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;
namespace Business.BusinessAspects.Autofac
{
    //JWT TOKEN İÇİN :

    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;//jwt gödererk bir istek yapıyoruz ya her bir istek için bir http context oluşturur 

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');//mangerde roler vermiştil securedoptions(product.add,admin) gibi 
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();//injection alt yapımızı okuyabilen araç 
            // otofecte injection yapar 

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();//claim rolerini bul bakalım 
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))//eğer claimlerinin içinde ilgili role varse return et yani methodu çalıştırmaya devam et 
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);//yoksa yetkin yok mesaagi ver  ver :
        }
    }
}
