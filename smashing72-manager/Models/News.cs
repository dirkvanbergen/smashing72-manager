﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace smashing72_manager.Models
{

    [Table("smashing72_admin.News")]
    public class News
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        public string Article { get; set; }

        public DateTime PublishDate { get; set; }

        public int AuthorId { get; set; }

        public int? IsReplacedById { get; set; }

        public virtual User Author { get; set; }

        public virtual News IsReplacedBy { get; set; }
    }
}