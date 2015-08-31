﻿// <autogenerated>
//   This file was generated by T4 code generator Dto.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

using Mes.Demo.Contracts.TestLog;
using Mes.Demo.Dtos.TestLog;
using Mes.Demo.Models.TestLog;
using Mes.Utility.Data;
using Mes.Utility.Extensions;
using Mes.Utility.Filter;
using Mes.Web.Mvc.Binders;
using Mes.Web.Mvc.Security;
using Mes.Web.UI;

using Util;


namespace Mes.Demo.Web.Areas.Admin.Controllers
{
    
    public class CpkController : AdminBaseController
    {
        public ITestLogContract TestLogContract { get; set; }



        [AjaxOnly]
        public ActionResult GridData(int? id)
        {
            int total;
            GridRequest request = new GridRequest(Request);
            var datas = GetQueryData<Cpk, int>(TestLogContract.Cpks, out total, request).Select(m => new
            {
                m.Id,
                m.PlatForm,
                m.ProjectName,
                m.Opcode,
                m.Ip,
                m.Sn,
                m.TestTime,
                m.Result,
                m.Wgsn,
                m.Tch,
                m.Pcl,
                m.TestVal,
                m.MinVal,
                m.MaxVal,
                m.TestDate,
                m.LogFileName,
                m.ZipFileName,
                
            });
            
            return Json(new GridData<object>(datas, total), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Add([ModelBinder(typeof(JsonBinder<CpkDto>))] ICollection<CpkDto> dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = TestLogContract.AddCpks(dtos.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost]
        [AjaxOnly]
        public ActionResult Edit([ModelBinder(typeof(JsonBinder<CpkDto>))] ICollection<CpkDto> dtos)
        {
            dtos.CheckNotNull("dtos");
            OperationResult result = TestLogContract.EditCpks(dtos.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Delete([ModelBinder(typeof(JsonBinder<int>))] ICollection<int> ids)
        {
            ids.CheckNotNull("ids");
            OperationResult result = TestLogContract.DeleteCpks(ids.ToArray());
            return Json(result.ToAjaxResult(), JsonRequestBehavior.AllowGet);
        }
        
        public override void CreateExcel()
        {
            GridRequest request = new GridRequest(Request);
            var filterGroup = request.FilterGroup;
            Expression<Func<Cpk, bool>> predicate = FilterHelper.GetExpression<Cpk>(filterGroup);
            var cpks = TestLogContract.Cpks.Where(predicate).Select(m => new
            {
                m.Id,
                m.PlatForm,
                m.ProjectName,
                m.Opcode,
                m.Ip,
                m.Sn,
                m.TestTime,
                m.Result,
                m.Wgsn,
                m.Tch,
                m.Pcl,
                m.TestVal,
                m.MinVal,
                m.MaxVal,
                m.TestDate,
                m.LogFileName,
                m.ZipFileName
            });
            if (cpks.Count() > 300000)
            {
                Response.Write("数量超过300000，不能下载");
                return;
            }
            Excel(cpks, "Cpk" + DateTime.Now.ToString("yyyyMMddhhmmss"));
        }

        public void DownLoadZip(string zipFileName)
        {
            string filePath = Server.MapPath("/DownLoad/") + zipFileName;
            DownLoad(filePath, zipFileName );
        }
        public override ActionResult Index()
        {
            ViewBag.ToolbarItem = "export";
            return View();
        }
    }
}
