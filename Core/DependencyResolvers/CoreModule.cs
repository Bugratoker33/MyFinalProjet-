using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;//buna ihtiyacımız var 
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
          serviceCollection.AddMemoryCache();//.nete özgü//memory cash olduğunu söyledik  IMemoryCache 
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//uygulama seviyesinde servis bağımlılıklarımızı çözümleyeceğimiz yer
           serviceCollection.AddSingleton<ICacheManeger, MemoryCacheManeger>(); // birisi senden ıcashmaneger isterse sende ona momorycahmaneger ver
           // serviceCollection.AddSingleton<Stopwatch>();
            serviceCollection.AddSingleton<Stopwatch>();
        }
    }
}
