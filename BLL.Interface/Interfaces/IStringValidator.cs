using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Interfaces
{
    /// <summary>
    /// Interface for check input information type string
    /// </summary>
    public interface IStringValidator
    {
        Tuple<bool, string> IsValid(string info);
    }
}
