using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace smashing72_manager.Controllers.api
{
    public class ImageController : ApiController
    {
        [HttpPost, Route("api/image")]
        public IHttpActionResult PostImage()
        {
            var httpRequest = HttpContext.Current.Request;
            var postedFile = httpRequest.Files.Get(0);
            var savePath = HttpContext.Current.Server.MapPath("~/img/" + postedFile.FileName);
            postedFile.SaveAs(savePath);
            var location = "/img/" + postedFile.FileName;
            return Json(new { location });
        }
    }
}
