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
    public class UserInfoConfiguration : EntityTypeConfiguration<UserInfoDbModel>
    {
        public UserInfoConfiguration()
        {
            HasKey(p => p.Id);
            Property(p => p.FirstName).IsRequired();
            Property(p => p.LastName).IsRequired();
            Property(p => p.Passport).IsOptional();
            Property(p => p.Email).IsOptional();
            Property(p => p.CreatorId).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            Property(p => p.ModifierId).IsOptional();
            Property(p => p.ModifiedDate).IsOptional();
        }
    }
}
