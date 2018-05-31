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
    /// Interface for AccountRepository
    /// </summary>
    public interface IAccountRepository : IRepository<AccountDto, AccountDbModel>
    {
        AccountDto Get(string number);

        IEnumerable<AccountDto> GetAll();

        IEnumerable<string> GetNumbers(int userId);
    }
}
