using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Dto;
using BLL.Interface.Interfaces;
using DependencyResolver;
using MvcPL.Mapper;
using MvcPL.Models;
using Ninject;

namespace MvcPL.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            IKernel resolver = new StandardKernel();
            resolver.ConfigurateResolverWeb();

            IAccountService accountService = resolver.Get<IAccountService>();

            var result = Mapper<IEnumerable<AccountViewDto>, IEnumerable<AccountViewModel>>
                .MapView(accountService.GetAll());

            return View(result);
        }
    }
}