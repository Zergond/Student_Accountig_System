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

        //[HttpGet]
        //[ActionName("GetStudents")]
        //public IList<>

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]        
        public async System.Threading.Tasks.Task<JsonResult> GetStudents()
        {
            var studentlist = await _studentProvider.GetStudentsAsync();
            return Json(studentlist, JsonRequestBehavior.AllowGet);
            //return View(await _studentProvider.GetStudentsAsync());
        }
        [Authorize]
        public async System.Threading.Tasks.Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             var student = await _studentProvider.GetByIdAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        [Authorize]
        public async System.Threading.Tasks.Task<ActionResult> Delete()
        {

            return View(await _studentProvider.GetStudentsAsync());
        }

    }
}