using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using smashing72_manager.Models;

namespace smashing72_manager.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content
        public ActionResult Index()
        {
            List<Content> contents;
            using (var db = new SmashingModel())
            {
                contents = db.Contents.ToList();
            }

            ViewBag.List = contents;

            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            List<Content> list;
            using (var db = new SmashingModel())
            {
                list = db.Contents.Include("HtmlData").ToList();
            }

            var model = list.FirstOrDefault(c => c.Id == id) ?? new Content();

            ViewBag.List = list;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Content model)
        {
            using (var db = new SmashingModel())
            {
                if (model.Id == 0)
                {
                    db.Contents.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    db.Contents.Attach(model);
                    db.HtmlDatas.Attach(model.HtmlData);
                    db.Entry(model).State = EntityState.Modified;
                    db.Entry(model.HtmlData).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}