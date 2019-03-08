using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using smashing72_manager.Models;

namespace smashing72_manager.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            List<News> list;
            using (var db = new SmashingModel())
            {
                list = db.News.ToList();
            }

            ViewBag.List = list;

            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            List<News> list;
            using (var db = new SmashingModel())
            {
                list = db.News.ToList();
            }

            var model = list.FirstOrDefault(n => n.Id == id) ?? new News();

            ViewBag.List = list;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(News model)
        {
            using (var db = new SmashingModel())
            {
                if (model.Id == 0)
                {
                    model.PublishDate = DateTime.Now;
                    model.AuthorId = 1;
                    db.News.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    db.News.Attach(model);
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}