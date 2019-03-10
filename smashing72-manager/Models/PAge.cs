namespace smashing72_manager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("smashing72_admin.Page")]
    public partial class Page
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(100)]
        public string MenuTitle { get; set; }

        [StringLength(50)]
        public string UrlSegment { get; set; }

        public bool ShowInMenu { get; set; }

        public int? MenuOrder { get; set; }

        public int? ParentContentId { get; set; }

        [StringLength(50)]
        public string DataType { get; set; }

        public string Html { get; set; }
    }
}
