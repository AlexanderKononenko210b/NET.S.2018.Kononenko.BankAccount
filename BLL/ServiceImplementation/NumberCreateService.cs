using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Interfaces;

namespace BLL.ServiceImplementation
{
    /// <summary>
    /// Service for create unique number account
    /// </summary>
    public class NumberCreateService : IAccountNumberCreateService
    {
        #region Fields

        /// <summary>
        /// Account number in the bank's accounting plan
        /// </summary>
        private const string NUMBER_BANKING_ACCOUNTING = "40512";

        /// <summary>
        /// Check digit of the account
        /// </summary>
        private const int SPECIAL_NUMBER = 1;

        /// <summary>
        /// Number department in bank
        /// </summary>
        private const string NUMBER_DEPARTMENT_BANK = "0079";

        /// <summary>
        /// Count digit in number describe number account
        /// </summary>
        private const int NUMBER_DIGIT_COUNT_BANK_ACCOUNT_IN_DEPARTMENT = 10;

        /// <summary>
        /// Counter number account
        /// </summary>
        private int countAccountInDepartment;

        /// <summary>
        /// Field save instance
        /// </summary>
        private static volatile NumberCreateService instance;

        /// <summary>
        /// Object for sinhronization access to instance
        /// </summary>
        private static readonly Object padlock = new object();

        #endregion

        #region Public Api

        /// <summary>
        /// Get instance NumberCreateService
        /// </summary>
        public static NumberCreateService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new NumberCreateService();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Get and Set number account in department
        /// </summary>
        public int CountAccountInDepartment
        {
            get => countAccountInDepartment;

            private set => countAccountInDepartment = value;
        }

        /// <summary>
        /// Get new unique number for account
        /// </summary>
        /// <returns></returns>
        public string GetNumberAccount()
        {
            var result = $"{NUMBER_BANKING_ACCOUNTING}{SPECIAL_NUMBER}" +
                         $"{NUMBER_DEPARTMENT_BANK}{GetNumberCountAccountInDepartment(++this.CountAccountInDepartment)}";

            return result;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Helper method for generate CountBankAccountInFaffliate in string format
        /// </summary>
        /// <param name="inputNumber"></param>
        /// <returns></returns>
        private string GetNumberCountAccountInDepartment(int inputNumber)
        {
            int number = 0, helperNumber = inputNumber;

            while (helperNumber > 0)
            {
                number++;

                helperNumber /= 10;
            }

            return $"{new String('0', NUMBER_DIGIT_COUNT_BANK_ACCOUNT_IN_DEPARTMENT - number)}{inputNumber}";
        }

        #endregion
    }
}
