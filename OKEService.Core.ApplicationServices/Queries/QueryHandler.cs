﻿using OKEService.Core.Domain.Common;
using OKEService.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OKEService.Core.ApplicationServices.Queries
{
    public interface IQueryHandler<TQuery, TData>
        where TQuery : class, IQuery<TData>
    {
        Task<QueryResult<TData>> Handle(TQuery request);
    }

    public abstract class QueryHandler<TQuery, TData> : IQueryHandler<TQuery, TData>
        where TQuery : class, IQuery<TData>
    {
        protected readonly OKEServiceServices _hamoonApplicationContext;
        protected readonly QueryResult<TData> result = new QueryResult<TData>();

        protected virtual Task<QueryResult<TData>> ResultAsync(TData data, ApplicationServiceStatus status)
        {
            result._data = data;
            result.Status = status;
            return Task.FromResult(result);
        }

        protected virtual QueryResult<TData> Result(TData data, ApplicationServiceStatus status)
        {
            result._data = data;
            result.Status = status;
            return result;
        }


        protected virtual Task<QueryResult<TData>> ResultAsync(TData data)
        {
            var status = data != null ? ApplicationServiceStatus.Ok : ApplicationServiceStatus.NotFound;
            return ResultAsync(data, status);
        }

        protected virtual QueryResult<TData> Result(TData data)
        {
            var status = data != null ? ApplicationServiceStatus.Ok : ApplicationServiceStatus.NotFound;
            return Result(data, status);
        }

        public QueryHandler(OKEServiceServices hamoonApplicationContext)
        {
            _hamoonApplicationContext = hamoonApplicationContext;
        }

        public abstract Task<QueryResult<TData>> Handle(TQuery request);
    }
}
