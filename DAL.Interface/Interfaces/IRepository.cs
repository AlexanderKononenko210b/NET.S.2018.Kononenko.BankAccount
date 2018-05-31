using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DAL.Interface.DbModels;

namespace DAL.Interface.Interfaces
{
    /// <summary>
    /// Interface repository with CRUD operation for type
    /// </summary>
    /// <typeparam name="T">type instance for work</typeparam>
    public interface IRepository<T,P>
        where T : Entity
        where P : Entity
    {
        T Add(T model);

        T Get(int id);

        T Update(T model);

        T Delete(T model);

        void Commit();
    }
}
