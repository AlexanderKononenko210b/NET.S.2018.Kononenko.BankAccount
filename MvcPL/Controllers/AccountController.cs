using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BLL.Interface.Dto;
using BLL.Interface.Interfaces;
using DependencyResolver;
using MvcPL.Infrastructure.Validators;
using MvcPL.Mapper;
using MvcPL.Models;
using MvcPL.Properties;
using MvcPL.Infrastructure.Attributs;
using Ninject;

namespace MvcPL.Controllers
{
    public class AccountController : Controller
    {
        #region Fields

        private IAccountService accountService;

        private IUserService userService;

        #endregion

        #region Constructors

        public AccountController(IAccountService accountService, IUserService userService)
        {
            this.accountService = accountService;

            this.userService = userService;
        }

        #endregion

        #region Get operation with accounts

        /// <summary>
        /// Get All Accounts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAll()
        {
            var result = Mapper<IEnumerable<AccountViewDto>, IEnumerable<AccountViewModel>>
                .MapView(accountService.GetAll());

            return View(result);
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        [HttpGet]
        public ActionResult GetUserById(int? id)
        {
            if (id == null)
            {
                return View("Error", "", Resources.NullArgument);
            }

            try
            {
                var user = userService.Get(id.Value);

                var userForDisplay = Mapper<UserViewDto, UserViewModel>.MapView(user);

                return View(userForDisplay);
            }
            catch (Exception e)
            {
                return View("Error", e.Message);
            }
        }

        #endregion

        #region Create new account

        /// <summary>
        /// Get form for create new account
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Post form with information about new account
        /// </summary>
        /// <param name="model">model type CreateAccountViewModel</param>
        /// <returns>Action result</returns>
        [HttpPost]
        public ActionResult Create(CreateAccountViewModel model)
        {
            if (model == null)
                return View("Error", "", Resources.NullArgument);

            if (!ModelState.IsValid)
            {
                return View("Create");
            }

            try
            {
                var user = userService.Create(model.FirstName, model.LastName, model.Passport, model.Email);

                var account = accountService.OpenAccount(model.AccountType, user.Id);

                return View("CreateSuccess", account);

            }
            catch (Exception e)
            {
                return View("Error", "", e.Message);
            }
        }

        #endregion

        #region Close account

        /// <summary>
        /// Get form for edit account
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Close(int? id)
        {
            if (id == null)
                return View("Error", "", Resources.NullArgument);

            try
            {
                var account = accountService.Get(id.Value);

                if (account == null)
                {
                    return View("Error", "", Resources.AccountNotFount);
                }

                var accountViewModel = Mapper<AccountViewDto, AccountViewModel>.MapView(account);

                return View(accountViewModel);
            }
            catch (Exception e)
            {
                return View("Error", "", e.Message);
            }
        }

        /// <summary>
        /// Get form for edit account
        /// </summary>
        /// <returns></returns>
        [HttpParamAction]
        [HttpPost]
        public ActionResult Close(AccountViewModel model)
        {
            if (model == null)
                return View("Error", "", Resources.NullArgument);

            model.IsClosed = true;

            try
            {
                var accountViewDto = Mapper<AccountViewModel, AccountViewDto>.MapView(model);

                var resultClose = accountService.Close(accountViewDto);

                if (!resultClose)
                {
                    return View("Error", "", Resources.CloseAccountInvalid);
                }

                ViewBag.Number = accountViewDto.NumberOfAccount;

                return View("CloseSuccess");
            }
            catch (Exception e)
            {
                return View("Error", "", e.Message);
            }
        }

        [HttpParamAction]
        [HttpPost]
        public ActionResult Cansel(AccountViewModel model)
        {
            return RedirectToAction("GetAll");
        }

        #endregion

        #region Deposit operation

        /// <summary>
        /// Get form for edit account
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Deposit()
        {
            var userId = 86;//Get userId from Request

            try
            {
                var numbers = accountService.GetAllNumbers(userId);

                if (numbers == null || numbers.Any() == false)
                    return View("Info", "", Resources.NotExistAccounts);

                ViewData["Numbers"] = new SelectList(numbers);

                return View("Deposit");
            }
            catch (Exception e)
            {
                return View("Error", "", e.Message);
            }
        }

        /// <summary>
        /// Get form for edit account
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Deposit(DepositAndWithDrawViewModel model)
        {
            try
            {
                Check.NotNull(model);

                Check.CheckString(model.AccountNumber);

                Check.CheckString(model.Value);

                decimal value;

                if (Decimal.TryParse(model.Value, out value))
                {
                    var resultDepositDto = accountService.DepositAccount(model.AccountNumber, value);

                    var resultDeposit = Mapper<AccountViewDto, AccountViewModel>.MapView(resultDepositDto);

                    ViewData["Operation"] = "Deposit";

                    ViewData["Deposit"] = model.Value;

                    return View("OperationSuccess", resultDeposit);
                }

                var userId = 86;//Get userId from Request

                var numbers = accountService.GetAllNumbers(userId);

                if (numbers == null || numbers.Any() == false)
                    return View("Info", "", Resources.NotExistAccounts);

                ViewData["Numbers"] = new SelectList(numbers);

                ViewData["Message"] = Resources.DepositValueNotValid;

                return View("Deposit");
            }
            catch (Exception e)
            {
                return View("Error", "", e.Message);
            }
        }

        #endregion

        #region WithDraw operation

        /// <summary>
        /// Get form for withdraw operation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WithDraw()
        {
            var userId = 86;//Get userId from Request

            try
            {
                var numbers = accountService.GetAllNumbers(userId);

                if (numbers == null || numbers.Any() == false)
                    return View("Info", "", Resources.NotExistAccounts);

                ViewData["Numbers"] = new SelectList(numbers);

                return View("WithDraw");
            }
            catch (Exception e)
            {
                return View("Error", "", e.Message);
            }
        }

        /// <summary>
        /// Post form with information about account and value withdraw
        /// </summary>
        /// <param name="model">model type DepositAndWithDrawViewModel</param>
        [HttpPost]
        public ActionResult WithDraw(DepositAndWithDrawViewModel model)
        {
            try
            {
                Check.NotNull(model);

                Check.CheckString(model.AccountNumber);

                Check.CheckString(model.Value);

                decimal value;

                if (Decimal.TryParse(model.Value, out value))
                {
                    var resultWithDrawDto = accountService.WithDrawAccount(model.AccountNumber, value);

                    var resultWithDraw = Mapper<AccountViewDto, AccountViewModel>.MapView(resultWithDrawDto);

                    ViewData["Operation"] = "WithDraw";

                    ViewData["Deposit"] = model.Value;

                    return View("OperationSuccess", resultWithDraw);
                }

                var userId = 86;//Get userId from Request

                var numbers = accountService.GetAllNumbers(userId);

                if (numbers == null || numbers.Any() == false)
                    return View("Info", "", Resources.NotExistAccounts);

                ViewData["Numbers"] = new SelectList(numbers);

                ViewData["Message"] = Resources.WithDrawValueNotValid;

                return View("WithDraw");
            }
            catch (Exception e)
            {
                return View("Error", "", e.Message);
            }
        }

        #endregion

        #region Transfer operation

        /// <summary>
        /// Get form for withdraw operation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Transfer()
        {
            var userId = 86;//Get userId from Request

            try
            {
                var numbers = accountService.GetAllNumbers(userId);

                if (numbers == null || numbers.Any() == false)
                    return View("Info", "", Resources.NotExistAccounts);

                ViewData["Numbers"] = new SelectList(numbers);

                return View("Transfer");
            }
            catch (Exception e)
            {
                return View("Error", "", e.Message);
            }
        }

        /// <summary>
        /// Post form with information about accounts and value transfer
        /// </summary>
        /// <param name="model">model type TransferViewModel</param>
        [HttpPost]
        public ActionResult Transfer(TransferViewModel model)
        {
            try
            {

                Check.NotNull(model);

                Check.CheckString(model.FirstNumber);

                Check.CheckString(model.SecondNumber);

                Check.CheckString(model.Value);

                if (model.FirstNumber.Equals(model.SecondNumber, StringComparison.CurrentCulture))
                {
                    var userId = 86;//Get userId from Request

                    var numbers = accountService.GetAllNumbers(userId);

                    if (numbers == null || numbers.Any() == false)
                        return View("Info", "", Resources.NotExistAccounts);

                    ViewData["Numbers"] = new SelectList(numbers);

                    ViewData["Message"] = Resources.DublicatedNumberAccount;

                    return View("Transfer");
                }

                decimal value;

                if (Decimal.TryParse(model.Value, out value))
                {
                    var resultTransferDto = accountService.Transfer(model.FirstNumber, model.SecondNumber, value);

                    var resultTransfer = (Mapper<AccountViewDto, AccountViewModel>.MapView(resultTransferDto.Item1),
                        Mapper<AccountViewDto, AccountViewModel>.MapView(resultTransferDto.Item2));

                    ViewData["Transfer"] = model.Value;

                    return View("TransferSuccess", resultTransfer);
                }

                return View("Error", Resources.WithDrawValueNotValid);
            }
            catch (Exception e)
            {
                return View("Error", "", e.Message);
            }
        }

        #endregion

    }
}