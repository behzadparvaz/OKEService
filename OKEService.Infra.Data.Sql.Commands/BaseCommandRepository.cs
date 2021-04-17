﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OKEService.Core.Domain.Data;
using OKEService.Core.Domain.Entities;
using OKEService.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace OKEService.Infra.Data.Sql.Commands
{
    public class BaseCommandRepository<TEntity, TDbContext> : ICommandRepository<TEntity>, IUnitOfWork
        where TEntity : AggregateRoot
        where TDbContext : BaseCommandDbContext
    {
        protected readonly TDbContext _dbContext;

        public BaseCommandRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        void ICommandRepository<TEntity>.Delete(long id)
        {
            var entity = _dbContext.Set<TEntity>().Find(id);
            _dbContext.Set<TEntity>().Remove(entity);
        }

        void ICommandRepository<TEntity>.Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        void ICommandRepository<TEntity>.DeleteGraph(long id)
        {
            var graphPath = _dbContext.GetIncludePaths(typeof(TEntity));
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
            foreach (var item in graphPath)
            {
                query = query.Include(item);
            }
            var entity = query.FirstOrDefault(c => c.Id == id);
            if (entity?.Id > 0)
                _dbContext.Set<TEntity>().Remove(entity);
        }

        TEntity ICommandRepository<TEntity>.Get(long id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public TEntity Get(BusinessId businessId)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(c => c.BusinessId == businessId);
        }

        TEntity ICommandRepository<TEntity>.GetGraph(long id)
        {
            var graphPath = _dbContext.GetIncludePaths(typeof(TEntity));
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
            var temp = graphPath.ToList();
            foreach (var item in graphPath)
            {
                query = query.Include(item);
            }
            return query.FirstOrDefault(c => c.Id == id);
        }

        void ICommandRepository<TEntity>.Insert(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        bool ICommandRepository<TEntity>.Exists(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.Set<TEntity>().Any(expression);
        }

        public TEntity GetGraph(BusinessId businessId)
        {
            var graphPath = _dbContext.GetIncludePaths(typeof(TEntity));
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
            var temp = graphPath.ToList();
            foreach (var item in graphPath)
            {
                query = query.Include(item);
            }
            return query.FirstOrDefault(c => c.BusinessId == businessId);
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void BeginTransaction()
        {
            _dbContext.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _dbContext.RollbackTransaction();
        }
    }
}
