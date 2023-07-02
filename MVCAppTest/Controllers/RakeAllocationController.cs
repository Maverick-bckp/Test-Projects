using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCAppTest.Repositories;
using MVCAppTest.Models;

namespace MVCAppTest.Controllers
{
    public class RakeAllocationController : Controller
    {
        // GET: RakeAllocation
        RakeAllocationLogRepository RlogRepo = null;
        public RakeAllocationController()
        {
            RlogRepo = new RakeAllocationLogRepository();
            ModelState.Clear();
        }
        public ActionResult Index()
        {
            //return View(RlogRepo.GetAllData());
            return View();
        }

        public ActionResult getAllRakeAllocationLog()
        {
            //return View(RlogRepo.GetAllData());
            return View();
        }

        [HttpPost]
        public ActionResult editRakeAllocationLog(RakeAllocationLog rlog)
        {
            try
            {
                RlogRepo.UpdateRakeAllocationLog(rlog);
                return RedirectToAction("getAllRakeAllocationLog");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Employee/EditEmpDetails/5    
        public ActionResult editRakeAllocationLog(string linenum, string takeovernum)
        { 
            return View(RlogRepo.GetAllData().Find(x => x.LineNum == linenum && x.TakeOverNum == takeovernum));

        }
    }
}