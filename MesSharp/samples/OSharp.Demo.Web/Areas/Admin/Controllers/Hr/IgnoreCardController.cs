﻿// <autogenerated>
//   This file was generated by T4 code generator Dto.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

using Mes.Demo.Contracts.TestLog;
using Mes.Demo.Models.Hr;
using Mes.Utility.Data;
using Mes.Utility.Extensions;
using Mes.Utility.Filter;
using Mes.Web.Mvc.Binders;
using Mes.Web.Mvc.Security;
using Mes.Web.UI;


namespace Mes.Demo.Web.Areas.Admin.Controllers
{
    public class IgnoreCardController : AdminBaseController
    {
        public IHrContract HrContract { get; set; }



        [AjaxOnly]
        public ActionResult GridData(int? id)
        {
            int total;
            GridRequest request = new GridRequest(Request);
            var datas = GetQueryData<IgnoreCard, int>(HrContract.IgnoreCards, out total, request).Select(m => new
            {
                m.Id,
                m.EmpNo
               
            });
            return Json(new GridData<object>(datas, total), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Add([ModelBinder(typeof(JsonBinder<IgnoreCardDto>))] ICollection<IgnoreCardDto> dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = HrContract.AddIgnoreCards(dtos.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Edit([ModelBinder(typeof(JsonBinder<IgnoreCardDto>))] ICollection<IgnoreCardDto> dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = HrContract.EditIgnoreCards(dtos.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Delete([ModelBinder(typeof(JsonBinder<int>))] ICollection<int> ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result = HrContract.DeleteIgnoreCards(ids.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        public override void CreateExcel()
        {
            GridRequest request = new GridRequest(Request);
            var filterGroup = request.FilterGroup;
            Expression<Func<IgnoreCard, bool>> predicate = FilterHelper.GetExpression<IgnoreCard>(filterGroup);
            var ignoreCards = HrContract.IgnoreCards.Where(predicate).Select(m => new
            {
                m.EmpNo
            });

            Excel(ignoreCards, "IgnoreCards" + DateTime.Now.ToString("yyyyMMddhhmmss"));
        }
    }
}