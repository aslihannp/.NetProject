using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
//Burayı Northwind projesine bağlı kalmamak için yapıyoruz

namespace Core.DataAccess
{
    //T'nin alabileceği değerleri sınırlandıralım: Generic Constraint
    //class : referans tip
    //IEntity : IEntity olabilir veya IEntity implemente eden bir nesne olabilir
    //new() : new lenebilir olmalı
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        List<T> GetAll(Expression<Func<T,bool>> filter=null); //Bu çirkin şey filitreleyerek getirilmesini sağlıyo
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }

    //YILDIZLI NOT: CORE KATMANI DİĞER KATMANLARI REFERANS ALMAZ. Alırsa o katmana bağımlı olmuş olur. Core sayesinde sadece nothwind projesinde değil car rental projesinde de kullanabilicem
}
