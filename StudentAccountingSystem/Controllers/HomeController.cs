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
        public async System.Threading.Tasks.Task<ActionResult> GetStudents(string Id, string Name, string LastName, string Age, string StudyDate, string RegisteredDate)
        {
            if(Id!=""||Name!=""||LastName!=""||Age!=""||StudyDate!=""||RegisteredDate!="")
            {
               var studentlistbyfilter = await _studentProvider.GetStudentsAsyncByFilter(Id, Name, LastName, Age, StudyDate, RegisteredDate);
               var jsonbyfilter = JsonConvert.SerializeObject(studentlistbyfilter);
               return Content(jsonbyfilter, "application/json");
            }
            var studentlist = await _studentProvider.GetStudentsAsync();
            var json = JsonConvert.SerializeObject(studentlist);
            return Content(json, "application/json");
            //return Json(studentlist, JsonRequestBehavior.AllowGet);          
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