using smashing72_manager.Models;
using smashing72_manager.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace smashing72_manager.Controllers.api
{
    [Authorize]
    [RoutePrefix("api/news")]
    public class NewsController : ApiController
    {
        private SmashingModel db = new SmashingModel();

        [HttpGet, Route("")]
        public IEnumerable<NewsVm> GetNews()
        {
            var news = db.News.ToList().Select(NewsVm.FromNews).ToList();
            return news;
        }

        [ResponseType(typeof(NewsVm))]
        [HttpGet, Route("{id}")]
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
        [HttpPost, Route("update/{id}")]
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
        [HttpPost, Route("add", Name = "PostNews")]
        public IHttpActionResult PostNews(NewsVm newsVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var news = newsVm.ToNews();

            db.News.Add(news);
            db.SaveChanges();

            return CreatedAtRoute("PostNews", new { id = news.Id }, NewsVm.FromNews(news));
        }

        // DELETE: api/News/5
        [ResponseType(typeof(NewsVm))]
        [HttpPost, Route("delete/{id}")]
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
}