﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        T GetById(object id);
        IList<T> GetAll();
        void Add(T entity);
    }
}
