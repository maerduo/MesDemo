﻿using System;
using System.Linq;
using System.Linq.Expressions;

using Mes.Core;
using Mes.Demo.Dtos.TestLog;
using Mes.Demo.Models.Hr;
using Mes.Demo.Models.TestLog;
using Mes.Utility.Data;


namespace Mes.Demo.Contracts.TestLog
{
    public partial interface IHrContract : IDependency
    {
        /// <summary>
        /// 获取产线 信息查询数据集
        /// </summary>
        IQueryable<SwipeCard> SwipeCards { get; }

        /// <summary>
        /// 检查产线信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的产线信息编号</param>
        /// <returns>产线信息是否存在</returns>
        bool CheckSwipeCardExists(Expression<Func<SwipeCard, bool>> predicate, int id = 0);

        /// <summary>
        /// 添加产线信息
        /// </summary>
        /// <param name="dtos">要添加的产线信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddSwipeCards(params SwipeCardDto[] dtos);

        /// <summary>
        /// 更新产线信息
        /// </summary>
        /// <param name="dtos">包含更新信息的产线DTO信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult EditSwipeCards(params SwipeCardDto[] dtos);

        /// <summary>
        /// 删除产线信息
        /// </summary>
        /// <param name="ids">要删除的产线信息编号</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteSwipeCards(params int[] ids);
        
    }
}
