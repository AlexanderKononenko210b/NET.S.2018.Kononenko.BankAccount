using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Dto;
using BLL.Interface.Entities;

namespace BLL.Interface.Interfaces
{
    /// <summary>
    /// Interface for operation with user information
    /// </summary>
    public interface IUserService
    {
        UserInfo Create(string firstName, string lastName, string passport, string email);

        UserViewDto Get(int id);
    }
}
