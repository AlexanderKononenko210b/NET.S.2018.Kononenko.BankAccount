using System.Data.Entity;
using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using BLL.Mappers;
using BLL.Service;
using DAL.Context;
using DAL.Fake;
using DAL.Interface.Dto;
using DAL.Interface.Interfaces;
using DAL.Repositories;
using Ninject;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolver(this IKernel kernel)
        {
            kernel.Bind<DbContext>().To<AccountContext>().InSingletonScope();
            kernel.Bind<AccountContext>().ToSelf().InSingletonScope();
            kernel.Bind<IAccountNumberCreateService>().To<NumberCreateService>().InSingletonScope();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IAccountRepository>().To<AccountRepository>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IAccountService>().To<AccountService>();
        }
    }
}
