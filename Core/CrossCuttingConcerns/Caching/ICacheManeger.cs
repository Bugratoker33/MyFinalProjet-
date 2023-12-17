using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManeger //memory kulanıcaz başka bir şekilde lochcash de yapabiliriz:
    {
        //hangi tiple çalıştığımızı ve hangi tipe dönüştürdüğümüzü söylüyor olacağız
        T Get<T>(string key); //ben sana key veriyim sen belekten o key karşılık gelen datayı ver:;

        object Get(string key);// generic olmayan tip 

        void Add(string key, object value, int duration);//duration ne kadar zaman olucak 

        bool IsAdd(string key);//cashde var mı varsa data getir yoksa veri tabanından getir;
        void Remove(string key);//cashden silme 

        void RemoveByPattern(string pattern );//başı sonu önemli değil içinde get olanları sil (key);


    }
}
