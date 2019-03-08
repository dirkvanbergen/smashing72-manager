using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using smashing72_manager.Models;

namespace smashing72_manager.Controllers
{
    public class TeamController : Controller
    {
        // GET: Teams
        public ActionResult Index()
        {
            List<Team> list;
            using (var db = new SmashingModel())
            {
                list = db.Teams.ToList();
            }

            ViewBag.List = list;

            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            List<Team> list;
            using (var db = new SmashingModel())
            {
                list = db.Teams.ToList();
            }

            var model = list.FirstOrDefault(n => n.Id == id) ?? new Team();

            ViewBag.List = list;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Team model)
        {
            using (var db = new SmashingModel())
            {
                if (model.Id == 0)
                {
                    db.Teams.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    db.Teams.Attach(model);
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}