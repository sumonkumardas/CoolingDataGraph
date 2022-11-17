using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoolingDataGraph.Models;

namespace CoolingDataGraph.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           return View();
        }

        public ActionResult About()
        {
            var postedFile = Request.Files["fileUpload"];
            var filePath = Path.Combine(Server.MapPath("~/CSV Files/"), "InputFile.csv");
            postedFile.SaveAs(filePath);

           
            return View();
        }

        public ActionResult GetGraphValues(string path)
        {
            var filePath = Path.Combine(Server.MapPath("~/App_Data/"), "InputFile.csv");
            var coolingLoadDataList = new CoolingData();
            coolingLoadDataList.CoolingLoadDataList = coolingLoadDataList.ConvertCoolingDataFromCsv(HttpContext.Server.MapPath("~/CSV Files/InputFile.csv"));
            var model = new GraphModel();
            model.SetModelValue(coolingLoadDataList.CoolingLoadDataList);
            return Json(new { GraphModel = model }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}