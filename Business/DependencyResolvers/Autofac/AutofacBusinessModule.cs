using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Business.CSS;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule :Module
    {// sen bir aotofac modulüsün dedik 
       
        protected override void Load(ContainerBuilder builder)
        { //uygulama ayağa kalktığında burası çalışıcak

            builder.RegisterType<ProductManeger>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

            builder.RegisterType<CategoryManeger>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();


            //DATA TUTMADIĞIMIZ İÇİN SİNGELİNSTANCE KULANIYORUZ 
            //BİRİSİ BİZDEN IPRODUCTDAL İSTERSE EFPRDOUCTDAL VER
            //KONSTRACTIR KULANIRKEN HER SEFERİNDE NEWLMWİYORUZ ARTIK BİZİM YERİMİZİ BU METHOT BİR KERE YAPIYOR VE HGEP ONU KULANIYOR.
            //BİRİSİ BİZDEN IPROUCTSERVİCE İSTERSE ONA PRODUCTMANEGER İNSTANCE ÖRNEĞİNİ VER DEMEK 
            // builder.registery type = builder.Services.AddSingleton<IProductService, ProductManeger>();

            //kod başlandığında validat edilen ve aspec olarak kulanılan kodları bak 

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
