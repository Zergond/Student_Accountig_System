using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using BLL.Models;
using System.Net;
using Newtonsoft.Json;

namespace StudentAccountingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentProvider _studentProvider;

        public HomeController(IStudentProvider studentProvider)
        {
            _studentProvider = studentProvider;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]        
        public async System.Threading.Tasks.Task<ActionResult> GetStudents(string pageIndex,string pageSize)
        {
               var studentdata = await _studentProvider.GetStudentsAsyncByFilter(pageIndex,pageSize);
               int count = _studentProvider.GetStudentsCount();
               var studentjson = JsonConvert.SerializeObject(new { studentdata, count });
               return Content(studentjson, "application/json");      
        }
        [Authorize]
        public async System.Threading.Tasks.Task<ActionResult> Edit(string Id)
        {
            return View();
        }
        [Authorize]
        public async System.Threading.Tasks.Task<ActionResult> Delete()
        {

            return View(await _studentProvider.GetStudentsAsync());
        }

    }
}