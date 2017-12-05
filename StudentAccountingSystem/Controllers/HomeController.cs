using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;

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
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            return View(await _studentProvider.GetStudentsAsync());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}