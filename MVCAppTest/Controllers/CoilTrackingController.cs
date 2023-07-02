using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCAppTest.Models;

namespace MVCAppTest.Controllers
{
    public class CoilTrackingController : Controller
    {
        // GET: CoilTracking
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getSlabGrade()
        {
            CoilTracking objCoilTracking = new CoilTracking();
            List<SelectListItem> grades = new List<SelectListItem>();
            grades.Add(new SelectListItem { Text = "T89256A", Value = "1" });
            grades.Add(new SelectListItem { Text = "T72486B", Value = "2" });
            grades.Add(new SelectListItem { Text = "F670268U", Value = "3" });
            grades.Add(new SelectListItem { Text = "F670181Z", Value = "4" }); 
            grades.Add(new SelectListItem { Text = "T58926A", Value = "5" }); 
            objCoilTracking.SlabGrade = grades;
            return View(objCoilTracking);
        }

        [HttpPost]
        public ActionResult DisplaySlabGrades(string[] grades)
        {
            return Json(new { success = grades });
        }

    }
}