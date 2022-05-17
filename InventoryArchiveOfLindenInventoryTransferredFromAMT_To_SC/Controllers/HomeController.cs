using InventoryArchiveOfLindenInventoryTransferredFromAMT_To_SC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryArchiveOfLindenInventoryTransferredFromAMT_To_SC.Controllers
{
    public class HomeController : Controller
    {
        DBHelper objDB = new DBHelper();

        public ActionResult Index()
        {

            Session["User"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult Index(BentexUser model)
        {
            //var s = objDB.GetCBLoginInfo(model.UserName, model.Password);
            //var item = s.FirstOrDefault();
            Session.Clear();
            var item = ((model.UserName == "admin" && model.Password == "nasdaq") || (model.UserName == "admin" && model.Password == "bobby")) ? "Success" : "User Does not Exists";

            if (item == "Success")
            {
                Session["User"] = "Admin";
                return RedirectToAction("Index", "Calculate");
            }
            else if (item == "User Does not Exists")
            {
                ViewBag.NotValidUser = item;

            }
            else
            {
                ViewBag.Failedcount = item;
            }

            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Clear();
            Session["User"] = null;
            //FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
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