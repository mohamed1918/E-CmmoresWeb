﻿using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    abstract class BaseSpeceification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpeceification(Expression<Func<TEntity,bool>>? criteria) 
        {
            Criteria = criteria;
        }

        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new List<Expression<Func<TEntity, object>>>();

        protected void AddInclude(Expression<Func<TEntity, object>> IncludeExpression)
        {
            IncludeExpressions.Add(IncludeExpression);
        }
        #endregion

        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        
        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExp) 
        {
            OrderBy = orderByExp;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExp)
        {
            OrderByDescending = orderByDescExp;
        }
        #endregion

        #region Pagination
        public int Take { get; private set; }

        public int Skip {  get; private set; }

        public bool IsPaginated { get; set; }

        protected void ApplyPagination(int PageSize , int PageIndex)
        {
            Take = PageSize;
            Skip = (PageSize-1) * PageSize;
            IsPaginated = true;
        }

        #endregion
    }
}
