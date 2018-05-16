using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Interfaces
{
    /// <summary>
    /// Interface for union operations 
    /// with database in one transaction
    /// </summary>
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; }

        IUserRepository UserRepository { get; }

        void Commit();
    }
}
