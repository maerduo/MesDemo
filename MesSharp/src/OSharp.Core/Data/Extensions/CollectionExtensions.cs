﻿
using System;
using System.Linq;
using System.Linq.Expressions;

using Mes.Utility;
using Mes.Utility.Data;
using Mes.Utility.Extensions;
using Mes.Utility.Filter;


namespace Mes.Core.Data.Extensions
{
    /// <summary>
    /// 集合扩展辅助操作类
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合 中查询指定分页条件的子数据集
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="pageCondition">分页查询条件</param>
        /// <param name="total">输出符合条件的总记录数</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Where<TEntity, TKey>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            PageCondition pageCondition,
            out int total) where TEntity : EntityBase<TKey>
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");
            pageCondition.CheckNotNull("pageCondition");

            return source.Where<TEntity, TKey>(predicate, pageCondition.PageIndex, pageCondition.PageSize, out total, pageCondition.SortConditions);
        }


        /// <summary>
        /// 从指定<see cref="IQueryable{T}"/>集合 中查询指定分页条件的子数据集
        /// </summary>
        /// <typeparam name="TEntity">动态实体类型</typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="total">输出符合条件的总记录数</param>
        /// <param name="sortConditions">排序条件集合</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Where<TEntity, TKey>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate,
            int pageIndex,
            int pageSize,
            out int total,
            SortCondition[] sortConditions = null) where TEntity : EntityBase<TKey>
        {
            source.CheckNotNull("source");
            predicate.CheckNotNull("predicate");
            pageIndex.CheckGreaterThan("pageIndex", 0);
            pageSize.CheckGreaterThan("pageSize", 0);

            if ((predicate.Type.FullName.Contains("Mes.Demo.Models.TestLog.Cpk")) &&(predicate.Body.ToString()=="True"))
            {
                total = 10000000;
            }
            else
            total = source.Count(predicate);
            source = source.Where(predicate);
            if (sortConditions == null || sortConditions.Length == 0)
            {
                source = source.OrderBy(m => m.Id);
            }
            else
            {
                int count = 0;
                IOrderedQueryable<TEntity> orderSource = null;
                foreach (SortCondition sortCondition in sortConditions)
                {
                    orderSource = count == 0
                        ? CollectionPropertySorter<TEntity>.OrderBy(source, sortCondition.SortField, sortCondition.ListSortDirection)
                        : CollectionPropertySorter<TEntity>.ThenBy(orderSource, sortCondition.SortField, sortCondition.ListSortDirection);
                    count++;
                }
                source = orderSource;
            }
            return source != null
                ? source.Skip((pageIndex - 1) * pageSize).Take(pageSize)
                : Enumerable.Empty<TEntity>().AsQueryable();
        }
    }
}