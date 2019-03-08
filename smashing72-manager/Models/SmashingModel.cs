namespace smashing72_manager.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SmashingModel : DbContext
    {
        public SmashingModel()
            : base("name=SmashingModel")
        {
        }

        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<HtmlData> HtmlDatas { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Content>()
                .HasMany(e => e.Children)
                .WithOptional(e => e.Parent)
                .HasForeignKey(e => e.ParentContentId);
        }
    }
}
