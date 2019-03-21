using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using smashing72_manager.Models;

namespace smashing72_manager.Controllers.api
{
    [Authorize]
    [RoutePrefix("api/page")]
    public class PageController : ApiController
    {
        private SmashingModel db = new SmashingModel();

        [HttpGet, Route("")]
        public IQueryable<Page> GetPages()
        {
            return db.Pages;
        }

        // GET: api/page/5
        [ResponseType(typeof(Page))]
        [HttpGet, Route("{id}")]
        public IHttpActionResult GetPage(int id)
        {
            Page content = db.Pages.Find(id);
            if (content == null)
            {
                return NotFound();
            }

            return Ok(content);
        }

        // PUT: api/page/5
        [ResponseType(typeof(void))]
        [HttpPost, Route("update/{id}")]
        public IHttpActionResult PutPage(int id, Page content)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != content.Id)
            {
                return BadRequest();
            }

            db.Entry(content).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentExists(id))
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

        // POST: api/page
        [ResponseType(typeof(Page))]
        [HttpPost, Route("add/{id}")]
        public IHttpActionResult PostPage(Page content)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pages.Add(content);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = content.Id }, content);
        }

        // DELETE: api/Page/5
        [ResponseType(typeof(Page))]
        [HttpPost, Route("delete/{id}")]
        public IHttpActionResult DeleteContent(int id)
        {
            Page content = db.Pages.Find(id);
            if (content == null)
            {
                return NotFound();
            }

            db.Pages.Remove(content);
            db.SaveChanges();

            return Ok(content);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContentExists(int id)
        {
            return db.Pages.Count(e => e.Id == id) > 0;
        }
    }
}