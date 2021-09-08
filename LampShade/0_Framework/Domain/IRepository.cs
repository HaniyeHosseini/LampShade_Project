﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Domain
{
    public interface IRepository <TKey , T> where  T : class 
    {
        T GetBy(TKey id);
        List<T> Get();

        void Create(T entiti);
        bool Exist(Expression<Func<T, bool>> expression);

        void Save();

    }
}
