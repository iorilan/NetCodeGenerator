

using YourProjectName.BusinessLogic.Helpers;
using YourProjectName.BusinessLogic.Models;
using YourProjectName.BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using YourProjectName.DataAccess;
using YourProjectName.Models;

namespace YourProjectName.Controllers
{
    [Authorize]
    public class YourTableNameController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
       
	    [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(YourTableNameObj model)
        {
            if (!ModelState.IsValid)
            {
                ShowValidationError();
                return View(model);
            }

            bool isSuccess = false;

            try
            {
                var service = new YourTableNameService();
                isSuccess = await service.AddOrUpdate(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error create YourTableName." + ex.Message);
                _log.Error(ex);
            }

            return ShowMessage(isSuccess, model);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Edit(int id)
        {
            var service = new YourTableNameService();
            var model = await service.GetById(id);
            return View("Edit", model);
        }


        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Edit(YourTableNameObj model)
        {
            if (!ModelState.IsValid)
            {
                ShowValidationError();
                return View(model);
            }

            bool isSuccess = false;

            try
            {
                var service = new YourTableNameService();
                isSuccess = await service.AddOrUpdate(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error Edit YourTableName." + ex.Message);
                _log.Error(ex);
            }

            return ShowMessage(isSuccess, model);
        }

        public async System.Threading.Tasks.Task<ActionResult> Detail(int id)
        {
            var service = new YourTableNameService();
            var result = await service.GetById(id);
            if (result != null) { return View("Detail", result); }
            else
            {
                return View("index", new YourTableNameObj());
            }
        }

        private ActionResult ShowMessage(bool isSuccess, YourTableNameObj model)
        {
            if (isSuccess)
            {
                ViewData[UIKeys.IsSuccess] = true;
                ViewData[UIKeys.Message] = "Operation successful.";
                return View("Index");
            }
            else
            {
                ViewData[UIKeys.IsError] = true;
                ViewData[UIKeys.Message] = "An error occured on server.";
                return View(model);
            }
        }

        private void ShowValidationError()
        {
            ViewData[UIKeys.IsError] = true;
            ViewData[UIKeys.Message] = "Please fix the validation errors";
        }

        #region DataTables - JSON 
        public ActionResult GetJsonData(int draw, int start, int length)
        {
            string search = Request.QueryString[DataTableQueryString.Searching];
            string sortColumn = "";
            string sortDirection = "asc";

            if (Request.QueryString[DataTableQueryString.OrderingColumn] != null)
            {
                sortColumn = Request.QueryString[DataTableQueryString.OrderingColumn].ToString();
                sortColumn = GetServiceSortingColumn(sortColumn);
            }
            if (Request.QueryString[DataTableQueryString.OrderingDir] != null)
            {
                sortDirection = Request.QueryString[DataTableQueryString.OrderingDir];
            }

            var dataTableData = new DataTableData
            {
                draw = draw
            };
            var recordsFiltered = 0;
            var service = new YourTableNameService();
            dataTableData.data = service.Search(out recordsFiltered, start, length, sortColumn, sortDirection, search);

            dataTableData.recordsFiltered = recordsFiltered;
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }


        public string GetServiceSortingColumn(string sortColumnNo)
        {
            var name = DataTableHelper.SoringColumnName<YourTableNameObj>(sortColumnNo);
            return name;
        }

        public class DataTableData
        {
            public int draw { get; set; }
            public int recordsFiltered { get; set; }
            public IList<YourTableNameObj> data { get; set; }
        }
        #endregion
    }
}