using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Interfaces
{
    /// <summary>
    /// Interface repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T Add(T model);

        T Get(string number);

        T Get(int id);

        T Update(T model);

        T Delete(T model);
    }
}
