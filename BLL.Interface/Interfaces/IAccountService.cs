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

        Account GetByNumber(string number);

        Account OpenAccount(AccountType type, int userId, IAccountNumberCreateService creator);

        decimal DepositAccount(Account account, decimal deposit);

        decimal WithDrawAccount(Account account, decimal withdraw);

        bool Close(Account account);

        (Account, Account) Transfer(Account first, Account second, decimal transfer);
    }
}
