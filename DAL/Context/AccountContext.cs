using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DbModelConfigurations;
using DAL.Initializers;
using DAL.Interface.DbModels;
using DAL.Interface.Dto;

namespace DAL.Context
{
    /// <summary>
    /// Context for work with account and user entity
    /// </summary>
    public class AccountContext : DbContext
    {
        public AccountContext()
            : base("AccountConnection")
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        public IDbSet<AccountDbModel> Accounts { get; set; }

        public IDbSet<UserInfoDbModel> Users { get; set; }

        public IDbSet<AccountTypeDbModel> AccountTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AccountConfiguration());
            modelBuilder.Configurations.Add(new UserInfoConfiguration());
            modelBuilder.Configurations.Add(new AccountTypeDbModelConfiguration());
        }
    }
}
