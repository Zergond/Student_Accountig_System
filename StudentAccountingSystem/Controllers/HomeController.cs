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

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            @ViewBag.leftpaneltabtext = "Student Table";
            return View();
        }
        [Authorize(Roles = "User")]
        public ActionResult UserProfileSettings()
        {
            @ViewBag.leftpaneltabtext = "Student Profile Settings";
            return View();
        }
        [Authorize(Roles ="User")]
        public async System.Threading.Tasks.Task<ActionResult> GetStudentProfile()
        {
            var student = await _studentProvider.GetCurrentStudentAsync();
            var studentjson = JsonConvert.SerializeObject(student);
            return Content(studentjson, "application/json");
        }
        [Authorize(Roles = "Admin")]
        public async System.Threading.Tasks.Task<ActionResult> GetStudents(string pageIndex,string pageSize)
        {
               var studentdata = await _studentProvider.GetStudentsAsyncByFilter(pageIndex,pageSize);
               int count = _studentProvider.GetStudentsCount();
               var studentjson = JsonConvert.SerializeObject(new { studentdata, count });
               return Content(studentjson, "application/json");      
        }

    }
}