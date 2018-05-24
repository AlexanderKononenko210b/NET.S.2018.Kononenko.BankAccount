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

            var accounts = accountService.GetAll();

            var result = new List<AccountViewModel>();

            foreach (var item in accounts)
            {
                result.Add(Mapper<AccountViewDto, AccountViewModel>.MapView(item));
            }

            return View(result);
        }
    }
}