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
    /// Class for configurations table descride account in database
    /// </summary>
    public class AccountConfiguration : EntityTypeConfiguration<AccountDbModel>
    {
        public AccountConfiguration()
        {
            HasKey(p => p.Id);
            Property(p => p.NumberOfAccount).IsRequired();
            Property(p => p.Balance).IsRequired();
            Property(p => p.BenefitPoints).IsRequired();
            Property(p => p.IsClosed).IsRequired();
            Property(p => p.UserId).IsRequired();
            Property(p => p.TypeId).IsRequired();
            Property(p => p.CreatorId).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            Property(p => p.ModifierId).IsOptional();
            Property(p => p.ModifiedDate).IsOptional();
            HasRequired(p => p.User)
                .WithMany(p => p.Accounts)
                .HasForeignKey(p => p.UserId);
            HasRequired(p => p.Type)
                .WithMany(p => p.Accounts)
                .HasForeignKey(p => p.TypeId);
        }
    }
}
