using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Errander.Models;
using Microsoft.AspNet.Identity;

namespace Errander.Controllers
{
    public class HomeController : Controller
    {
        private ErranderContext db = new ErranderContext();
        ApplicationDbContext context = new ApplicationDbContext();
        
        [Authorize]
        public ActionResult Index()
        {
            var currentUserID = User.Identity.GetUserId();
            ApplicationUser currentUser = context.Users.Find(currentUserID);
            
            return View(db.errands.Where(x => x.City == currentUser.City && x.IsCompleted == false && x.InProgress == false));
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