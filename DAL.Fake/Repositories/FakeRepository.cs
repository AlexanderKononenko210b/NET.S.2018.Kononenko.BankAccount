//using System;
//using System.Collections.Generic;
//using System.Linq;
//using DAL.Fake.Exceptions;
//using DAL.Interface.Interfaces;
//using DAL.Interface.Dto;
    
//namespace DAL.Fake.Repositories
//{
//    /// <summary>
//    /// Fake Account Repository
//    /// </summary>
//    public class FakeRepository : IRepository<AccountDto>
//    {
//        private List<AccountDto> list = new List<AccountDto>();

//        /// <summary>
//        /// Get instance type Account by number
//        /// </summary>
//        /// <param name="number">number of Account</param>
//        /// <returns>instance Account if exist in list</returns>
//        public AccountDto Get(string number)
//        {
//            if(number == null)
//                throw new ArgumentNullException($"Argument {nameof(number)} is null");

//            var equalityComparer = EqualityComparer<string>.Default;

//            return list.Find(item => equalityComparer.Equals(item.NumberOfAccount, number));
//        }

//        /// <summary>
//        /// Add model to list
//        /// </summary>
//        /// <param name="model">model</param>
//        public AccountDto Add(AccountDto model)
//        {
//            if (model == null)
//                throw new ArgumentNullException($"Argument {nameof(model)} is null");

//            if (this.Get(model.NumberOfAccount) != null)
//                throw new ExistInDataBaseException($"Account {nameof(model)} already exist in repository");

//            list.Add(model);

//            return model;
//        }

//        /// <summary>
//        /// Update Account in list
//        /// </summary>
//        /// <param name="model">model with update</param>
//        /// <returns>Updated model</returns>
//        public AccountDto Update(AccountDto model)
//        {
//            if (model == null)
//                throw new ArgumentNullException($"Argument {nameof(model)} is null");

//            AccountDto modelForUpdate = Get(model.NumberOfAccount);

//            if(modelForUpdate == null)
//                throw new ExistInDataBaseException($"Account {nameof(model)} does not exist in repository");

//            list.Remove(modelForUpdate);

//            list.Add(model);

//            return model;
//        }

//        /// <summary>
//        /// delete model from list
//        /// </summary>
//        /// <param name="model">model for delete</param>
//        /// <returns>instance that delete</returns>
//        public AccountDto Delete(AccountDto model)
//        {
//            if (model == null)
//                throw new ArgumentNullException($"Argument {nameof(model)} is null");

//            AccountDto modelForDelete = Get(model.NumberOfAccount);

//            if (modelForDelete == null)
//                throw new ExistInDataBaseException($"Account {nameof(model)} does not exist in repository");

//            list.Remove(modelForDelete);

//            return model;
//        }

//        /// <summary>
//        /// Block state for get all accounts in repository
//        /// </summary>
//        /// <returns></returns>
//        public IEnumerable<AccountDto> GetAll()
//        {
//            return list;
//        }

//        public AccountDto Get(int id)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
