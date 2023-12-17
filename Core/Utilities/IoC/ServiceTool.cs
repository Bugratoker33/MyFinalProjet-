using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }  //injection değerni tutmak içinn yazdık 

        public static IServiceCollection Create(IServiceCollection services)//.netin servislerini kularak ve 
        {
            ServiceProvider = services.BuildServiceProvider();//onlari build et apide ve o injetionları oluşturmayı sağlar 
            return services;
        }
    }
}
