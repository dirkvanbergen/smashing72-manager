using smashing72_manager.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace smashing72_manager.ViewModels
{
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