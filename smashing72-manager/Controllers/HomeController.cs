using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using smashing72_manager.Models;

namespace smashing72_manager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var contents = new List<Content>();
            using (var db = new SmashingModel())
            {
                contents = db.Contents.ToList();
            }

            ViewBag.Contents = contents;
            return View();
        }

        public ActionResult Edit(int contentId = 0)
        {
            var contents = new List<Content>();
            using (var db = new SmashingModel())
            {
                contents = db.Contents.Include("HtmlData").ToList();
            }

            var model = contents.FirstOrDefault(c => c.Id == contentId) ?? new Content();

            ViewBag.Contents = contents;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Content content)
        {
            using (var db = new SmashingModel())
            {
                if (content.Id == 0)
                {
                    db.Contents.Add(content);
                    db.SaveChanges();
                }
                else
                {
                    db.Contents.Attach(content);
                    db.HtmlDatas.Attach(content.HtmlData);
                    db.Entry(content).State = EntityState.Modified;
                    db.Entry(content.HtmlData).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}