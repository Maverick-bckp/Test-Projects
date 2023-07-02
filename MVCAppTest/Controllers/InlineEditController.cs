using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCAppTest.Models;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;

namespace MVCAppTest.Controllers
{
    [AllowAnonymous]
    public class InlineEditController : Controller
    {
        static OracleConnection conTRISAPP = null;
        // GET: InlineEdit
        public ActionResult Index()
        {
            List<UnitDetailsStoreMapping> ud = new List<UnitDetailsStoreMapping>();
            using (conTRISAPP = new OracleConnection(ConfigurationManager.AppSettings["connTRISAPP"].ToString()))
            {
                DataTable dt = new DataTable();
                OracleCommand cmd = conTRISAPP.CreateCommand();
                cmd.CommandText = "select * from unitdetails_store_mapping";
                cmd.CommandType = CommandType.Text;
                if (conTRISAPP.State != ConnectionState.Open)
                {
                    conTRISAPP.Open();
                }
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                oda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        UnitDetailsStoreMapping udlog = new UnitDetailsStoreMapping();
                        udlog.StaffNo = dr["StaffNo"].ToString(); 
                        udlog.Product = dr["Product"].ToString(); 
                        udlog.StorageLocation = dr["Storage_Location"].ToString(); 
                        ud.Add(udlog);
                    }
                }
            }
            return View(ud.ToList());
        }

        [HttpPost]
        public ActionResult saveuser(string id, string propertyName, string value)
        {
            var status = false;
            var message = "";

            ////Update data to database 
            //using (MyDatabaseEntities dc = new MyDatabaseEntities())
            //{
            //    var user = dc.SiteUsers.Find(id);
            //    if (user != null)
            //    {
            //        dc.Entry(user).Property(propertyName).CurrentValue = value;
            //        dc.SaveChanges();
            //        status = true;
            //    }
            //    else
            //    {
            //        message = "Error!";
            //    }
            //}

            var response = new { value = value, status = status, message = message };
            JObject o = JObject.FromObject(response);
            return Content(o.ToString());
        }

    }
}