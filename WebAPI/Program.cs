using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Extensions;
using Microsoft.AspNetCore.Builder;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            //Autofac, Ninject, Castledwindsor, StructureMap, LightInject , DryInjet__> IOC ile ayn� g�reveri g�r�yor::
            //

            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // builder.Services.AddSingleton<IProductService, ProductManeger>();//bana arka planda bir referance olu�tur �OC conteyner bizim i�in arka tarafta neeliyor
            ////***** //biri catorda �prdoct service isterse bir tane ona producmanger ver  ver demek 
            // //biri bizden �producservice isterse ona birtane arka tarafta productmanger olu�tur ona ver 
            // //singleton i�erisinde data tutmuyorsak kulan�r�z ��nk� bir milyondan daha fazla instens olu�turmam�z�n �n�ne ge�iyor 
            // //******datal� addscopep veya addTransient:
            // builder.Services.AddSingleton<IProductDal , EfProductDal>();





            //ba�ka bir ioc konteyner kulanmak istersek bunu yapar�z::::
            //ioc konteyner olarak art�c autofac deyiz:::::
                         //servis sa�lay�c� fabrikas� olarak kulan 
                         //ba�ka bir ioc conteyner kulanmak istersek bunu uygular�z::::
            builder.Host.UseServiceProviderFactory(services => new AutofacServiceProviderFactory())
                     .ConfigureContainer<ContainerBuilder>(builder => 
                     { 
                         builder.RegisterModule(new AutofacBusinessModule());
                     });

            builder.Services.AddCors();

            var tokenOptions = builder.Configuration.GetSection(key: "TokenOptions").Get<TokenOptions>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidIssuer = tokenOptions.Issuer,
                         ValidAudience = tokenOptions.Audience,
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = SecurityKeyHalper.CreatSecurityKey(tokenOptions.SecurityKey)
                     };
                 });
            //   builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//bunu siliyor
            // Core.Utilities.IoC.ServiceTool.Create(builder.Services);
            
            builder.Services.AddDependencyResolvers(new ICoreModule[]
           {
                new CoreModule(),
           });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.ConfigureCustomExceptionMiddleware();

            // app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());

            // CORS middleware ekleyin


            app.UseHttpsRedirection();

            app.UseAuthentication();// midile veyr asp net ya�am d�ng�s�nde hangi yap�lar�n ss�ras�yla devreye girece�ini s�yl�yoruz 
            app.UseAuthorization();
           

            app.MapControllers();

            app.Run();
        }
    }
}