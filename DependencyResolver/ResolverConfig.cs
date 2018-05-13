using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using BLL.Mappers;
using BLL.Service;
using DAL.Fake;
using DAL.Fake.Repositories;
using DAL.Interface.DTO;
using DAL.Interface.Interfaces;
using DAL.Repositories;
using Ninject;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolver(this IKernel kernel)
        {
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IAccountNumberCreateService>().To<NumberCreateService>().InSingletonScope();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IRepository<AccountDto>>().To<Repository>();

            //kernel.Bind<IRepository<AccountDto>>().To<FakeRepository>();
            //kernel.Bind<IApplicationSettings>().To<ApplicationSettings>();
            //kernel.Bind<IRepository>().To<AccountBinaryRepository>().WithConstructorArgument("test.bin");
        }
    }
}
