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

            //Autofac, Ninject, Castledwindsor, StructureMap, LightInject , DryInjet__> IOC ile ayný göreveri görüyor::
            //

            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // builder.Services.AddSingleton<IProductService, ProductManeger>();//bana arka planda bir referance oluþtur ÝOC conteyner bizim için arka tarafta neeliyor
            ////***** //biri catorda ýprdoct service isterse bir tane ona producmanger ver  ver demek 
            // //biri bizden ýproducservice isterse ona birtane arka tarafta productmanger oluþtur ona ver 
            // //singleton içerisinde data tutmuyorsak kulanýrýz çünkü bir milyondan daha fazla instens oluþturmamýzýn önüne geçiyor 
            // //******datalý addscopep veya addTransient:
            // builder.Services.AddSingleton<IProductDal , EfProductDal>();





            //baþka bir ioc konteyner kulanmak istersek bunu yaparýz::::
            //ioc konteyner olarak artýc autofac deyiz:::::
                         //servis saðlayýcý fabrikasý olarak kulan 
                         //baþka bir ioc conteyner kulanmak istersek bunu uygularýz::::
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

            app.UseAuthentication();// midile veyr asp net yaþam döngüsünde hangi yapýlarýn ssýrasýyla devreye gireceðini söylüyoruz 
            app.UseAuthorization();
           

            app.MapControllers();

            app.Run();
        }
    }
}