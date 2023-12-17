using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{//static olamsı gerekir extentions methodlarının
    public static class ServiceCollectionExtensioins// ioc için core mudulundeki yazılan bağımlılıkları burdan servicetool vasıtasıyla halediyoruz this yazarak methodu genişletiyoruz 
    {//polimorfizim 
      public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection , ICoreModule[] modules)//core modul veye cash mdul 
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }
            return ServiceTool.Create(serviceCollection);
        }
    }//IServiceCollection aspnet uygulamızın apimizin services bağımlılıklarını eklediğimiz koleksiyonun ta kendisi 
}
