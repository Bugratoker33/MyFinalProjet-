using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManeger : ICacheManeger
    {
        //adapter pettern :

        IMemoryCache _memoryCashe;//microsoftun özeliği //default bir şekilde geldi 
        //ctor iş görmez çünkü zincir web api  busines dataacsees 
        //aspectçok farklı bir zincir servicetol oluşturduk ICoremodule diye bizde aspectide zincire kattık 
        //bunuda service toll eklemeliyiz 

        public MemoryCacheManeger()
        {
            _memoryCashe=ServiceTool.ServiceProvider.GetService<IMemoryCache>();//core da yaptığımız memory cachein karşılığını vere (coremodule );

        }
        public void Add(string key, object value, int duration)
        {
            _memoryCashe.Set(key, value, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            return _memoryCashe.Get<T>(key); 
        }

        public object Get(string key)
        {
            return _memoryCashe.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCashe.TryGetValue(key, out _);//belekte böle bir anahtar var mı ona bak datayı istemiyorum (out _) datayı verme 
        }

        public void Remove(string key)
        {
           _memoryCashe.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {//çalışma anında bellekten silmeye yarıyor 
            dynamic cacheEntriesCollection = null;            //memorycache IMemorycache
            var cacheEntriesFieldCollectionDefinition = typeof(MemoryCache).GetField("_coherentState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            // git beleğe bak belekte memorycache türünde olan entriescollectionun içine at 
            var cacheEntriesPropertyCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            if (cacheEntriesFieldCollectionDefinition != null)
            {
                var coherentStateValueCollection = cacheEntriesFieldCollectionDefinition.GetValue(_memoryCashe);
                var entriesCollectionValueCollection = coherentStateValueCollection?.GetType()
                    .GetProperty(
                        "EntriesCollection",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
                    );


                cacheEntriesCollection = entriesCollectionValueCollection.GetValue(coherentStateValueCollection);
            }


            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();
            foreach (var cacheItem in cacheEntriesCollection)
            {
                var cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }


            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCashe.Remove(key);
            }

        }
    }
}
