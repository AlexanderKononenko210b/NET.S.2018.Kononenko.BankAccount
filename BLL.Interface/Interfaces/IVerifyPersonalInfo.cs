using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Interfaces
{
    /// <summary>
    /// Strategy validate string input
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IVerifyPersonalInfo<in T>
    {
        Tuple<bool, string> IsVerify(string inputInfo, IEnumerable<T> strategyValidate);
    }
}
