using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Interfaces
{
    /// <summary>
    /// Interface for operation with user information
    /// </summary>
    public interface IUserService
    {
        PersonalInfo Create(string firstName, string lastName, string passport, string email);
    }
}
