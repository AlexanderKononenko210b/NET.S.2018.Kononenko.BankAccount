﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DAL.Exceptions;
using DAL.Interface.DbModels;
using DAL.Interface.Interfaces;
using DAL.Mappers;
using DAL.Validators;

namespace DAL.Repositories
{
    /// <summary>
    /// Base class for all repositories. 
    /// It is implements logic CRUD operation for all repository
    /// </summary>
    public class Repository<T,P> : IRepository<T,P>, IDisposable
        where T : Entity
        where P : Entity
    {
        #region Fields

        private bool isDisposed;

        private readonly DbContext context;

        private readonly IDbSet<P> dbSet;

        #endregion

        #region Constructors

        public Repository(DbContext context)
        {
            this.context = context;

            this.dbSet = context.Set<P>();
        }

        #endregion

        #region Public Api

        /// <summary>
        /// Add new instance type T in database
        /// </summary>
        /// <param name="model">instance type T</param>
        /// <returns>instance type T if operation was succesfully</returns>
        public virtual T Add(T model)
        {
            Check.NotNull(model);

            if (isDisposed)
                throw new ObjectDisposedException($"Context {nameof(context)} is disposed");

            var modelForAdd = Mapper<T, P>.Map(model);

            var resultAdd = dbSet.Add(modelForAdd);

            context.SaveChanges();

            var resultDto = Mapper<P, T>.Map(resultAdd);

            return resultDto;
        }

        /// <summary>
        /// Delete instance type T from database
        /// </summary>
        /// <param name="model">instance type T</param>
        /// <returns>instance type T if operation was succesfully</returns>
        public virtual T Delete(T model)
        {
            Check.NotNull(model);

            if (isDisposed)
                throw new ObjectDisposedException($"Context {nameof(context)} is disposed");

            var modelForDelete = Mapper<T, P>.Map(model);

            if (!dbSet.Any(item => item.Id == modelForDelete.Id))
                throw new ExistInDatabaseException($"Instance {model} doesn`t exist in database");

            var resultDelete = dbSet.Remove(modelForDelete);

            context.SaveChanges();

            var resultDto = Mapper<P, T>.Map(resultDelete);

            return resultDto;
        }

        /// <summary>
        /// Get instance by id
        /// </summary>
        /// <param name="id">identificator instance</param>
        /// <returns>instance type T</returns>
        public virtual T Get(int id)
        {
            if(id <= 0)
                throw new ArgumentOutOfRangeException($"Argument {id} have to be more than zero");

            if (isDisposed)
                throw new ObjectDisposedException($"Context {nameof(context)} is disposed");

            var resultGet = dbSet.SingleOrDefault(item => item.Id == id);

            var resultDto = Mapper<P, T>.Map(resultGet);

            return resultDto;
        }

        /// <summary>
        /// Update instance
        /// </summary>
        /// <param name="model">instance for update</param>
        /// <returns>update instance type T if operation succesfully</returns>
        public virtual T Update(T model)
        {
            Check.NotNull(model);

            if (isDisposed)
                throw new ObjectDisposedException($"Context {nameof(context)} is disposed");

            var modelDbModel = dbSet.SingleOrDefault(item => item.Id == model.Id);

            if(modelDbModel == null)
                throw new ExistInDatabaseException($"Entity with id : {model.Id} is absent in database");

            modelDbModel = Mapper<T, P>.MapToSelf(modelDbModel, model);

            context.Entry(modelDbModel).State = EntityState.Modified;

            context.SaveChanges();

            return model;
        }

        #endregion

        #region Disposable

        /// <summary>
        /// If unmanage resources are not release (isDisposed = false)
        /// set isDisposed in true and call method Dispose with false parameter
        /// </summary>
        ~Repository()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                Dispose(false);
            }
        }

        /// <summary>
        /// Determine manage unmanage resources
        /// if isDisposed = false set isDisposed in true and call method 
        /// Dispose with true parameter and not call finalizer
        /// </summary>
        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Virtual method for free unmanage resources
        /// </summary>
        /// <param name="disposing">true - method call in determine miner, false - undetermine miner</param>
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }

            context.Dispose();

            isDisposed = true;
        }

        #endregion
    }
}
