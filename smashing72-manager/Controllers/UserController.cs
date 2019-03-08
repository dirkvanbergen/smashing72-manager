using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using smashing72_manager.Models;

namespace smashing72_manager.Controllers
{
    public class UserController : Controller
    {
        // GET: Teams
        public ActionResult Index()
        {
            List<User> list;
            using (var db = new SmashingModel())
            {
                list = db.Users.ToList();
            }

            ViewBag.List = list;

            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            List<User> list;
            using (var db = new SmashingModel())
            {
                list = db.Users.ToList();
            }

            var model = list.FirstOrDefault(n => n.Id == id) ?? new User();

            ViewBag.List = list;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(User model)
        {
            using (var db = new SmashingModel())
            {
                if (model.Id == 0)
                {
                    db.Users.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    db.Users.Attach(model);
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}