using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DbModels;

namespace DAL.DbModelConfigurations
{
    /// <summary>
    /// Class configuration entity AccountTypeDbModel
    /// </summary>
    public class AccountTypeDbModelConfiguration : EntityTypeConfiguration<AccountTypeDbModel>
    {
        public AccountTypeDbModelConfiguration()
        {
            HasKey(p => p.Id);
            Property(p => p.Type).IsRequired();
        }
    }
}
