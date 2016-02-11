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
    public class ErrandsController : Controller
    {
        private ErranderContext db = new ErranderContext();
        private ApplicationDbContext context = new ApplicationDbContext();
        

        // GET: Errands
        public ActionResult Index()
        {
          
            var currentUserID = User.Identity.GetUserId();
            ApplicationUser currentUser = context.Users.Find(currentUserID);
            return View(db.errands.Where(x => x.City == currentUser.City && x.IsCompleted == false && x.InProgress == false).ToList());
        }

        public ActionResult ErrandsCompletedByUser()
        {
            var currentUserID = User.Identity.GetUserId();
            return PartialView(db.errands.Where(x => x.AcceptingUserID == currentUserID && x.IsCompleted == true));
            //return PartialView()
        }

        

        // GET: Errands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Errand errand = db.errands.Find(id);
            if (errand == null)
            {
                return HttpNotFound();
            }
            return View(errand);
        }

        // GET: Errands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Errands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,CurrentLocation,ErrandDestination,City,IsCompleted,InProgress,AmountOffered,NeededBy")] Errand errand)
        {
            if (ModelState.IsValid)
            {
          
                var currentUserID = User.Identity.GetUserId();
                errand.UserId = currentUserID;
                //errand.AcceptingUserID = null;
                db.errands.Add(errand);
                db.SaveChanges();
                return RedirectToAction("GetUserSubmittedErrands"); //redirect to users submitted 
            }

            return View(errand);
        }
        public ActionResult ShowErrandsCompletedForUser()
        {
            string CurrentUserID = User.Identity.GetUserId();
            return PartialView(db.errands.Where(x => x.UserId == CurrentUserID && x.IsCompleted == true));
        }
        public ActionResult GetUserSubmittedErrands()
        {
            string CurrentUserID = User.Identity.GetUserId();
            return View(db.errands.Where(x => x.UserId == CurrentUserID && x.IsCompleted == false));
        }
        public ActionResult MarkComplete(int? id)
        {
            Errand errand = db.errands.Find(id);
            if (errand == null)
            {
                return HttpNotFound();
            }
            errand.InProgress = false;
            errand.IsCompleted = true;
            errand.CompletionTime = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        // GET: Errands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Errand errand = db.errands.Find(id);
            if (errand == null)
            {
                return HttpNotFound();
            }
            return View(errand);
        }

        // POST: Errands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserId,Name,CurrentLocation,ErrandDestination,City,IsCompleted,InProgress,AmountOffered,NeededBy")] Errand errand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(errand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(errand);
        }

        public ActionResult CommitToErrand(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Errand errand = db.errands.Find(id);
            errand.InProgress = true;
            errand.AcceptingUserID = User.Identity.GetUserId();
            return RedirectToAction("Index","Home");

        }

        // GET: Errands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Errand errand = db.errands.Find(id);
            if (errand == null)
            {
                return HttpNotFound();
            }
            return View(errand);
        }

        // POST: Errands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Errand errand = db.errands.Find(id);
            db.errands.Remove(errand);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        

        public ActionResult TakeErrand(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Errand errand = db.errands.Find(id);
           
            return View(errand);
        }
        [HttpPost, ActionName("TakeErrand")]
        [ValidateAntiForgeryToken]
        public ActionResult TakeErrandConfirmation(int id)
        {
            Errand errand = db.errands.Find(id);
            errand.InProgress = true;
            errand.AcceptingUserID = User.Identity.GetUserId();
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ShowErrandsAcceptedByUser()
        {
            string currentUserID = User.Identity.GetUserId();
            return View(db.errands.Where(x => x.AcceptingUserID == currentUserID && x.InProgress == true));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
