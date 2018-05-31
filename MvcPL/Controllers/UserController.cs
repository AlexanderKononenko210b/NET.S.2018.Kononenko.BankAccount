using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Dto;
using BLL.Interface.Interfaces;
using MvcPL.Properties;
using DependencyResolver;
using MvcPL.Mapper;
using MvcPL.Models;
using Ninject;

namespace MvcPL.Controllers
{
    public class UserController : Controller
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        [HttpGet]
        public ActionResult GetAll()
        {
            return View();
        }
    }
}