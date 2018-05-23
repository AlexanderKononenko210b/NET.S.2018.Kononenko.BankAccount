using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DbModels;
using DAL.Interface.Dto;

namespace DAL.Interface.Interfaces
{
    /// <summary>
    /// Interface consists methods specify for type UserInfoDto 
    /// </summary>
    public interface IUserRepository : IRepository<UserInfoDto, UserInfoDbModel>
    {
        IEnumerable<UserInfoDto> GetAll();
    }
}
