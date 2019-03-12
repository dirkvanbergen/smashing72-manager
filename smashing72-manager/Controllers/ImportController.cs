using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using smashing72_manager.Models;

namespace smashing72_manager.Controllers
{
    public class ImportController : Controller
    {
        // GET: Import
        public ActionResult Index()
        {
            var list = new List<News>();
            var request = WebRequest.Create("https://smashing72.nl/index.html");
            using (var stream = request.GetResponse().GetResponseStream())
            {
                var readingTitle = false;
                var readingContent = false;
                var currentContent = "";
                var currentTitle = "";
                var currentDate = DateTime.MinValue;

                using (var reader = new StreamReader(stream, Encoding.Default))
                {
                    var linenum = 0;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine() ?? "";
                        linenum++;

                        if (line.Contains("<font class=\"paginaTitel\">"))
                        {
                            readingTitle = true;
                        }
                        else if (line.Contains("<font class=\"content\">"))
                        {
                            readingContent = true;
                        }
                        else if (line.Contains("</font>") && readingTitle)
                        {
                            readingTitle = false;
                        }
                        else if (line.Contains("</font>") && readingContent)
                        {
                            if (currentContent.Length > 0 && currentTitle.Length > 0 && currentDate > DateTime.MinValue)
                            {
                                list.Add(new News
                                {
                                    Title = currentTitle,
                                    Article = currentContent,
                                    PublishDate = currentDate,
                                    AuthorId = 1
                                });
                            }

                            readingContent = false;
                            readingTitle = false;
                            currentContent = "";
                            currentTitle = "";
                            currentDate = DateTime.MinValue;
                        }
                        else if (readingTitle)
                        {
                            var parts = line.Split(new [] {"..."}, StringSplitOptions.RemoveEmptyEntries);
                            if (parts.Length == 2)
                            {
                                currentDate = DateTime.ParseExact(parts[0].Trim(), "dd-MM-yyyy",
                                    CultureInfo.InvariantCulture);
                                currentTitle = parts[1].Trim();
                            }
                        }
                        else if (readingContent)
                        {
                            currentContent += line;
                        }
                    }
                }
            }

            list.Reverse();
            using (var db = new SmashingModel())
            {
                db.News.AddRange(list);
                db.SaveChanges();
            }

            return null;
        }
    }
}