using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Dto;
using BLL.Interface.Entities;
using BLL.Interface.Enum;

namespace BLL.Interface.Interfaces
{
    /// <summary>
    /// Interface AccountService
    /// </summary>
    public interface IAccountService
    {
        IEnumerable<AccountViewDto> GetAll();

        AccountViewDto Get(int id);

        AccountViewDto GetByNumber(string number);

        IEnumerable<string> GetAllNumbers(int userId);

        AccountViewDto OpenAccount(string type, int userId);

        AccountViewDto DepositAccount(string numberAccount, decimal deposit);

        AccountViewDto WithDrawAccount(string numberAccount, decimal withdraw);

        bool Close(AccountViewDto account);

        (AccountViewDto, AccountViewDto) Transfer(string numberFirst, string numberSecond, decimal transfer);
    }
}
