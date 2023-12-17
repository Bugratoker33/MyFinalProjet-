using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{ //gneric constraint 
    //generic kısıt
    //class :demek referance tip demek 
    // Ientityrepository kısıtladık yani t class olabilir referance tip yadan ıEntitiyden implemente olan birşey olabilir
    //Ientitiy olabilir yada Ientity implemnete eden bir nesen olabilir 
    //New():New lenebilir olmalı ıentity newlenemz 
    public interface IEntityRepository<T> where T: class,IEntity,new()
    {

        //expression filitreleme özeliği listeleme yapınca mesala sadece kategory ıd si 2 olanı listeme gibi 
        //filte de vermeyebiliriz 
        //Temel oparasyonların imzası var
        List<T> GetAll(Expression<Func<T,bool>> filter=null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

       
    }
}
