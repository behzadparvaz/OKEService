﻿using OKEService.Core.Domain.Entities;
using OKEService.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace OKEService.Core.Domain.Data
{
    public interface ICommandRepository<TEntity> : IUnitOfWork
        where TEntity : AggregateRoot
    {
        void Delete(long id);
        void DeleteGraph(long id);
        void Delete(TEntity entity);
        void Insert(TEntity entity);
        TEntity Get(long id);
        TEntity Get(BusinessId businessId);
        TEntity GetGraph(long id);
        TEntity GetGraph(BusinessId businessId);
        bool Exists(Expression<Func<TEntity, bool>> expression);
    }
}
