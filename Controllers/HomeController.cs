using MVCRoutines.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCRoutines.Controllers
{
    public class HomeController : Controller
    {
        private DailyRoutinesEntities db = new DailyRoutinesEntities();

        public ActionResult Index()
        {
            var routines = from r in db.Routines
                           select r;

            return View(routines);
        }

        public ActionResult Create()
       
        {
            return View();
        }

        //HttpPost annotation tells MVC to use POST request to server 

        [HttpPost]
        public ActionResult Create(Routine routine)
        //Ok for 2 methods with the same name("Create")
        //Because they have different parameter - this is overloading.
        {
            db.Routines.Add(routine);
            db.SaveChanges();
            //db.SaveChanges sends database changes to server.  
            return RedirectToAction("Index");

            //Redirects user through different action method to see updated records.
        }

        //For Edit method the record id is passed by the HttpActionLink as an 
        public ActionResult Edit(int id)

        {
            //input parameter. id used to look up record in database.
            Routine routine = db.Routines.Find(id);

            //Sends data to edit view.
            return View(routine);
        }

        //Post action method to allow edits to be saved
        [HttpPost]
        public ActionResult Edit(Routine routine)

        {
            //modifies existing record with changes passed in from view.
            db.Entry(routine).State = EntityState.Modified;
            //using System.Date.Entity!!!!
            db.SaveChanges();
            return RedirectToAction("Index");
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