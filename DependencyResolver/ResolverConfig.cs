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
using Ninject.Web.Common;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }

        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }

        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                kernel.Bind<DbContext>().To<AccountContext>().InRequestScope();
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            }
            else
            {
                kernel.Bind<DbContext>().To<AccountContext>().InSingletonScope();
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
            }

            kernel.Bind<IAccountNumberCreateService>().To<NumberCreateService>().InSingletonScope();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IAccountRepository>().To<AccountRepository>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IAccountService>().To<AccountService>();
        }
    }
}
