using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using smashing72_manager.Models;

namespace smashing72_manager.Controllers.api
{
    [System.Web.Mvc.Authorize]
    public class NewsController : ApiController
    {
        private SmashingModel db = new SmashingModel();

        // GET: api/News
        public IEnumerable<NewsVm> GetNews()
        {
            var news = db.News.ToList().Select(NewsVm.FromNews).ToList();
            return news;
        }

        // GET: api/News/5
        [ResponseType(typeof(NewsVm))]
        public IHttpActionResult GetNews(int id)
        {
            News news = db.News.Find(id);
            if (news == null)
            {
                return NotFound();
            }

            return Ok(NewsVm.FromNews(news));
        }

        // PUT: api/News/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNews(int id, NewsVm newsVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var news = newsVm.ToNews();

            if (id != news.Id)
            {
                return BadRequest();
            }

            db.Entry(news).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/News
        [ResponseType(typeof(NewsVm))]
        public IHttpActionResult PostNews(NewsVm newsVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var news = newsVm.ToNews();

            db.News.Add(news);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = news.Id }, NewsVm.FromNews(news));
        }

        // DELETE: api/News/5
        [ResponseType(typeof(NewsVm))]
        public IHttpActionResult DeleteNews(int id)
        {
            News news = db.News.Find(id);
            if (news == null)
            {
                return NotFound();
            }

            db.News.Remove(news);
            db.SaveChanges();

            return Ok(NewsVm.FromNews(news));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NewsExists(int id)
        {
            return db.News.Count(e => e.Id == id) > 0;
        }
    }
    public class NewsVm
    {
        private NewsVm() { }
        public static NewsVm FromNews(News n)
        {
            return new NewsVm
            {
                Id = n.Id,
                Title = n.Title,
                Article = n.Article,
                AuthorId = n.AuthorId,
                Year = n.PublishDate.Year,
                Month = n.PublishDate.Month,
                Day = n.PublishDate.Day,
                MonthName = n.PublishDate.ToString("MMMM", new CultureInfo("NL-nl")),
            };
        }

        public News ToNews()
        {
            return new News
            {
                Id = Id,
                Title = Title,
                Article = Article,
                AuthorId = AuthorId,
                PublishDate = new DateTime(Year, Month, Day)
            };
        }
        public int Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Title { get; set; }
        [Required]
        [AllowHtml]
        public string Article { get; set; }

        public int AuthorId { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public string MonthName { get; set; }
    }
}