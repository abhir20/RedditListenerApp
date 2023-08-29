using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    interface IUnitOfWork
    {
        void Commit();
        void Rollback();
        
        IRepository<T> Repository<T>() where T : class;
    }
}
